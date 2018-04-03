using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum GameMode
    {
        SinglePlayer,
        MultiPlayer
    }

    public static float SpeedMultiplier;

    [SerializeField] private GameObject _chain;
    [SerializeField] private Camera _camera;

    [SerializeField] private GameMode _gameMode;

    [SerializeField] private Gnox _gnox;
    [SerializeField] private GoblinUI _gnoxUI;

    [SerializeField] private Inventory _inv;
    [SerializeField] private Krilk _krilk;

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

        SpeedMultiplier = _speedMultiplier;
    }

    private void Update()
    {
        switch (_gameMode)
        {
            case GameMode.SinglePlayer:
                _camera.transform.position = Vector3.Lerp(_camera.transform.position,
                                                 _player1.ControlledGoblin.transform.position +
                                                 (Vector3) _player1.ControlledGoblin.Rigid.velocity, 0.05f) +
                                             Vector3.back;
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
                                                                                                 2) + Vector3.back,
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