using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMovementDance : MonoBehaviour {

    public AnimationCurve myAnimation;
    public float timeOscilate = 3;

    private float spawnTime;
    private float lerpTime;

    private Vector3 originalPose;

    private Vector3 initialPose;
    private Vector3 finalPose;

    private void OnEnable()
    {
        spawnTime = Time.time;
        originalPose = transform.position;

        transform.position = originalPose - Vector3.up * 0.5f;

        initialPose = originalPose - Vector3.up * 0.5f;
        finalPose = originalPose + Vector3.up * 0.5f;
    }

    private void Update ()
    {
        lerpTime += Time.deltaTime / timeOscilate;

        if (lerpTime > 1)
            lerpTime = 0;

        transform.position = new Vector3(transform.position.x, Mathf.Lerp(initialPose.y, finalPose.y, myAnimation.Evaluate(lerpTime)), transform.position.z);
    }
}
