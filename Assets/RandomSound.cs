using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{
    [SerializeField] AudioClip[] spawnSounds;
    [SerializeField] AudioClip[] deathSounds;
    
    AudioSource myAudioSource;

    private bool _isDeathSoundPlayed = false;
    private bool _isSpawnSoundPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
      myAudioSource = GetComponent<AudioSource>();
      SpawnSound();
    }

    // Update is called once per frame
    void Update()
    {
        Enemy enemy = GetComponent<Enemy>();

        if (enemy.IsDead)
        { 
            DeathSound();
        }

    }

    void SpawnSound()
    {
        if (_isSpawnSoundPlayer)
        {
            return;
        }

        AudioClip clip = spawnSounds[UnityEngine.Random.Range(0, spawnSounds.Length-1)];
        myAudioSource.PlayOneShot(clip);
        _isSpawnSoundPlayer = true;
    }

    void DeathSound()
    {
        if (_isDeathSoundPlayed)
        {
            return;
        }
        AudioClip clip = deathSounds[UnityEngine.Random.Range(0, deathSounds.Length-1)];
        myAudioSource.PlayOneShot(clip);
        _isDeathSoundPlayed = true;
    }
}
