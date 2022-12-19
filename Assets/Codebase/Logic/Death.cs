using System;
using System.Collections;
using UnityEngine;

namespace Codebase.Logic
{
  public class Death : MonoBehaviour
  {
    [SerializeField] private Health _health;
    [SerializeField] private UnitAnimator _animator;

    public event Action DeathHappend;
    public bool IsDead { get; private set; }


    private void Start() => 
      _health.HealthChanged += HealthChanged;


    private void OnDestroy()
    {
      _health.HealthChanged -= HealthChanged;
      DeathHappend?.Invoke();
    }

    private void HealthChanged()
    {
      if (_health.CurrentHp <= 0) 
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
      IsDead = true;
      _animator.PlayDeath();
      yield return new WaitForSeconds(2);
      Destroy(gameObject);
    }
    
  }
}