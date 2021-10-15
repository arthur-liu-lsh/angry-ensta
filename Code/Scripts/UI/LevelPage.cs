using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPage : MonoBehaviour
{
    public int page;

    public void Load(int level) {
        transform.parent.GetComponent<LevelSelect>().LoadLevel(page, level);
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void PageUp() {
        transform.parent.GetComponent<LevelSelect>().PageUp();
    }

    public void PageDown() {
        transform.parent.GetComponent<LevelSelect>().PageDown();
    }
}
