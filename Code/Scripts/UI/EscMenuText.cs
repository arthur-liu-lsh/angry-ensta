using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class EscMenuText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LevelCanvas levelCanvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<LevelCanvas>();
        TMP_Text escMenuText = GetComponent<TMP_Text>();
        escMenuText.text = "Niveau " + levelCanvas.page.ToString() + "-" + levelCanvas.level.ToString(); // Set text to current level in EscMenu
    }
}
