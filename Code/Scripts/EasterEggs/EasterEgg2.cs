using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

using TMPro;

public class EasterEgg2 : MonoBehaviour, IPointerClickHandler
{
    public AudioClip sound;

    private TMP_Text text;

    private bool shaking = false;

    void Start() {
        text = GetComponent<TMP_Text>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!shaking) {
            shaking = true;
            AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position);
            text.color = Color.red;
            text.fontSize = 100;
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake() {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0;

        while (elapsed < 21) {
            float x = Random.Range(-10f,10f);
            float y = Random.Range(-10f,10f);
            
            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            
            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPos;
        text.color = Color.white;
        text.fontSize = 50;
        shaking = false;
    }
}
