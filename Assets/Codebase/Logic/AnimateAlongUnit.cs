using UnityEngine;

namespace Codebase.Logic
{
  public class AnimateAlongUnit : MonoBehaviour
  {
    public UnitAnimator Animator;
    
    private IUnitAttack _attack;
    private Vector3 _startPosition;

    private void Start() => 
      _startPosition = transform.position;

    private void Update()
    {
      if (transform.position != _startPosition)
        Move();

      else
        Stop();
    }


    private void Move()
    {
      _startPosition = transform.position;
      Animator.Move();
    }

    private void Stop() => 
      Animator.StopMoving();
    
  }
}