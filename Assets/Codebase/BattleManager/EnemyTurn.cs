using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Codebase.Enemy;
using Codebase.Logic;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.BattleManager
{
  public class EnemyTurn : MonoBehaviour
  {
    [SerializeField] private List<GameObject> _enemyUnits;
    [SerializeField] private Button _endTurn;
    [SerializeField] private TurnCounter _turnCounter;
    [SerializeField] private PlayerTurn _playerTurn;
    [SerializeField] private GameOver _gameOver;

    public event Action EnemyTurnStarted;
    public event Action EveryoneIsDead;
    public List<GameObject> EnemyUnits => _enemyUnits;

    private int _currentEnemy;
    private EnemyAttack _enemyAttack;


    private void Start()
    {
      foreach (GameObject unit in _enemyUnits) 
        unit.GetComponent<Death>().DeathHappend += ClearDeadUnit;

      _endTurn.onClick.AddListener(Attack);
      _gameOver.GameOverHappened += StartTurn;
    }

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space)) 
        Attack();
    }

    private void Attack()
    {
      StartTurn();
      
      while (true)
      {
        if (_currentEnemy < _enemyUnits.Count)
        {
          if (CurrentEnemyIsDead())
          {
            _enemyUnits.RemoveAt(_currentEnemy);
            continue;
          }
          
          MakeAction();
        }

        else
          EndTurn();

        break;
      }
    }

    private void StartTurn()
    {
      _endTurn.gameObject.SetActive(false);
      EnemyTurnStarted?.Invoke();
    }

    private void EndTurn()
    {
      _playerTurn.ResetActiveUnit();
      _playerTurn.ChangeActiveUnit();
      _currentEnemy = 0;
      _endTurn.gameObject.SetActive(true);
      _turnCounter.CurrentTurn++;
    }

    private void MakeAction()
    {
      _enemyAttack = _enemyUnits[_currentEnemy].GetComponent<EnemyAttack>();
      _enemyAttack.MakeAction();
      _enemyAttack.ActionEnded += ChangeEnemy;
    }

    private bool CurrentEnemyIsDead() => 
      _enemyUnits[_currentEnemy] == null && _currentEnemy != _enemyUnits.Count;

    private void ChangeEnemy()
    {
      _enemyAttack.ActionEnded -= ChangeEnemy;
      _currentEnemy++;
      Attack();
    }

    private void ClearDeadUnit() => 
      StartCoroutine(CheckForDeadUnits());

    private IEnumerator CheckForDeadUnits()
    {
      yield return null;
            
      for(var i = _enemyUnits.Count - 1; i > -1; i--)
      {
        if (_enemyUnits[i] == null)
          _enemyUnits.RemoveAt(i);
      }

      if (_enemyUnits.Count == 0) 
        EveryoneIsDead?.Invoke();
    }

    private void OnDestroy()
    {
      foreach (GameObject unit in _enemyUnits.Where(unit => unit != null))
        unit.GetComponent<Death>().DeathHappend -= ClearDeadUnit;
    }
  }
}