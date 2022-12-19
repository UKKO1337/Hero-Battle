using UnityEngine;
using Random = UnityEngine.Random;

namespace Codebase.Player
{
  public class ActionSpawner : MonoBehaviour
  {
    [SerializeField] private GameObject[] _actions;

    private GameObject _currentAction;
    
    public void SpawnNewAction()
    {
      _currentAction = Instantiate(_actions[Random.Range(0, _actions.Length)], gameObject.transform.position + new Vector3(0, 1, -1.5f),
        Quaternion.identity);
      
      _currentAction.transform.SetParent(gameObject.transform);
    }
  }
}
