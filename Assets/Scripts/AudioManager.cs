using UnityEngine;
using System.Collections.Generic;

namespace AudioManager
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [SerializeField] private AudioSource musicAudioSource;
        [SerializeField] private AudioSource sfxAudioSource;
        [SerializeField] private GameObject audioSourcePrefab; // Prefab with an AudioSource component
        private List<AudioSource> sfxAudioSources = new List<AudioSource>();
        private Queue<AudioSource> availableSfxSources = new Queue<AudioSource>();

        [Header("Pool Settings")]
        [SerializeField] private int initialPoolSize = 3; // Number of SFX sources to preallocate

        [Header("Music Clips")]
        [SerializeField] private AudioClip Tempo1MusicClip;
        [SerializeField] private AudioClip Tempo2MusicClip;
        [SerializeField] private AudioClip Tempo3MusicClip;


        [Header("SFX Clips")]
        [SerializeField] private AudioClip DeathSound;

        protected void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.Log("Destroying Duplicate AudioManager.", gameObject);
                Destroy(this);
                return;
            }

            InitializeAudioSourcePool();
        }

        private void InitializeAudioSourcePool()
        {
            for (int i = 0; i < initialPoolSize; i++)
            {
                CreateNewAudioSource();
            }
        }

        private void CreateNewAudioSource()
        {
            GameObject audioSourceObj = Instantiate(audioSourcePrefab, Instance.transform);
            audioSourceObj.SetActive(true);
            AudioSource audioSource = audioSourceObj.GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
            sfxAudioSources.Add(audioSource);
            availableSfxSources.Enqueue(audioSource);
        }

        private AudioSource GetAvailableAudioSource()
        {
            if (availableSfxSources.Count == 0)
            {
                CreateNewAudioSource();
            }

            AudioSource source = availableSfxSources.Dequeue();
            return source;
        }

        private void ReturnAudioSourceToPool(AudioSource source)
        {
            source.Stop();
            source.clip = null;
            availableSfxSources.Enqueue(source);
        }

        public void PlaySfx(AudioClip clip)
        {
            foreach (var source in sfxAudioSources)
            {
                if (source.clip == clip) return;
            }
        }

        public void StopSfx(AudioClip clip)
        {
            foreach (var source in sfxAudioSources)
            {
                if (source.clip == clip)
                {
                    source.Stop();
                }
            }
        }

        private System.Collections.IEnumerator ReturnToPoolAfterPlayback(AudioSource source)
        {
            yield return new WaitUntil(() => !source.isPlaying);
            ReturnAudioSourceToPool(source);
        }

        public void StopAllSfx()
        {
            foreach (var source in sfxAudioSources)
            {
                source.Stop();
            }
        }

        public void PlayMusic(AudioClip clip)
        {
            if (musicAudioSource.clip == clip) return;
            musicAudioSource.clip = clip;
            musicAudioSource.Play();
        }

        public void StopMusic()
        {
            musicAudioSource.Stop();
        }

        public void StopClearMusic()
        {
            musicAudioSource.Stop();
            musicAudioSource.clip = null;
        }

        public void SetMusicVolume(float volume)
        {
            musicAudioSource.volume = volume;
        }

        public void SetSFXVolume(float volume)
        {
            foreach (AudioSource source in sfxAudioSources)
            {
                source.volume = volume;
            }
        }

        #region Music Sounds
        public void PlayTempo1_Music() => PlayMusic(Tempo1MusicClip);
        public void PlayTempo2_Music() => PlayMusic(Tempo2MusicClip);
        public void PlayTempo3_Music() => PlayMusic(Tempo3MusicClip);
        #endregion

        #region SFX Sounds
        public void PlayDeath_SFX() => PlaySfx(DeathSound);

        #endregion
    }
}
