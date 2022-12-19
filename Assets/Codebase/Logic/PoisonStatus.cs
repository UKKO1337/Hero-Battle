using Codebase.BattleManager;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Logic
{
  public class PoisonStatus : MonoBehaviour
  {
    [SerializeField] private Health _health;
    [SerializeField] private Image _currentHp;

    public int PoisonActiveTurns = 1;
    
    private TurnCounter _turnCounter;
    private Color _nonPoisonHpColor;
    private bool _isPoisoned;
    private int _poisonedTurn;


    private void Start()
    {
      _nonPoisonHpColor = _currentHp.color;
      _turnCounter = FindObjectOfType<TurnCounter>();
      _health.Poisoned += GetPoisoned;
    }

    private void Update()
    {
      if (PoisonTimesUp())
      {
        _health.TakePoisonDamage();
        GetHealedFromPoison();
      }
    }

    private bool PoisonTimesUp() => 
      _isPoisoned && _turnCounter.CurrentTurn == _poisonedTurn + PoisonActiveTurns;

    private void GetPoisoned()
    {
      _currentHp.color = Color.green;
      _isPoisoned = true;
      _poisonedTurn = _turnCounter.CurrentTurn;
    }

    public void GetHealedFromPoison()
    {
      _isPoisoned = false;
      _currentHp.color = _nonPoisonHpColor;
    }
  }
}