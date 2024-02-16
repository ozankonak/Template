using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour   
    {
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void Play2DSound(AudioClip clip,bool randomPitch = false)
        {
            audioSource.spatialBlend = 0;

            audioSource.pitch = randomPitch ? Random.Range(0.75f, 1.25f) : 1f;
            
            audioSource.PlayOneShot(clip);
        }
    }
}
