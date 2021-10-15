using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    private Vector3 targetPosition;

    public float shakeDuration;
    public float shakeMagnitude;

    private bool following = true;
    private bool panning = false;

    public void ScreenShake() {
        following = false; // Blocks camera movement
        StartCoroutine(Shake(shakeDuration, shakeMagnitude));
    }

    IEnumerator Shake(float duration, float magnitude) {
        Vector3 originalPos = transform.position;
        float elapsed = 0;

        while (elapsed < duration) {
            float x = Random.Range(-1f,1f) * magnitude;
            float y = Random.Range(-1f,1f) * magnitude;
            
            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            
            elapsed += Time.deltaTime;

            yield return null;
        }

        following = true;
        transform.position = originalPos;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            panning = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            panning = false;
        }

        // Define a target position on target transform
        if (panning) {
            targetPosition.x = 19.20f;
        }
        else {
            if (target != null) {
                targetPosition = target.TransformPoint(new Vector3(0, 0, -10));

                if (targetPosition.x < 0) {
                    targetPosition.x = 0;
                }
                if (targetPosition.x > 19.20f) {
                    targetPosition.x = 19.20f;
                }
            }
            else {
                targetPosition = new Vector3(0, 0, -10);
            }
        }


        if (following) {
        // Smoothly move the camera towards that target position
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(targetPosition.x, transform.position.y, transform.position.z), ref velocity, smoothTime);
        }
    }
}