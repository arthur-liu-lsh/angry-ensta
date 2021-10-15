using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCanvas : MonoBehaviour
{
    public int page;
    public int level;
    
    public bool isPaused = false;

    private GameObject escMenu;
    private GameObject helpMenu;
    private GameObject defeatMenu;
    private GameObject victoryMenu;

    public GameObject remainingBirdsBar;
    private GameObject[] remainingBirdIcons;

    public void InitBirdsBar(GameObject[] iconArray) {
        remainingBirdIcons = new GameObject[iconArray.Length];
        for (int i = 0; i < iconArray.Length; i++) {
            remainingBirdIcons[i] = Instantiate(iconArray[i], remainingBirdsBar.transform.position + new Vector3(-i*30,0,0), Quaternion.identity, remainingBirdsBar.transform);
        }
    }

    public void HideBirdInBar(int n) {
        if (0 <= n && n < remainingBirdIcons.Length) {
            remainingBirdIcons[n].SetActive(false);
        }
    }

    public void Pause() {
        escMenu.SetActive(true); // Activate escape menu
        Time.timeScale = 0f; // Stop time
        isPaused = true;
    }
    
    public void Resume() {
        escMenu.SetActive(false); // Disable escape menu
        helpMenu.SetActive(false); // Disable escape menu
        Time.timeScale = 1f; // Resume time 
        isPaused = false;
    }

    public void RestartLevel() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name); // Reload scene
    }

    public void SelectLevel() {
        SceneManager.LoadScene("LevelSelect");
    }

    public void OpenHelp() {
        helpMenu.SetActive(true); // Activate help menu
    }

    public void CloseHelp() {
        helpMenu.SetActive(false); // Activate help menu
    }

    public void LoadNextLevel() {
        if (level < 12) {
            SceneManager.LoadScene("Level" + page.ToString() + "-" + (level + 1).ToString());
        }
        else {
            SceneManager.LoadScene("LevelSelect");
        }
    }

    public void ShowVictoryMenu() {
        Time.timeScale = 0f; // Stop time
        isPaused = true;
        victoryMenu.SetActive(true);
        PlayerPrefs.SetInt("Level" + page.ToString() + "-" + (level + 1).ToString(),1);
        PlayerPrefs.Save();
    }

    public void ShowDefeatMenu() {
        Time.timeScale = 0f; // Stop time
        isPaused = true;
        defeatMenu.SetActive(true);
    }



    void Start()
    {
        Time.timeScale = 1f; // Resume time

        escMenu = GameObject.FindGameObjectWithTag("EscMenu");
        helpMenu = GameObject.FindGameObjectWithTag("LevelHelpMenu");
        defeatMenu = GameObject.FindGameObjectWithTag("DefeatMenu");
        victoryMenu = GameObject.FindGameObjectWithTag("VictoryMenu");


        escMenu.SetActive(false);
        helpMenu.SetActive(false);
        defeatMenu.SetActive(false);
        victoryMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                Resume();
            }
            else {
                Pause();
            }
        }
    }

}
