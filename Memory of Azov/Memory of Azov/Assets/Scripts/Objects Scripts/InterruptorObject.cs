using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterruptorObject : LightenableObject
{

    #region Public Variables
    [Header("\tGame Designers Variables")]
    [Tooltip("Puerta que accionara este timbre")]
    public ConectionScript myDoor;
    [Tooltip("Color del timbre para que se accione")]
    public PlayerController.LightColor colorToOpenBell;
    [Tooltip("Emissive color")] 
    public Color emissiveColor;

    [Header("Own Script Variables")]
    public MeshRenderer myRenderer;
    #endregion

    #region Private Variables
    private bool insideRadius;

    private Material myMat;
    private Transform target;
    #endregion

    private void Start()
    {
        target = GameManager.Instance.GetPlayer();
        myMat = myRenderer.material;
        myMat.SetColor("_EmissionColor", Color.black);
    }

    private void Update()
    {
        CheckPlayerDistance();
        CheckLighten();
    }

    #region Lighten Methods
    private void CheckPlayerDistance()
    {
        if (Vector3.Distance(target.position, transform.position) < target.GetComponent<PlayerController>().GetInitialLanternLength())
        {
            GameManager.Instance.player.OnLightenPuzzleEnter(this.gameObject);
        }
        else
        {
            GameManager.Instance.player.OnLightenPuzzleExit(this.gameObject);
        }
    }

    private void CheckLighten()
    {
        if (insideRadius)
        {
            if (GameManager.Instance.player.IsCurrentLightOfColor(colorToOpenBell))
            {
                myDoor.OpenByBell();
                enabled = false;
                myMat.SetColor("_EmissionColor", emissiveColor);
                tag = GameManager.Instance.GetTagOfDesiredType(GameManager.TypeOfTag.Wall);
            }
        }
    }

    public override void InsideLanternRange()
    {
        insideRadius = true;
    }

    public override void OutsideLanternRange()
    {
        insideRadius = false;
    }

    public override bool IsInSight()
    {
        return insideRadius;
    }
    #endregion
}
