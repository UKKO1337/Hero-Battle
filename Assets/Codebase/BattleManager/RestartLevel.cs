using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Codebase.BattleManager
{
  public class RestartLevel : MonoBehaviour
  {
    [SerializeField] private Button _restart;

    private void Start() => 
      _restart.onClick.AddListener(Restart);

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.R)) 
        Restart();
    }

    private static void Restart() => 
      SceneManager.LoadScene("Main");
  }
}