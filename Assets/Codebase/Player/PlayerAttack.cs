using System;
using System.Collections;
using Codebase.BattleManager;
using Codebase.Logic;
using DG.Tweening;
using UnityEngine;

namespace Codebase.Player
{
  public class PlayerAttack : MonoBehaviour, IUnitAttack
  {
    [SerializeField] private UnitAnimator _animator;
    [SerializeField] private int _damage;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _attackSound;

    public event Action ActionHappend;
    public event Action ActionEnded;
    
    private Vector3 _startPosition;
    private PlayerTurn _playerTurn;
    
    private void Start()
    {
      _startPosition = transform.position;
      _playerTurn = FindObjectOfType<PlayerTurn>();
    }


    public void MakeAction(Transform target) => 
      StartCoroutine(ShowAttack(target));

    private IEnumerator ShowAttack(Transform target)
    {
      _animator.AttackHappened += DoDamage;
      _animator.AttackEnded += GoToStartPosition;
      GoToTarget(target);
      yield return new WaitForSeconds(1);
      _animator.PlayAttack();
    }

    public void GoToStartPosition()
    {
      transform.DOMove(_startPosition, 1);
      _playerTurn.ChangeActiveUnit();
      ActionEnded?.Invoke();
      _animator.AttackHappened -= DoDamage;
      _animator.AttackEnded -= GoToStartPosition;
    }

    public void GoToTarget(Transform target)
    {
      ActionHappend?.Invoke();
      Vector3 positionToStayOffSet = target.forward + new Vector3(1.5f, 0, 0);
      Vector3 positionToStay = target.position + positionToStayOffSet;
      transform.DOMove(positionToStay, 1);
    }

    public void DoDamage()
    {
      _audioSource.clip = _attackSound;
      _audioSource.Play();
      
      if (Physics.Raycast(transform.position + Vector3.up,Vector3.left, out RaycastHit hit, 5))
      {
        var health = hit.collider.GetComponentInParent<Health>();
        
        if (health != null) 
          health.TakeDamage(_damage);
      }
    }
  }
}