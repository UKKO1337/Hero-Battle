using UnityEngine;
using UnityEngine.UI;

namespace Codebase.UI
{
  public class Exit : MonoBehaviour
  {
    public void ExitGame()
    {
      Debug.Log("Game quit");
      Application.Quit();
    }
  }
}