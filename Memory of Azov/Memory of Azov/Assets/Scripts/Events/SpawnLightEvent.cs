using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLightEvent : EventClass {

    #region Public Variables
    public GameObject lightToSpawn;
    public Vector3 forceAdd;
    public bool isGreenLight = false;
    #endregion

    public void Awake()
    {
        lightToSpawn.SetActive(false);
    }

    public override void EventAction()
    {
        lightToSpawn.SetActive(true);
        SoundManager.Instance.ScenarioSoundEnum(SoundManager.SoundRequestScenario.S_LightbulbSpawns, this.gameObject.transform);
    }

    public void LightbullsFalls()
    {
        if (isGreenLight)
        {
            SoundManager.Instance.ScenarioSoundEnum(SoundManager.SoundRequestScenario.S_LightbulbFalls, this.gameObject.transform);
        }
        else
        {
            SoundManager.Instance.ScenarioSoundEnum(SoundManager.SoundRequestScenario.S_LightbulbFalls, this.gameObject.transform);
        }
        
    }

}
