using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public GameObject errorText;

    private GameObject[] pageArray;
    private int currentPage = 0;


    public void PageUp() {
        if (currentPage > 0) {
            pageArray[currentPage].SetActive(false);
            pageArray[currentPage-1].SetActive(true);
            currentPage--;
        }
    }

    public void PageDown() {
        if (currentPage < pageArray.Length - 1) {
            pageArray[currentPage].SetActive(false);
            pageArray[currentPage+1].SetActive(true);
            currentPage++;
        }
    }

    public void LoadLevel(int page, int level) {
        string str = "Level";
        str += page.ToString();
        str += "-";
        str += level.ToString();
        if ((page == 1 && level == 1) || PlayerPrefs.HasKey(str)) {
            SceneManager.LoadScene(str);
        }
        else {
            GameObject text = Instantiate(errorText);
            text.transform.SetParent(transform, false);
        }
    }

    void Start() {
        GameObject[] listLevel = GameObject.FindGameObjectsWithTag("LevelPage");
        pageArray = new GameObject[listLevel.Length];
        for (int i = 0; i < pageArray.Length; i++) {
            for (int j = 0; j < pageArray.Length; j++) {
                if (listLevel[j].GetComponent<LevelPage>().page == i+1){
                    pageArray[i] = listLevel[j];
                }
            }
        }
        for (int i = 1; i < pageArray.Length; i++) {
            pageArray[i].SetActive(false);
        }
    }
}
