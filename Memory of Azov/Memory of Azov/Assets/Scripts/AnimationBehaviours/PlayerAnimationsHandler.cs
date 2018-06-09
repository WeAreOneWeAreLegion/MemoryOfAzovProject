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
        myController.ShowEgg();
    }

    public void HideEgg()
    {
        myController.HideEgg();
    }

    public void ReactiveLight()
    {
        myController.ReactiveLight();
    }

    public void GameOver()
    {
        GameManager.Instance.CallPlayerDeath();
    }

}
