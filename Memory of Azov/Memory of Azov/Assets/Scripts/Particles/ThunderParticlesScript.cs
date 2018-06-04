using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderParticlesScript : MonoBehaviour {

    public GameObject thunderRenderPrefab;
    public GameObject thunderLightPrefab;

    private GameObject thunderRenderLighting;
    private GameObject thunderLightLighting;

    [Header("Random range variables")]
    [Tooltip("Esta variable es el número inclusive, la segunda variable es el número exclusive")]
    public float firstNumber = 0;
    [Tooltip("Esta variable es el número exclusive")]
    public float secondNumber = 11;

    //Las dejo públicas para que se vea que cambian y se puedan modificar para testear
    private float spawnTime = 10; // one instance every interval seconds
    private float elapsedTime = 0f; // time elapsed since last generation

    private bool randomTimerSet = false;

    void Update()
    {
        if (!randomTimerSet)
        {
            spawnTime = Random.Range(firstNumber, secondNumber);//(5, 11);
            randomTimerSet = true;
        }

        if (elapsedTime >= spawnTime)
        {
            SpawnThunder();
            elapsedTime = 0;
            randomTimerSet = false;
        }
        else
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 2 && elapsedTime <= 3f)
            {
                if (thunderRenderPrefab && thunderLightPrefab)
                {
                    DestroyThunder();
                }
            }
        }
    }

    void SpawnThunder()
    {
        thunderRenderLighting = (GameObject)Instantiate(thunderRenderPrefab, this.gameObject.transform.position, thunderRenderPrefab.transform.rotation);
        
        thunderLightLighting = (GameObject)Instantiate(thunderLightPrefab, this.gameObject.transform.position, thunderLightPrefab.transform.rotation);
    }

    void DestroyThunder()
    {
        Destroy(thunderRenderLighting, thunderRenderPrefab.GetComponent<ParticleSystem>().startLifetime);
        Destroy(thunderLightLighting, thunderLightPrefab.GetComponent<ParticleSystem>().startLifetime);
    }
}
