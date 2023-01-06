using UnityEngine;

namespace Erntemaschine.Sounds
{
    [RequireComponent(typeof(AudioSource))]
    internal class RandomSoundPlayer : MonoBehaviour
    {
        [SerializeField] private SoundCollection _collection;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Play()
        {
            _audioSource.Stop();
            _audioSource.clip = _collection.Clips[Random.Range(0, _collection.Clips.Length)];
            _audioSource.Play();
        }
    }
}
