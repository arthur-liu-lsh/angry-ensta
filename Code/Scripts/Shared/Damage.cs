using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float health;
    public float damageThreshold;

    public Sprite[] damagedSprites;

    public AudioClip destroySound;
    public GameObject destroyParticles;

    private Rigidbody2D rb;

    private float maxHealth;
    private SpriteRenderer spriteRenderer;


    float KineticEnergy(float m, float v) {
        return .5f*m*v*v;
    }

    bool CheckDestroy() {
        if (health < 0) {
            return true;
        }
        return false;
    }

    void CheckAppearance() {
        int index = -1;
        for (int i = damagedSprites.Length; i > 0; i--) {
            if (health < i * maxHealth / (damagedSprites.Length + 1)) {
                index = i - 1;
            }
        }
        if (index != -1) {
            spriteRenderer.sprite = damagedSprites[index];
        }
    }

    public void ReceiveDamage(float damage) {
        this.health -= damage;
        CheckAppearance();
        if (CheckDestroy()) { // Object is destroyed, generate destroy sound and particles
            AudioSource.PlayClipAtPoint(destroySound, this.gameObject.transform.position);
            if (destroyParticles != null) {
                Instantiate(destroyParticles, this.transform.position, this.transform.rotation);
            }
            Destroy(gameObject);
        }
    }



    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground") {
            float energy = KineticEnergy(rb.mass, col.relativeVelocity.magnitude);
            if (energy > damageThreshold) {
                ReceiveDamage(energy);
            }
        }
        else {
            Rigidbody2D otherRb = col.gameObject.GetComponent<Rigidbody2D>();
            float energy = KineticEnergy(otherRb.mass, col.relativeVelocity.magnitude);
            if (energy > damageThreshold) {
                ReceiveDamage(energy);
            }
        }
    }



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        maxHealth = health;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
