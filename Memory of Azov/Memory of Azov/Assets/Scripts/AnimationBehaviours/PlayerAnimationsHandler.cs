using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationsHandler : MonoBehaviour {

    public PlayerController myController;

    public void Action()
    {
        myController.DoAction();
    }

    public void GameOver()
    {
        GameManager.Instance.CallPlayerDeath();
    }

}
