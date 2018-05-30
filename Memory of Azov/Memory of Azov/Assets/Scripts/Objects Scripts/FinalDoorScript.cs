using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoorScript : MonoBehaviour {

	public void Action()
    {
        if (GameManager.Instance.HasThroneKey())
        {
            Debug.Log("Can end game");
        }
    }

}
