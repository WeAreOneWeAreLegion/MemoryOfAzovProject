using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHUD : MonoBehaviour { //Erased soon

    [Header("\tGame Designers Variables")]
    [Header("Position Variables")]
    public Vector3 panelOffset;

    [Header("\t    Own Script Variables")]
    public Image healthBar;

    private Transform target;
    private RectTransform myRectTransform;

    private Enemy myEnemy;

    private float incrementFactorX;
    private float incrementFactorY;

    private void Update()
    {
        if (Time.timeScale == 0)
            return;

        Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(target.position);
        targetScreenPos.x += - myRectTransform.sizeDelta.x / 2 + panelOffset.x;
        targetScreenPos.x *= incrementFactorX;
        targetScreenPos.y += - myRectTransform.sizeDelta.y / 2 + panelOffset.y;
        targetScreenPos.y *= incrementFactorY;
        myRectTransform.anchoredPosition = targetScreenPos;
    }

    public void SetUp(Transform t, int initialHp)
    {
        incrementFactorX = GameManager.Instance.GetCanvasResolution().x / Screen.width;
        incrementFactorY = GameManager.Instance.GetCanvasResolution().y / Screen.height;

        myEnemy = t.GetComponent<Enemy>();
        target = myEnemy.visual;
        myRectTransform = GetComponent<RectTransform>();

        //myRectTransform.localScale = new Vector3(incrementFactorX, incrementFactorY, incrementFactorX);

        ModifyHp(initialHp);
    }

    public void ModifyHp(int hp)
    {
        int fullHp = myEnemy.initialHp;

        healthBar.transform.localScale = new Vector3(((float)hp/(float)fullHp),1,1);
    }

    public Transform GetTarget()
    {
        return myEnemy.transform;
    }

}
