using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource gameAudio;

    [SerializeField] private AudioClip moneyEarned;
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private AudioClip cantBuy;

    private void Start()
    {
        gameAudio = GetComponent<AudioSource>();
    }

    public void PlayMoneySound()
    {
        PlaySound(moneyEarned, 1.5f);
    }

    public void PlayButtonSound()
    {
        PlaySound(buttonSound, 1f);
    }

    public void PlayCantBuySound()
    {
        PlaySound(cantBuy, 1f);
    }

    private void PlaySound(AudioClip sound, float volume)
    {
        gameAudio.PlayOneShot(sound, volume);
    }
}
