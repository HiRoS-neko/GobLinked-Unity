using UnityEditor.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum GameMode
    {
        SinglePlayer,
        MultiPlayer
    }

    [SerializeField] private GameMode _gameMode;


    [SerializeField] private Player1 _player1;
    [SerializeField] private Player2 _player2;

    [SerializeField] private Gnox _gnox;
    [SerializeField] private Krilk _krilk;

    private void Start()
    {
        _player1.ControlledGoblin = _gnox;
        if (_gameMode == GameMode.MultiPlayer)
        {
            _player2.ControlledGoblin = _krilk;
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
                _player1.UpdateGoblinReference();
                _player2.UpdateGoblinReference();
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
                _player1.UpdateGoblinReference();
                break;
        }
    }
}