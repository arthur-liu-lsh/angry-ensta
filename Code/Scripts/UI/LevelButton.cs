using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public int level;

    public void Load() {
        transform.parent.parent.GetComponent<LevelPage>().Load(level);
    }
}
