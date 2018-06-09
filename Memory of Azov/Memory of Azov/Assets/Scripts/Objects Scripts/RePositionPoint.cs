using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePositionPoint : MonoBehaviour {

    public Transform target;

    public void Start()
    {
        target = GameManager.Instance.GetPlayer();
    }

    public void RePlace()
    {
        transform.position = target.position;
    }
}
