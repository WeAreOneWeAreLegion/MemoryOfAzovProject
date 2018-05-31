using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDeactive : MonoBehaviour {

    public float timeAlive = 1;

    private float timer;

    private void OnEnable()
    {
        timer = Time.time;
    }

    private void Update()
    {
        if (Time.time >= timer + timeAlive)
            gameObject.SetActive(false);
    }

}
