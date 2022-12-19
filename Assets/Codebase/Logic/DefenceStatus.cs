using System;
using Codebase.BattleManager;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Logic
{
  public class DefenceStatus : MonoBehaviour
  {
    [SerializeField] private Image _defence;
    [SerializeField] private Health _health;

    public float MaxDef = 30;
    public int DefActiveTurns = 3;

    public bool IsUnderDef => _isUnderDef;

    private float _currentDef;
    private int _currentDefTurn;
    private bool _isUnderDef;
    private TurnCounter _turnCounter;
    

    private void Start()
    {
      _turnCounter = FindObjectOfType<TurnCounter>();
      SetValue();
    }

    private void Update()
    {
      if (DefenceTimesUp())
      {
        _isUnderDef = false;
        _currentDef = 0;
        SetValue();
      }
    }

    private bool DefenceTimesUp() => 
      _turnCounter.CurrentTurn == _currentDefTurn + DefActiveTurns || _currentDef <= 0;

    public void MakeAction()
    {
      _isUnderDef = true;
      _currentDefTurn = _turnCounter.CurrentTurn;
      _currentDef += 5;
      SetValue();
    }

    public void TakeDamage(int damage)
    {
      if (_currentDef >= damage)
      {
        _currentDef -= damage;
        SetValue();
      }

      else
      {
        var negativeDamage = (int)_currentDef - damage;
        var positiveDamage = Math.Abs(negativeDamage);
        _health.TakeDamage(positiveDamage);
        _currentDef = 0;
      }
      
    }

    private void SetValue() => 
      _defence.fillAmount = _currentDef / MaxDef;
    
    
  }
}