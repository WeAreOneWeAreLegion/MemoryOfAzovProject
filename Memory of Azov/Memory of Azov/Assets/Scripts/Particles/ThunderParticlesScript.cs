using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderParticlesScript : MonoBehaviour {

    public GameObject thunderLightPrefab;

    [Header("Random range variables")]
    [Tooltip("Esta variable es el número inclusive, la segunda variable es el número exclusive")]
    public float firstNumber = 0;
    [Tooltip("Esta variable es el número exclusive")]
    public float secondNumber = 11;

    //Las dejo públicas para que se vea que cambian y se puedan modificar para testear
    private float spawnTime = 10; // one instance every interval seconds
    private float elapsedTime = 0f; // time elapsed since last generation

    private void OnEnable()
    {
        spawnTime = Random.Range(firstNumber, secondNumber);
    }

    void Update()
    {
        if (elapsedTime >= spawnTime)
        {
            SpawnThunder();
            spawnTime = Random.Range(firstNumber, secondNumber);
            elapsedTime = 0;
        }
        else
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 2.5f)
                if (thunderLightPrefab.activeInHierarchy)
                    DestroyThunder();
        }
    }

    void SpawnThunder()
    {
        StartCoroutine(SpawnLightSound());
        thunderLightPrefab.SetActive(true);
    }

    private IEnumerator SpawnLightSound()
    {
        yield return new WaitForSeconds(2.2f);
        SoundManager.Instance.AmbientSoundEnum(SoundManager.SoundRequestAmbient.A_Thunder, this.gameObject.transform);
        yield return null;
    }

    void DestroyThunder()
    {
        thunderLightPrefab.SetActive(false);
    }
}
