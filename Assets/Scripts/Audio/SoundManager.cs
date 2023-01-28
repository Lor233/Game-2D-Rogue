using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource audioBac;
    public AudioClip battle, pause, gameOver, win;
    public AudioSource audioSrc;
    public AudioClip pickCoin, swingSword, hitSword, killSword, hitWood, brokenWood, beAttack;

    private void Awake() {
        instance = this;
    }

    public void PlayBattle()
    {
        audioBac.clip = battle;
        audioBac.Play();
    }

    public void PlayPause()
    {
        audioBac.clip = pause;
        audioBac.Play();
    }

    public void PlayGameOver()
    {
        audioBac.clip = gameOver;
        audioBac.Play();
    }

    public void PlayWin()
    {
        audioBac.clip = win;
        audioBac.Play();
    }

    public void PlayPickCoin()
    {
        audioSrc.PlayOneShot(pickCoin);
    }

    public void PlaySwingSword()
    {
        audioSrc.PlayOneShot(swingSword);
    }

    public void PlayHitSword()
    {
        audioSrc.PlayOneShot(hitSword);
    }

    public void PlayKillSword()
    {
        audioSrc.PlayOneShot(killSword);
    }

    public void PlayHitWood()
    {
        audioSrc.PlayOneShot(hitWood);
    }

    public void PlayBrokenWood()
    {
        audioSrc.PlayOneShot(brokenWood);
    }

    public void PlayBeAttack()
    {
        audioSrc.PlayOneShot(beAttack);
    }
}
