using System;
using System.Collections;
using Codebase.BattleManager;
using Codebase.Logic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Codebase.Enemy
{
  public class EnemyAttack : MonoBehaviour, IUnitAttack
  {
    [SerializeField] private UnitAnimator _animator;
    [SerializeField] private int _damage;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _attackSound;

    public event Action ActionHappend;
    public event Action ActionEnded;
    
    private PlayerTurn _playerTurn;
    private GameObject _currentPlayerUnit;

    private Vector3 _startPosition;


    private void Start()
    {
      _startPosition = transform.position;
      _playerTurn = FindObjectOfType<PlayerTurn>();
      _animator.AttackHappened += DoDamage;
      _animator.AttackEnded += GoToStartPosition;
    }

    public void MakeAction()
    {
      for(var i = _playerTurn.PlayerUnits.Count - 1; i > -1; i--)
      {
        if (_playerTurn.PlayerUnits[i].GetComponent<Death>().IsDead)
          _playerTurn.PlayerUnits.RemoveAt(i);
      }
      
      var randomUnit = GetRandomPlayerUnit();
      _currentPlayerUnit = _playerTurn.PlayerUnits[randomUnit];

      Transform target = _currentPlayerUnit.transform;
      
      StartCoroutine(ShowAttack(target));

    }

    private int GetRandomPlayerUnit() => 
      Random.Range(0, _playerTurn.PlayerUnits.Count);

    private IEnumerator ShowAttack(Transform target)
    {
      GoToTarget(target);
      yield return new WaitForSeconds(1);
      _animator.PlayAttack();
    }

    public void GoToTarget(Transform target)
    {
      ActionHappend?.Invoke();
      Vector3 positionToStayOffSet = target.forward + new Vector3(-1.5f, 0, 0);
      Vector3 positionToStay = target.position + positionToStayOffSet;
      transform.DOMove(positionToStay, 1);
    }

    public void GoToStartPosition()
    {
      transform.DOMove(_startPosition, 1);
      ActionEnded?.Invoke();
    }

    public void DoDamage()
    {
      _audioSource.clip = _attackSound;
      _audioSource.Play();
      
      if (Physics.Raycast(transform.position + new Vector3(0, 1,0 ), Vector3.right, out RaycastHit hit, 5))
      {
        if (hit.collider.GetComponentInChildren<DefenceStatus>().IsUnderDef)
          hit.collider.GetComponentInChildren<DefenceStatus>().TakeDamage(_damage);

        else
        {
          var health = hit.collider.GetComponent<Health>();
        
          if (health != null) 
            health.TakeDamage(_damage);
        }
        
        
      }
      
    }
    
  }
}