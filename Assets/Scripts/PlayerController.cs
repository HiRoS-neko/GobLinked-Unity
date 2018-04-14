using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum GameMode
    {
        SinglePlayer,
        MultiPlayer
    }

    public static float SpeedMultiplier;
    [SerializeField] private Camera _camera;

    [SerializeField] private GameObject _chain;
    private bool _delay = true;

    [SerializeField] public GameMode _gameMode;

    [SerializeField] public Gnox _gnox;

    [SerializeField] private AbilityManager _gnoxAbilityManager;
    [SerializeField] private GoblinUI _gnoxUI;

    [SerializeField] private Inventory _inv;


    [SerializeField] public Krilk _krilk;
    [SerializeField] private AbilityManager _krilkAbilityManager;

    [SerializeField] private GoblinUI _krilkUI;
    [SerializeField] [Range(1, 10)] private float _speedMultiplier;

    [SerializeField] public Player1 Player1;
    [SerializeField] public Player2 Player2;
    private bool _switch;

    private void Awake()
    {
        Player1.ControlledGoblin = _krilk;
        if (_gameMode == GameMode.MultiPlayer) Player2.ControlledGoblin = _gnox;
        else _chain.SetActive(false);

        _krilk.HealthUI = _krilkUI.HealthManager;
        _gnox.HealthUI = _gnoxUI.HealthManager;

        _krilk.AbilityManager = _krilkAbilityManager;
        _gnox.AbilityManager = _gnoxAbilityManager;


        _krilk.Items = _inv;
        _gnox.Items = _inv;

        SpeedMultiplier = _speedMultiplier;

        GlobalScript.SceneChanged += GlobalScriptOnSceneChanged;

        SetMode(_gameMode);
    }

    public void SetMode(GameMode gameMode)
    {
        _gameMode = gameMode;
        switch (_gameMode)
        {
            case GameMode.MultiPlayer:
                Player1.ControlledGoblin = _krilk;
                Player1.GoblinType = Goblin.GoblinType.Krilk;
                Player2.ControlledGoblin = _gnox;
                Player2.GoblinType = Goblin.GoblinType.Gnox;
                _gnox.gameObject.SetActive(true);
                _krilk.gameObject.SetActive(true);
                _chain.SetActive(true);
                break;
            case GameMode.SinglePlayer:
                Player1.ControlledGoblin = _krilk;
                Player1.GoblinType = Goblin.GoblinType.Krilk;
                Player2.ControlledGoblin = null;
                _gnox.gameObject.SetActive(false);
                _krilk.gameObject.SetActive(true);
                _chain.SetActive(false);
                break;
        }
    }

    private void GlobalScriptOnSceneChanged(string prevScene)
    {
        //find object in scene with the name of the previous scene
        var spawns = GameObject.FindGameObjectsWithTag("Spawn");

        foreach (var spawn in spawns)
            if (spawn.name == prevScene)
            {
                _chain.gameObject.transform.position = (Vector3) (Vector2) spawn.transform.position +
                                                       Vector3.forward * _chain.gameObject.transform.position.z;
                _krilk.gameObject.transform.position = (Vector3) (Vector2) spawn.transform.position +
                                                       Vector3.forward * _krilk.gameObject.transform.position.z;
                _gnox.gameObject.transform.position = (Vector3) (Vector2) spawn.transform.position +
                                                      Vector3.forward * _gnox.gameObject.transform.position.z;
                _camera.gameObject.transform.position = (Vector3) (Vector2) spawn.transform.position +
                                                        Vector3.forward * _camera.gameObject.transform.position.z;
                break;
            }
    }

    private void Update()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Switch")) > 0.5f && _switch)
        {
            SwitchPlayers();
            _switch = false;
        }
        else if (Mathf.Abs(Input.GetAxisRaw("Switch")) < 0.5f)
        {
            _switch = true;
        }

        switch (_gameMode)
        {
            case GameMode.SinglePlayer:
                _camera.transform.position = Vector3.Lerp(_camera.transform.position,
                                                 Player1.ControlledGoblin.transform.position +
                                                 (Vector3) Player1.ControlledGoblin.Rigid.velocity / 2, 0.02f) +
                                             10 * Vector3.back;
                break;
            case GameMode.MultiPlayer:
                _camera.transform.position = Vector3.Lerp(_camera.transform.position, (Player1.ControlledGoblin
                                                                                           .transform.position +
                                                                                       Player2.ControlledGoblin
                                                                                           .transform.position) / 2 +
                                                                                      (Vector3) (
                                                                                          (Player1.ControlledGoblin
                                                                                               .Rigid.velocity +
                                                                                           Player2.ControlledGoblin
                                                                                               .Rigid.velocity) / 4) +
                                                                                      10 * Vector3.back, 0.02f);
                break;
        }

        if (Input.GetAxisRaw("Cancel") > 0.5 && _delay)
        {
            _delay = false;
            GlobalScript.Paused = !GlobalScript.Paused;
            if (GlobalScript.Paused)
            {
                Time.timeScale = 0;
                _inv.ShowInventory(GlobalScript.Paused);
            }
            else
            {
                Time.timeScale = 1;
                _inv.ShowInventory(GlobalScript.Paused);
            }
        }
        else if (Input.GetAxisRaw("Cancel") < 0.5)
        {
            _delay = true;
        }
    }

    public void SwitchPlayers()
    {
        switch (_gameMode)
        {
            case GameMode.MultiPlayer:
                //Switch Which goblins the players are referencing
                var tempGobo = Player1.ControlledGoblin;
                Player1.ControlledGoblin = Player2.ControlledGoblin;
                Player2.ControlledGoblin = tempGobo;

                if (Player1.ControlledGoblin == _krilk)
                {
                    Player1.GoblinType = Goblin.GoblinType.Krilk;
                    Player2.GoblinType = Goblin.GoblinType.Gnox;
                }
                else
                {
                    Player1.GoblinType = Goblin.GoblinType.Gnox;
                    Player2.GoblinType = Goblin.GoblinType.Krilk;
                }


                Debug.Log("Switched between Goblins");
                break;


            case GameMode.SinglePlayer:
                Goblin currentGoblin, newGoblin;
                if (Player1.ControlledGoblin == _krilk)
                {
                    currentGoblin = _krilk;
                    newGoblin = _gnox;
                    Player1.GoblinType = Goblin.GoblinType.Gnox;
                }
                else
                {
                    currentGoblin = _gnox;
                    newGoblin = _krilk;
                    Player1.GoblinType = Goblin.GoblinType.Krilk;
                }

                //Teleport other goblin to current goblin
                newGoblin.gameObject.transform.position = currentGoblin.gameObject.transform.position;
                //enable new goblin
                newGoblin.gameObject.SetActive(true);
                //switch which goblin is referenced
                Player1.ControlledGoblin = newGoblin;
                //disable original goblin
                currentGoblin.gameObject.SetActive(false);
                break;
        }
    }
}