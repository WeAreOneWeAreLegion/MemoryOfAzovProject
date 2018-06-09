using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    #region Public Variables
    [Header("\tGame Designers Variables")]
    public int numOfDesiredGhosts;
    public int numOfGhostGivingHearts;
    public int numOfDesiredGems;
    public List<EnemySO> enemiesData = new List<EnemySO>();

    public Transform cameraCinematic;
    public float timeForShake = 10;

    [Header("\t    Own Script Variables")]
    [Tooltip("Lista de muros oscuros")]
    public List<GameObject> blackWalls = new List<GameObject>();
    [Tooltip("Lista de objetos que sueltan enemigos")]
    #endregion

    #region Private Variables
    private int ghostIndex = 0;
    private int heartsIndex = 0;
    private float internalTimer;
    private bool allInspected;

    private List<TransparentObject> objectsWithEnemies = new List<TransparentObject>();
    private List<TransparentObject> objectsWithGems = new List<TransparentObject>();
    private List<TransparentObject> allObjects = new List<TransparentObject>();
    #endregion

    private void Awake()
    {
        SetStartingRoom();
    }

    private void OnEnable()
    {
        internalTimer = Time.time;
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy && !allInspected && !GameManager.Instance.GetIsInCombateMode())
        {
            if (Time.time >= internalTimer + timeForShake)
            {
                Debug.Log("Shake it off");
                ShakeAnItem();
                DetectAllItemsInspected();
                internalTimer = Time.time;
            }
        }
    }

    #region Set-Up Methods
    private void SetStartingRoom()
    {
        try
        {
            ShowBlackWalls();

            List<TransparentObject> temporalObjects = new List<TransparentObject>();

            foreach (TransparentObject t in GetComponentsInChildren<TransparentObject>())
            {
                if (t.isStatic)
                    continue;

                t.spawnGhost = false;
                t.spawnGem = false;
                t.spawnAllGhosts = false;
                allObjects.Add(t);
                temporalObjects.Add(t);
            }

            objectsWithEnemies = new List<TransparentObject>();

            int randomNum;

            for (int i = 0; i < numOfDesiredGhosts; i++)
            {
                randomNum = Random.Range(0, temporalObjects.Count);

                temporalObjects[randomNum].spawnGhost = true;
                temporalObjects[randomNum].spawnAllGhosts = true;

                if (ghostIndex < enemiesData.Count)
                {
                    if (heartsIndex < numOfGhostGivingHearts)
                    {
                        temporalObjects[randomNum].givenHealth = true;
                        heartsIndex++;
                    }

                    temporalObjects[randomNum].enemyData = enemiesData[ghostIndex];
                    ghostIndex++;
                }
                else
                {
                    int randomData = Random.Range(0, enemiesData.Count);

                    if (heartsIndex < numOfGhostGivingHearts)
                    {
                        temporalObjects[randomNum].givenHealth = true;
                        heartsIndex++;
                    }

                    temporalObjects[randomNum].enemyData = enemiesData[randomData];
                }

                objectsWithEnemies.Add(temporalObjects[randomNum]);
                temporalObjects.RemoveAt(randomNum);
            }

            for (int i = 0; i < numOfDesiredGems; i++)
            {
                randomNum = Random.Range(0, temporalObjects.Count);

                temporalObjects[randomNum].spawnGem = true;
                objectsWithGems.Add(temporalObjects[randomNum]);

                GameManager.Instance.IncreaseMaxGems(temporalObjects[randomNum].transform.gameObject);

                temporalObjects.RemoveAt(randomNum);
            }
        }
        catch
        {
            Debug.LogWarning("La habitacion "+name+" no tiene suficientes objetos examinables respecto a los valores que se piden");
        }
    }
    #endregion

    #region Enemy Spawner Method
    public void ShakeAnItem()
    {
        int rng = Random.Range(0, allObjects.Count);

        while (allObjects[rng].GetIsInspected())
            rng = Random.Range(0, allObjects.Count);

        allObjects[rng].VisualShake();
        Debug.Log("Already shaked object "+ allObjects[rng].name);
    }

    public void DetectAllItemsInspected()
    {
        allInspected = true;

        foreach (TransparentObject to in allObjects)
        {
            if (!to.GetIsInspected())
            {
                allInspected = false;
                return;
            }
        }

    }

    public void ShowAllEnemiesFromRoom()
    {
        CameraBehaviour.Instance.TargetCinematic(cameraCinematic);
        CameraBehaviour.Instance.ChangeCameraBehaviourState(CameraBehaviour.CameraState.Cinematic);

        foreach (TransparentObject t in objectsWithEnemies)
        {
            if (t.spawnGhost)
            {
                t.ShakeObjectAnimation(false);
            }
        }
    }
    #endregion

    #region Black Wall Methods
    public void HideBlackWalls()
    {
        blackWalls.ForEach(x => x.SetActive(false));
    }

    public void ShowBlackWalls()
    {
        blackWalls.ForEach(x => x.SetActive(true));
    }
    #endregion
}
