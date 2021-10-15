using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Divide : BirdAbility
{
    public GameObject smallBoschetto;
    

    
    public override void Ability() {
        //Increment bird count 3 time because 3 birds are spawned
        GameObject manager = GameObject.FindGameObjectWithTag("GameManager");
        
        if (manager != null) {
            manager.GetComponent<GameManager>().BirdCountIncrement();
            manager.GetComponent<GameManager>().BirdCountIncrement();
            manager.GetComponent<GameManager>().BirdCountIncrement();
        }

        GameObject[] spawnlings = new GameObject[3];

        // Get normalized speed direction and normal vectors
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        Vector2 normalizedVelocity = velocity;
        normalizedVelocity.Normalize();

        Vector2 normal = Vector2.Perpendicular(velocity);
        normal.Normalize();

        // Instantiate small birds
        spawnlings[0] = Instantiate(smallBoschetto, (Vector2)this.transform.position - .32f * normal + .19f * normalizedVelocity, this.transform.rotation);
        spawnlings[1] = Instantiate(smallBoschetto, (Vector2)this.transform.position - .37f * normalizedVelocity, this.transform.rotation);
        spawnlings[2] = Instantiate(smallBoschetto, (Vector2)this.transform.position + .32f * normal + .19f * normalizedVelocity, this.transform.rotation);

        // Set small birds speeds
        spawnlings[0].GetComponent<Rigidbody2D>().velocity = velocity - .75f * normal;
        spawnlings[1].GetComponent<Rigidbody2D>().velocity = velocity;
        spawnlings[2].GetComponent<Rigidbody2D>().velocity = velocity + .75f * normal;

        // Enable bird properties
        for (int i = 0; i < 3; i++) {
            spawnlings[i].GetComponent<SelfDestruct>().enabled = true;
            spawnlings[i].GetComponent<BirdParticles>().enabled = true;
        }

        // Set next bird to middle spawnling
        spawnlings[1].GetComponent<BirdSlingshot>().nextBird = this.GetComponent<BirdSlingshot>().nextBird;
        spawnlings[1].GetComponent<BirdSlingshot>().CallRelease();
        this.GetComponent<BirdSlingshot>().nextBird = null;
        
        // Set camera target to middle spawnling
        if (Camera.main != null) {
            CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
            cam.target = spawnlings[1].transform;
        }

        Destroy(this.gameObject);
    }
}
