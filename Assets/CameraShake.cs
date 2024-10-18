using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public void StartCameraShake(float shakeTime, float shakeIntensity, RectTransform uiElement)
    {
        StartCoroutine(Shake(shakeTime, shakeIntensity, uiElement));
    }

    IEnumerator Shake(float shakeTime, float shakeIntensity, RectTransform uiElement)
    {
        Vector3 originalCamPos = transform.position;   // Camera original position
        Vector3 originalUIPos = uiElement.anchoredPosition; // UI original position
        float time = shakeTime;

        while (time > 0)
        {
            if (Time.timeScale != 0)
            {
                Vector3 camOffset = Vector3.zero;
                Vector3 uiOffset = Vector3.zero;

                // Generate random offsets for camera and UI
                camOffset.x = Random.Range(-shakeIntensity, shakeIntensity);
                camOffset.y = Random.Range(-shakeIntensity, shakeIntensity);
                uiOffset.x = Random.Range(-shakeIntensity, shakeIntensity);
                uiOffset.y = Random.Range(-shakeIntensity, shakeIntensity);

                // Apply the offsets to the camera and UI
                transform.position = originalCamPos + camOffset;
                uiElement.anchoredPosition = originalUIPos + uiOffset;
            }

            time -= Time.deltaTime;
            yield return null;
        }

        // Restore original positions after shaking
        transform.position = originalCamPos;
        uiElement.anchoredPosition = originalUIPos;
    }
}
