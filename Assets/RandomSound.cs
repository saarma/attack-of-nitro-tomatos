using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{
    [SerializeField] AudioClip[] spawnSounds;
    [SerializeField] AudioClip[] deathSounds;
    
    AudioSource myAudioSource;

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
        if (spawnSounds.Length == 0)
        {

        }

        AudioClip clip = spawnSounds[UnityEngine.Random.Range(0, spawnSounds.Length-1)];
        myAudioSource.PlayOneShot(clip);
    }

    void DeathSound()
    {
        AudioClip clip = deathSounds[UnityEngine.Random.Range(0, deathSounds.Length-1)];
        myAudioSource.PlayOneShot(clip);
    }
}
