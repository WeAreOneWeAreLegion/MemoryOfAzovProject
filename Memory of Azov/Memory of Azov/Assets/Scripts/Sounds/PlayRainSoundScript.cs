using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRainSoundScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SoundManager.Instance.AmbientSoundEnum(SoundManager.SoundRequestAmbient.A_Rain, this.gameObject.transform);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
