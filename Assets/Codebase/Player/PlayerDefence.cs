using Codebase.BattleManager;
using Codebase.Logic;
using UnityEngine;

namespace Codebase.Player
{
  public class PlayerDefence : MonoBehaviour
  {
    [SerializeField] private UnitAnimator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _defenceSound;

    private PlayerTurn _playerTurn;

    private void Start() => 
      _playerTurn = FindObjectOfType<PlayerTurn>();
    
    public void MakeAction(Transform target)
    {
      _animator.PlayHeal();
      _audioSource.clip = _defenceSound;
      _audioSource.Play();
      target.GetComponentInChildren<DefenceStatus>().MakeAction();
      _playerTurn.ChangeActiveUnit();
    }
  }
}