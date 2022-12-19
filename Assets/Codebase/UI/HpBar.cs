using Codebase.Logic;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.UI
{
  public class HpBar : MonoBehaviour
  {
    [SerializeField] private Image _currentHp;
    [SerializeField] private Health _health;

    private void Start() => 
      _health.HealthChanged += SetValue;

    private void SetValue() => 
      _currentHp.fillAmount = _health.CurrentHp / _health.MaxHp;
  }
}