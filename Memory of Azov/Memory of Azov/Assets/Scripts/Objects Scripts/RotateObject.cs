using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {

    public float rotation;

    private void Update()
    {
        transform.rotation *= Quaternion.Euler(Vector3.forward * rotation * Time.deltaTime);
    }
}
