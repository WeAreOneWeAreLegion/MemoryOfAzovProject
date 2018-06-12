using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChandelierBehaviour : MonoBehaviour {

    public float xDesiredRotation;
    public float lerpTime = 2;
    public bool activate;

    private float currentRotation;
    private float timer;

	// Use this for initialization
	void Start () {
        currentRotation = transform.eulerAngles.x;
	}
	
	// Update is called once per frame
	void Update () {
        if (activate)
        {
            timer += Time.deltaTime / lerpTime;
            transform.rotation = Quaternion.Euler(Vector3.right * Mathf.Lerp(currentRotation, xDesiredRotation, timer));
            if (timer >= 1)
                enabled = false;
        }
	}
}
