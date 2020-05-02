using UnityEngine;

namespace FungusLibrary
{
    public class AudioManager : MonoBehaviour
    {
        [HideInInspector]
        public AudioSource Source;
        public AudioClip[] Sounds;

        private void Start()
        {
            Source = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Метод воспроизведения звука
        /// </summary>
        /// <param name="clip"></param>
        public void PlayAudio(AudioClip clip)
        {
            GetComponent<AudioSource>().PlayOneShot(clip);
        }
    }
}
