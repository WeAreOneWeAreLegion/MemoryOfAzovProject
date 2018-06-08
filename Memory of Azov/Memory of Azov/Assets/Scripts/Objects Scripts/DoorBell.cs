﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBell : MonoBehaviour {

    #region Public Variables
    [Header("\tGame Designers Variables")]
    [Tooltip("Si es fake spawnea fantasmas")]
    public bool isFakeBell;
    [Tooltip("Posiciones donde apareceran los fantasmas, por transform habra un fantasma")]
    public List<Transform> ghostSpawns = new List<Transform>();
    public List<EnemySO> enemiesData = new List<EnemySO>();
    [Tooltip("Cuantos fantasmas dejaran caer una vida")]
    public int numOfGivenHearts;
    [Header("\t    Own Script Variables")]
    [Tooltip("Puerta que accionara este timbre")]
    public ConectionScript myDoor;
    public Animation myAnimation;
    #endregion

    #region Open Door Method
    public void OpenDoor()
    {
        if (isFakeBell)
        {
            for (int i = 0; i < ghostSpawns.Count; i++)
            {
                Ray ray = new Ray(transform.position + Vector3.up, Vector3.down);

                RaycastHit hit;

                Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("FloorLayer"));

                ObjectsManager.ItemRequest ir = ObjectsManager.ItemRequest.None;

                if (i < numOfGivenHearts)
                    ir = ObjectsManager.ItemRequest.Health;

                GameObject go = EnemyManager.Instance.GetEnemy(hit.transform != null ? hit.transform.parent : this.transform, enemiesData[i], ir);

                go.transform.position = ghostSpawns[i].position;
                go.transform.forward = GameManager.Instance.GetPlayer().position - transform.position;
            }
        }
        else
        {
			Debug.Log ("Door Open");
            myDoor.OpenByBell();
        }

        enabled = false;
        myAnimation.Play();
        tag = GameManager.Instance.GetTagOfDesiredType(GameManager.TypeOfTag.Wall);
    }
    #endregion
}
