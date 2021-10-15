using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] birdArray;
    public GameObject[] iconArray;

    private int enemyCount;
    private int birdCount;

    public float endingDelay;
    private bool levelEnding;

    private LevelCanvas levelCanvas;
    private int birdsShot = 0;


    public void EnemyCountDecrement() { // Called by enemies on destroy
        enemyCount -= 1;
        if (enemyCount <= 0) { // All enemies are destroyed
            EndLevel();
        }
    }

    public void BirdCountDecrement() { // Called by birds on destroy
        birdCount -= 1;
        if (birdCount <= 0) { // All birds are destroyed
            EndLevel();
        }
    }
    
    public void BirdCountIncrement() { // Called by birds on destroy
        birdCount += 1;
        if (birdCount <= 0) { // All birds are destroyed
            EndLevel();
        }
    }

    public void HideBirdInBar() {
        levelCanvas.HideBirdInBar(birdsShot);
        birdsShot++;
    }



    void EndLevel() {
        if (!levelEnding) {
            StartCoroutine(WaitForLevelEnding());
        }
    }

    void CallVictoryScreen() {
        levelCanvas.ShowVictoryMenu();
    }

    void CallDefeatScreen() {
        levelCanvas.ShowDefeatMenu();
    }

    IEnumerator WaitForLevelEnding()
	{
		levelEnding = true;
		yield return new WaitForSeconds(endingDelay);

        if (enemyCount <= 0) { // Victory (priority over defeat)
            CallVictoryScreen();
        }
        else if (birdCount <= 0) { // Defeat
            CallDefeatScreen();
        }
    }



    void Start()
    {
        levelCanvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<LevelCanvas>();


        Rigidbody2D hook = GameObject.FindGameObjectWithTag("Hook").GetComponent<Rigidbody2D>();

        GameObject[] birdInstanceArray = new GameObject[birdArray.Length];

        // Count birds
        birdCount = birdArray.Length;

        // Instantiate birds and connect them to the slingshot hook
        // Set all birds as inactive
        for (int i = 0; i < birdArray.Length; i++) {
            birdInstanceArray[i] = Instantiate(birdArray[i], hook.transform.position, hook.transform.rotation);
            birdInstanceArray[i].GetComponent<SpringJoint2D>().connectedBody = hook;
            birdInstanceArray[i].SetActive(false);
        }

        // Set nextBird to activate following birds after launch
        for (int i = 0; i < birdInstanceArray.Length - 1; i++) {
            birdInstanceArray[i].GetComponent<BirdSlingshot>().nextBird = birdInstanceArray[i+1];
        }

        // Set first bird as active and as target of main camera
        if (birdInstanceArray.Length > 0) {
            birdInstanceArray[0].SetActive(true);
            Camera.main.GetComponent<CameraFollow>().target = birdInstanceArray[0].transform;
        }

        // Count enemies
        GameObject[] enemyInstanceArray = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemyInstanceArray.Length;

        levelCanvas.InitBirdsBar(iconArray);
    }


    void Update()
    {

    }
}
