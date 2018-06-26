using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float shakeTimer;
    private float shakeAmount;

    private void Update()
    {
        if(shakeTimer >= 0)
        {
            Vector2 shakePos = Random.insideUnitCircle * shakeAmount;

            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + shakePos.x, transform.position.y + shakePos.y, transform.position.z), 0.5f);
            //new Vector3(transform.position.x + shakePos.x, transform.position.y + shakePos.y, transform.position.z);
            shakeTimer -= Time.deltaTime;
        } else
        {
            transform.position = new Vector3(0f, 0f, -10);
        }
    }

    public void ShakeCamera(float shakePower, float shakeDuration)
    {
        shakeAmount = shakePower;
        shakeTimer = shakeDuration;
    }
}
