using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BirdAbility : MonoBehaviour
{
    public float collisionDisableDelay;

    private LevelCanvas escMenu;
    private BirdParticles birdParticles;


    public virtual void Ability() {}

    // Disables ability after a certain time after collision
    void OnCollisionEnter2D(Collision2D col) {
        if (this.enabled) {
            StartCoroutine(Disable());
        }
    }

    IEnumerator Disable()
	{
		yield return new WaitForSeconds(collisionDisableDelay);
		this.enabled = false;
	
	}



    void Start()
    {
        escMenu = GameObject.FindGameObjectWithTag("Canvas").GetComponent<LevelCanvas>();
        birdParticles = GetComponent<BirdParticles>();

    }

	void Update()
	{
        if (Input.GetMouseButtonDown(0) && escMenu != null && !escMenu.isPaused) { // Caution won't activate if there's no escMenu
            if (EventSystem.current.IsPointerOverGameObject()) {
                return;
            }
            if (birdParticles != null) {
                birdParticles.EmitPowerPuff();
            } 
            Ability();
            this.enabled = false;
        }
	}
}
