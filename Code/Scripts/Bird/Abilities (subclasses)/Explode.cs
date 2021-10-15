using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : BirdAbility
{
    public float explosionForce;
    public float explosionRange;

    public LayerMask layerToHit;

    public GameObject explosionParticles;
    public AudioClip explosionSound;
    


    public override void Ability() {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Get damageable objects in radius
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, explosionRange, layerToHit);
        
        foreach (Collider2D col in objects) {
            Vector2 direction = col.transform.position - transform.position;
            float distance = direction.magnitude;
            direction.Normalize();

            // Generate force and deal damage to all objects according to distance
            col.GetComponent<Rigidbody2D>().AddForce(direction * explosionForce / distance);
            col.GetComponent<Damage>().ReceiveDamage(explosionForce / distance);
        }

        Instantiate(explosionParticles, this.transform.position, this.transform.rotation);
        AudioSource.PlayClipAtPoint(explosionSound, this.gameObject.transform.position);
        Camera.main.GetComponent<CameraFollow>().ScreenShake();

        Destroy(this.gameObject);
    }
}
