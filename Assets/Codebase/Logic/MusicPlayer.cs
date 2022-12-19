using UnityEngine;
using Random = UnityEngine.Random;

namespace Codebase.Logic
{
  public class MusicPlayer : MonoBehaviour
  {
    [SerializeField] private AudioClip[] _audioClips;
    [SerializeField] private AudioSource _audioSource;
    
    private void Start() => 
      GetRandomMusic();

    private void Update()
    {
      if (!_audioSource.isPlaying) 
        GetRandomMusic();
    }

    private void GetRandomMusic()
    {
      _audioSource.clip = _audioClips[Random.Range(0, _audioClips.Length)];
      _audioSource.Play();
    }
  }
}
