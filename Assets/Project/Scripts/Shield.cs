using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    [SerializeField] private float rotateSpeed;

    private void Update()
    {
        transform.Rotate(Vector3.one * rotateSpeed);
    }
}
