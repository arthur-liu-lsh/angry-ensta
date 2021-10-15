using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSounds : MonoBehaviour
{
    public AudioClip launchSound;
    public AudioClip bounceSound;

    public float minimalBounceSoundSpeed;



    public void PlayLaunchSound() {
        AudioSource.PlayClipAtPoint(launchSound, this.transform.position);
    }

    void PlayBounceSound() {
        AudioSource.PlayClipAtPoint(bounceSound, this.transform.position);
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Ground" && col.relativeVelocity.magnitude > minimalBounceSoundSpeed) {
            PlayBounceSound();
        }
    }
}
