using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    #region Public Variables
    [Header("\tGame Designers Variables")]
    public int numOfDesiredGhosts;
    public int numOfDesiredGems;
    public List<EnemySO> enemiesData = new List<EnemySO>();

    [Header("\t    Own Script Variables")]
    [Tooltip("Lista de muros oscuros")]
    public List<GameObject> blackWalls = new List<GameObject>();
    [Tooltip("Lista de objetos que sueltan enemigos")]
    #endregion

    #region Private Variables
    private int ghostIndex = 0;

    private List<TransparentObject> objectsWithEnemies = new List<TransparentObject>();
    private List<TransparentObject> objectsWithGems = new List<TransparentObject>();
    private List<TransparentObject> allObjects = new List<TransparentObject>();
    #endregion

    private void Awake()
    {
        SetStartingRoom();
    }

    #region Set-Up Methods
    private void SetStartingRoom()
    {
        try
        {
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
                    temporalObjects[randomNum].enemyData = enemiesData[ghostIndex];
                    ghostIndex++;
                }
                else
                {
                    int randomData = Random.Range(0, enemiesData.Count);
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
            Debug.Log("Room: "+ name +" is bugged");
        }
    }
    #endregion

    #region Enemy Spawner Method
    public void ShowAllEnemiesFromRoom()
    {
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
