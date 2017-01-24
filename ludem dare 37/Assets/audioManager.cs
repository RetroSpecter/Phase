using UnityEngine;
using System.Collections;

public class audioManager : MonoBehaviour {

    private AudioSource source;
    public AudioClip soundFX;
    public static audioManager instance;

    void Start() {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void Play(AudioClip sound) {
        source.clip = sound;
        source.volume = 0.5f;
        source.PlayOneShot(sound);
    }

    public void Play(AudioClip sound, float volume) {
        source.clip = sound;
        source.volume = volume;
        source.PlayOneShot(sound);
    }
}
