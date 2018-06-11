using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {

	public float timeToDestroy = 2;
	private float timer;

	void Start () {
		timer = Time.time;
	}
	
	void Update () {
		if (Time.time >= timer + timeToDestroy) 
			Destroy (this.gameObject);
	}
}
