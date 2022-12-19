using Codebase.Player;
using UnityEngine;

namespace Codebase.Actions
{
  public class ActionPoison : MonoBehaviour, IActionAttack
  {
    private GameObject _player;

    private void Start() => 
      _player = transform.parent.gameObject;

    public void MakeAction(Transform target) => 
      _player.GetComponent<PlayerPoison>().MakeAction(target);
  }
}