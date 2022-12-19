using Codebase.BattleManager;
using Codebase.Logic;
using UnityEngine;

namespace Codebase.Player
{
  public class PlayerHeal : MonoBehaviour
  {
    [SerializeField] private UnitAnimator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _healSound;

    private PlayerTurn _playerTurn;

    private void Start() => 
      _playerTurn = FindObjectOfType<PlayerTurn>();


    public void MakeAction(Transform target)
    {
      _animator.PlayHeal();
      _audioSource.clip = _healSound;
      _audioSource.Play();
      target.GetComponent<Health>().Heal();
      _playerTurn.ChangeActiveUnit();
    }
  }
}