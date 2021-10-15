using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerate : BirdAbility
{
    public float speedMultiplier;
    

    
    public override void Ability() {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = rb.velocity * speedMultiplier;
    }
}
