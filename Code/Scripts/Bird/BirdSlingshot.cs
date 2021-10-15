using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSlingshot : MonoBehaviour
{
	private Rigidbody2D rb;
	private Rigidbody2D hook;

	public float releaseTime = .15f;
	public float maxDragDistance = 2f;

	public GameObject nextBird;

	private GameManager gameManager;
	private LevelCanvas escMenu; 
	private BirdSounds birdSounds;

	private bool isPressed = false;
	private bool switched = false;
	private bool shot = false;



	// Activates the next bird and sets the main camera target on next bird
	void FollowNext() {
		if (nextBird != null)
		{
			nextBird.SetActive(true);
			if (Camera.main != null) {
            	CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
            	cam.target = nextBird.transform;
			}
		}
	}



    void Start()
    {
		gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		escMenu = GameObject.FindGameObjectWithTag("Canvas").GetComponent<LevelCanvas>();
        rb = GetComponent<Rigidbody2D>();
		hook = GameObject.FindGameObjectWithTag("Hook").GetComponent<Rigidbody2D>();
		birdSounds = GetComponent<BirdSounds>();
    }

	void Update ()
	{
		if (isPressed)
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

			if (Vector3.Distance(mousePos2D, hook.position) > maxDragDistance)
				rb.position = hook.position + (mousePos2D - hook.position).normalized * maxDragDistance;
			else
				rb.position = mousePos;
		}
	}



	void OnMouseDown ()
	{
		if (escMenu != null && !escMenu.isPaused && !shot) {
			isPressed = true;
			rb.isKinematic = true;
		}
	}

	void OnMouseUp ()
	{
		if (escMenu != null && !escMenu.isPaused && !shot) {
			shot = true;
			isPressed = false;
			rb.isKinematic = false;

			StartCoroutine(Release());
		}
	}

	public void CallRelease() {
		// Activate bird behaviours
        GetComponent<BirdParticles>().enabled = true;
        GetComponent<SelfDestruct>().enabled = true;
		GetComponent<SpringJoint2D>().enabled = false;

		// Disable slingshot script
		this.enabled = false;

		StartCoroutine(SwitchBird());

	}

	IEnumerator Release()
	{
		birdSounds.PlayLaunchSound();
		gameManager.HideBirdInBar();

		// Activate bird behaviours
        GetComponent<BirdParticles>().enabled = true;

		yield return new WaitForSeconds(releaseTime);

        GetComponent<SelfDestruct>().enabled = true;
		GetComponent<SpringJoint2D>().enabled = false;

		BirdAbility ability = GetComponent<BirdAbility>();
		if (ability != null) {
			ability.enabled = true;
		}

		// Disable slingshot script
		this.enabled = false;

		yield return new WaitForSeconds(3f);

		// Switch to next bird
		FollowNext();
		switched = true;
	
	}

	IEnumerator SwitchBird() {
		yield return new WaitForSeconds(3f);
		FollowNext();
		switched = true;
	}



	void OnDestroy()
	{
		if (!switched) {
			FollowNext();
		}
	}
}
