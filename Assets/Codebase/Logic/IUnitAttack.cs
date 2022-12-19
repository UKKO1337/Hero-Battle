using System;
using UnityEngine;

namespace Codebase.Logic
{
  public interface IUnitAttack
  {
    event Action ActionHappend;
    event Action ActionEnded;
    void GoToTarget(Transform target);
    void GoToStartPosition();
    void DoDamage();
  }
}