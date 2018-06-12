using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayRainSoundScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter(Collider other)
    {
        SoundManager.Instance.AmbientSoundEnum(SoundManager.SoundRequestAmbient.A_Rain, this.gameObject.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        SoundManager.Instance.AmbientSoundEnum(SoundManager.SoundRequestAmbient.A_RainOff, this.gameObject.transform);
    }
}
