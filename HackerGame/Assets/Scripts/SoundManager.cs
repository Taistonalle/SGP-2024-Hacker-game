using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    AudioSource clickSource;
    AudioSource clipSource;

    [Header("Audio clips for events")]
    [SerializeField] AudioClip taskWrong;
    [SerializeField] AudioClip taskCorrect;
    [SerializeField] AudioClip folderUnhacked;
    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioClip powerOff;

    private readonly List<AudioClip> audioClips = new();

    void Start() {
        clickSource = FindObjectOfType<GameManager>().GetComponent<AudioSource>();
        clipSource = GetComponent<AudioSource>();

        audioClips.Add(taskWrong);
        audioClips.Add(taskCorrect);
        audioClips.Add(folderUnhacked);
        audioClips.Add(clickSound);
        audioClips.Add(powerOff);
    }


    public void PlayOneShot(int listIndex, bool pitch) {
        float pitchValue = Random.Range(0.9f, 1.1f);

        switch (pitch) {
            case true:
            clipSource.pitch = pitchValue;
            break;
            default:
            clipSource.pitch = 1f;
            break;
        }

        clipSource.PlayOneShot(audioClips[listIndex]);
    }

    public void PlayClickSound(float volume) {
        float pitchValue = Random.Range(0.9f, 1.1f);
        clickSource.pitch = pitchValue;

        clickSource.PlayOneShot(clickSound, volume);
    }
}
