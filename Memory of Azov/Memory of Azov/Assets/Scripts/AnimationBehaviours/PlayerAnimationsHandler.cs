using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationsHandler : MonoBehaviour {

    public PlayerController myController;

    public void Action()
    {
        myController.DoAction();
    }

    public void ShowEgg()
    {
        myController.ShowItem();
    }

    public void HideEgg()
    {
        myController.HideItems();
    }

    public void ReactiveLight()
    {
        myController.ReactiveLight();
    }

    public void MoveAgainMegaStop()
    {
		if (CameraBehaviour.Instance.IsFollowing()) {
			myController.MoveAgainAfterMegaStop();
		}
    }

    public void OpenDoor()
    {
        myController.OpenCurrentDoor();
    }

    public void CrossDoor()
    {
        myController.CrossDoorActive();
    }

    public void ShowItem()
    {
        CameraBehaviour.Instance.ChangeCameraBehaviourState(CameraBehaviour.CameraState.ShowingItem);
    }

    public void GameOver()
    {
        GameManager.Instance.CallPlayerDeath();
    }

}
