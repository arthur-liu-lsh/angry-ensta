using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBirdDestroy : MonoBehaviour
{
    void OnDestroy()
    {
        // Call on GameManager to decrement enemy count
        GameObject manager = GameObject.FindGameObjectWithTag("GameManager");
        
        if (manager != null) {
            manager.GetComponent<GameManager>().BirdCountDecrement();
        }
    }
}
