using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public MainManager Manager;
    public AudioClip deathSound;

    private void OnCollisionEnter(Collision other)
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Destroy(other.gameObject);
        Manager.GameOver();
    }
}
