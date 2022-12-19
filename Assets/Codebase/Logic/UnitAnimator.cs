using System;
using UnityEngine;

namespace Codebase.Logic
{
  public class UnitAnimator : MonoBehaviour
  {
    public event Action AttackHappened;
    public event Action AttackEnded;
    
    private static readonly int Die = Animator.StringToHash("Death");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Heal = Animator.StringToHash("Heal");
    private static readonly int Hit = Animator.StringToHash("Hit");

    private Animator _animator;
    
    private void Awake() => 
      _animator = GetComponent<Animator>();
    
    public void PlayDeath() => 
      _animator.SetTrigger(Die);

    public void PlayHit() => 
      _animator.SetTrigger(Hit);

    public void Move() => 
      _animator.SetBool(IsMoving, true);

    public void StopMoving() => 
      _animator.SetBool(IsMoving, false);
    
    public void PlayAttack() => 
      _animator.SetTrigger(Attack);

    public void PlayHeal() => 
      _animator.SetTrigger(Heal);


    private void OnAttack() => 
      AttackHappened?.Invoke();

    private void OnAttackEnd() => 
      AttackEnded?.Invoke();
    
  }
}
