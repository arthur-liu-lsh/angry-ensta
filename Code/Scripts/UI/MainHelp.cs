using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainHelp : MonoBehaviour
{
    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetProgress() {
        int page = 1;
        for (int level = 1; level < 13; level++) {
            PlayerPrefs.DeleteKey("Level" + page.ToString() + "-" + (level).ToString());
        }
        PlayerPrefs.Save();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            MainMenu();
        }
    }
}
