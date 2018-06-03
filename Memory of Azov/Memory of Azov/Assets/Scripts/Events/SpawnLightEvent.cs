using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLightEvent : EventClass {

    #region Public Variables
    public GameObject lightToSpawn;
    public Vector3 forceAdd;
    #endregion

    public void Awake()
    {
        lightToSpawn.SetActive(false);
    }

    public override void EventAction()
    {
        lightToSpawn.SetActive(true);
    }

}
