using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void PlayStepsMarbleSound()
    {
        SoundManager.Instance.PlayerSoundEnum(SoundManager.SoundRequestPlayer.P_StepsMarble);
    }

    private void PlayStepsCarpetSound()
    {
        SoundManager.Instance.PlayerSoundEnum(SoundManager.SoundRequestPlayer.P_StepsCarpet);
    }
}
