using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Music")]
    [SerializeField] private List<AudioClip> allMusic;
    private List<AudioClip> randomAllMusic = new List<AudioClip>();

    [Header("Audio Sources")]
    [SerializeField] private AudioSource audioSourceMusic;
    [SerializeField] private AudioSource audioSourceSoundEffectsA;
    [SerializeField] private AudioSource audioSourceSoundEffectsB;
    private Coroutine running;
    private bool isMusicMuted = false;
    private bool isSoundEffectsMuted = false;
    private int currentMusic = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        audioSourceMusic = GetComponent<AudioSource>();
        RandomizeAllMusicAtStart();
    }

    private void Update()
    {
        if (running == null)
        {
            running = StartCoroutine(PlayMusicTracks());
        }
    }

    public void PlaySoundEffect(AudioClip _soundEffect)
    {
        if (!isSoundEffectsMuted)
        {
            if (audioSourceSoundEffectsA.isPlaying)
            {
                audioSourceSoundEffectsB.clip = _soundEffect;
                audioSourceSoundEffectsB.Play();
            }
            else
            {
                audioSourceSoundEffectsA.clip = _soundEffect;
                audioSourceSoundEffectsA.Play();
            }
        }
    }

    private IEnumerator PlayMusicTracks()
    {
        if (!isMusicMuted)
        {
            if (randomAllMusic.Count - 1 < currentMusic )
            {
                currentMusic = 0;
            }

            yield return PlayMusic();

            currentMusic++;
            
        }  
    }

    private IEnumerator PlayMusic()
    {
        audioSourceMusic.clip = randomAllMusic[currentMusic];
        audioSourceMusic.Play();

        while (audioSourceMusic.isPlaying)
        {
            yield return null;
        }

        running = null;
    }



    private void RandomizeAllMusicAtStart()
    {
        List<AudioClip> localAllMusic = new List<AudioClip>(allMusic);
        while (localAllMusic.Count > 0)
        {
            int randomMusicIndex = Random.Range(0, localAllMusic.Count - 1);
            randomAllMusic.Add(localAllMusic[randomMusicIndex]);
            localAllMusic.RemoveAt(randomMusicIndex);
        }
    }
}
