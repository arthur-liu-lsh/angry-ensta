using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdParticles : MonoBehaviour
{
    public GameObject trailParticle;
    public float puffDelay;

    public GameObject powerParticle;

    public GameObject collisionParticle;
    public float minimalBounceParticleSpeed;

    public float collisionDisableDelay;

    private float timer;


    // Disables particle emission after a certain time after collision
    void OnCollisionEnter2D(Collision2D col) {
        if (this.enabled) {
            StartCoroutine(Disable());
        }
        if (col.gameObject.tag == "Ground" && col.relativeVelocity.magnitude > minimalBounceParticleSpeed) {
            Vector2 contactPoint = col.GetContact(0).point;
            Instantiate(collisionParticle, contactPoint, transform.rotation);
        }
    }

    IEnumerator Disable()
	{
		yield return new WaitForSeconds(collisionDisableDelay);
		this.enabled = false;
	
	}


    // Emits a particle at bird position
    void EmitPuff() {
        Instantiate(trailParticle, transform.position, transform.rotation);
    }

    public void EmitPowerPuff() {
        Instantiate(powerParticle, transform.position, transform.rotation);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= puffDelay) {
            timer = 0;
            EmitPuff();
        }
    }
}
