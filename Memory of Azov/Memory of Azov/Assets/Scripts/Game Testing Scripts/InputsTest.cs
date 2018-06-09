using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsTest : MonoBehaviour {

    public Animator myAnimator;

	// Update is called once per frame
	void Update ()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");

        myAnimator.SetFloat("xValue", xInput);
        myAnimator.SetFloat("zValue", zInput);

        if (Input.GetKeyDown(KeyCode.W))
        {
            myAnimator.SetTrigger("OpenDoor");
            ActiveSecondaryLayer();
        }
    }

    private void ActiveSecondaryLayer()
    {
        myAnimator.SetLayerWeight(1,1);
    }

    public void DeactiveSecondaryLayer()
    {
        myAnimator.SetLayerWeight(1, 0);
    }

}
