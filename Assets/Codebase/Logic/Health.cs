using System;
using UnityEngine;

namespace Codebase.Logic
{
  public class Health : MonoBehaviour
  {
    [SerializeField] private UnitAnimator _animator;
    [SerializeField] private PoisonStatus _poisonStatus;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _oofSound;

    public float MaxHp = 30;
    public float CurrentHp { get; private set; }
    
    public event Action HealthChanged;
    public event Action Poisoned;
  
    private void Start() => 
      CurrentHp = MaxHp;


    public void TakeDamage(int damage)
    {
      PlayOof();
      _animator.PlayHit();
      CurrentHp -= damage;
      HealthChanged?.Invoke();
    }

    private void PlayOof()
    {
      _audioSource.clip = _oofSound;
      _audioSource.Play();
    }

    public void TakePoisonEffect()
    {
      PlayOof();
      _animator.PlayHit();
      CurrentHp -= 1;
      HealthChanged?.Invoke();
      Poisoned?.Invoke();
    }

    public void TakePoisonDamage()
    {
      PlayOof();
      CurrentHp -= 1;
      _animator.PlayHit();
      HealthChanged?.Invoke();
    }

    public void Heal()
    {
      if (CurrentHp < MaxHp)
      {
        CurrentHp += 1;
        HealthChanged?.Invoke();
        _poisonStatus.GetHealedFromPoison();
      }
    }
  }
}