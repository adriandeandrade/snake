using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float shakeTimer;
    private float shakeAmount;

    [SerializeField] private float intensity;

    public void ShakeCamera()
    {
        iTween.ShakePosition(gameObject, new Vector3(intensity, intensity, 0f), 0.3f);
    }
}
