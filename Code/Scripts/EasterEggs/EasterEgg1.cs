using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class EasterEgg1 : MonoBehaviour, IPointerClickHandler
{
    public AudioClip sound;
    public GameObject clone;

    private GameObject canvas;

    void Start() {
        canvas = transform.parent.parent.gameObject;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position);
        StartCoroutine(SpawnIcons());
    }

    void Spawn() {
        GameObject newClone = Instantiate(clone);
        newClone.transform.SetParent(canvas.transform, false);
        float x = Random.Range(-250f,250f);
        float y = Random.Range(-250f,250f);
        newClone.transform.position = transform.position + Vector3.right * x + Vector3.up * y;
    }

    IEnumerator SpawnIcons() {
        for(int i = 0; i < 3; i++) {
            Spawn();
            yield return new WaitForSeconds(1);
        }
        for(int i = 0; i < 5; i++) {
            Spawn();
            yield return new WaitForSeconds(0.5f);
        }
        for(int i = 0; i < 10; i++) {
            Spawn();
            yield return new WaitForSeconds(0.25f);
        }
        for(int i = 0; i < 30; i++) {
            Spawn();
            yield return new WaitForSeconds(0.05f);
        }
    }
}
