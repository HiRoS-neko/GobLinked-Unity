using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] private GameMode _gameMode;

    [SerializeField] public Gnox _gnox;
    [SerializeField] private GoblinUI _gnoxUI;

    [SerializeField] private Inventory _inv;

    [SerializeField] private AbilityManager _gnoxAbilityManager;
    [SerializeField] private AbilityManager _krilkAbilityManager;


    [SerializeField] public Krilk _krilk;

    [SerializeField] private GoblinUI _krilkUI;

    [SerializeField] private Player1 _player1;
    [SerializeField] private Player2 _player2;
    [SerializeField] [Range(1, 10)] private float _speedMultiplier;

    private void Awake()
    {
        _player1.ControlledGoblin = _krilk;
        if (_gameMode == GameMode.MultiPlayer) _player2.ControlledGoblin = _gnox;
        else _chain.SetActive(false);

        _krilk.HealthUI = _krilkUI.HealthManager;
        _gnox.HealthUI = _gnoxUI.HealthManager;
        
        _krilk.AbilityManager = _krilkAbilityManager;
        _gnox.AbilityManager = _gnoxAbilityManager;

        
        
        _krilk.Items = _inv;
        _gnox.Items = _inv;

        SpeedMultiplier = _speedMultiplier;

        GlobalScript.SceneChanged += GlobalScriptOnSceneChanged;
    }

    public void SetMode(GameMode gameMode)
    {
        _gameMode = gameMode;
        switch (_gameMode)
        {
            case GameMode.MultiPlayer:
                _player1.ControlledGoblin = _krilk;
                _player2.ControlledGoblin = _gnox;
                _gnox.gameObject.SetActive(true);
                _krilk.gameObject.SetActive(true);
                _chain.SetActive(true);
                break;
            case GameMode.SinglePlayer:
                _player1.ControlledGoblin = _krilk;
                _player2.ControlledGoblin = null;
                _gnox.gameObject.SetActive(false);
                _krilk.gameObject.SetActive(true);
                _chain.SetActive(false);
                break;
        }
    }

    private void GlobalScriptOnSceneChanged(Scene prevScene)
    {
        //find object in scene with the name of the previous scene
        var spawn = GameObject.Find(prevScene.name);
        //move both goblins to gameobject
        if (spawn != null)
        {
            _gnox.gameObject.transform.position = spawn.transform.position;
            _krilk.gameObject.transform.position = spawn.transform.position;
        }
    }

    private void Update()
    {
        switch (_gameMode)
        {
            case GameMode.SinglePlayer:
                _camera.transform.position = Vector3.Lerp(_camera.transform.position,
                                                 _player1.ControlledGoblin.transform.position +
                                                 (Vector3) _player1.ControlledGoblin.Rigid.velocity, 0.05f) +
                                             3 * Vector3.back;
                break;
            case GameMode.MultiPlayer:
                _camera.transform.position = Vector3.Lerp(_camera.transform.position, (_player1.ControlledGoblin
                                                                                           .transform.position +
                                                                                       _player2.ControlledGoblin
                                                                                           .transform.position) / 2 +
                                                                                      (Vector3) ((
                                                                                                     _player1
                                                                                                         .ControlledGoblin
                                                                                                         .Rigid
                                                                                                         .velocity +
                                                                                                     _player2
                                                                                                         .ControlledGoblin
                                                                                                         .Rigid
                                                                                                         .velocity) /
                                                                                                 2) + 3 * Vector3.back,
                    0.05f);
                break;
        }
    }

    public void SwitchPlayers()
    {
        switch (_gameMode)
        {
            case GameMode.MultiPlayer:
                //Switch Which goblins the players are referencing
                var tempGobo = _player1.ControlledGoblin;
                _player1.ControlledGoblin = _player2.ControlledGoblin;
                _player2.ControlledGoblin = tempGobo;
                Debug.Log("Switched between Goblins");
                break;
            case GameMode.SinglePlayer:
                Goblin currentGoblin, newGoblin;
                if (_player1.ControlledGoblin == _krilk)
                {
                    currentGoblin = _krilk;
                    newGoblin = _gnox;
                }
                else
                {
                    currentGoblin = _gnox;
                    newGoblin = _krilk;
                }

                //Teleport other goblin to current goblin
                newGoblin.gameObject.transform.position = currentGoblin.gameObject.transform.position;
                //enable new goblin
                newGoblin.gameObject.SetActive(true);
                //switch which goblin is referenced
                _player1.ControlledGoblin = newGoblin;
                //disable original goblin
                currentGoblin.gameObject.SetActive(false);
                break;
        }
    }
}