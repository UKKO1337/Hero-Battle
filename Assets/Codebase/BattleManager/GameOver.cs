using System;
using UnityEngine;

namespace Codebase.BattleManager
{
  public class GameOver : MonoBehaviour
  {
    [SerializeField] private EnemyTurn _enemyTurn;
    [SerializeField] private PlayerTurn _playerTurn;
    [SerializeField] private GameObject _winGameOver;
    [SerializeField] private GameObject _loseGameOver;

    public event Action GameOverHappened;

    private void Start()
    {
      _playerTurn.EveryoneIsDead += Lose;
      _enemyTurn.EveryoneIsDead += Win;
    }
    

    private void Lose()
    {
      if (_playerTurn.PlayerUnits.Count == 0)
      {
        GameOverHappened?.Invoke();
        Instantiate(_loseGameOver);
      }
        
    }

    private void Win()
    {
      if (_enemyTurn.EnemyUnits.Count == 0)
      {
        GameOverHappened?.Invoke();
        Instantiate(_winGameOver);
      }
        
    }
  }
}