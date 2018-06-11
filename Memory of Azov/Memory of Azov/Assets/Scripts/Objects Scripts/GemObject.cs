using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemObject : MonoBehaviour
{
    #region Public Variables
    [Header("\tGame Designers Variables")]
    [Tooltip("Fuerza de salida al aparecer")]
    [Range(1, 10)] public float initialForce = 5f;
    [Tooltip("Tiempo que tardara en autodestruirse si se es una gema que se auto recoje")]
    [Range(1, 10)] public float timeForAutoDestroy = 1f;

    [Header("\t    Own Script Variables")]
    [Tooltip("Referencia al Rigidbody")]
    public Rigidbody myRGB;
    #endregion

    #region Private Variables
    private bool autoDestroy;
    private float timeToDesapear;
    private bool pickedUp;

    private MeshRenderer myMesh;
    #endregion

    private void Start()
    {
        if (myMesh == null)
            myMesh = GetComponentInChildren<MeshRenderer>();

        myMesh.material.mainTexture = EggsManager.Instance.GetRandomTexture();

        if (myRGB == null)
            myRGB = GetComponent<Rigidbody>();

        if (myRGB.useGravity)
            myRGB.AddForce(transform.forward * initialForce, ForceMode.Impulse);

    }

    private void Update()
    {
        if (autoDestroy)
        {
            timeToDesapear += Time.deltaTime;

            if (timeToDesapear >= timeForAutoDestroy)
            {
                timeToDesapear = 0;
                DestroyGem();
            }
        } 
    }

    #region Destroy Method
    private void DestroyGem()
    {
        GameManager.Instance.IncreaseNumOfGems();
        ObjectsManager.Instance.ReturnRequest(gameObject, ObjectsManager.ItemRequest.Gem);
    }
    #endregion

    #region Public Methods
    public void ResetGem()
    {
        if (myRGB == null)
            myRGB = GetComponent<Rigidbody>();

        if (myMesh == null)
            myMesh = GetComponentInChildren<MeshRenderer>();

        myRGB.isKinematic = false;

        myRGB.velocity = Vector3.zero;
        myRGB.useGravity = true;

        GetComponent<Collider>().enabled = true;
        GetComponent<Collider>().isTrigger = false;

        myMesh.enabled = true;

        autoDestroy = false;
    }

    public void DiscoveredByWallPaint()
    {
        myRGB.AddForce(transform.up * initialForce, ForceMode.Impulse);
    }

    public void DiscoveredByFeature()
    {
        if (myRGB == null)
            myRGB = GetComponent<Rigidbody>();

        if (myMesh == null)
            myMesh = GetComponentInChildren<MeshRenderer>();

        myMesh.enabled = false;
        pickedUp = false;

        myRGB.velocity = Vector3.zero;
        myRGB.useGravity = false;
        myRGB.AddForce(Vector3.up * 2, ForceMode.Impulse);

        GetComponent<Collider>().enabled = false;

		StartCoroutine(PickUpCoroutine());

        autoDestroy = true;
        timeToDesapear = 0;
    }

    private IEnumerator PickUpCoroutine()
    {
		myMesh.enabled = false;
        GameManager.Instance.player.PickUpObject(PlayerController.ObjectPickedUp.Egg);
        yield return new WaitForSeconds(timeForAutoDestroy);
        DestroyGem();
        yield return null;
    }
    #endregion

    #region Unity Collision/Trigger Methods
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == GameManager.Instance.GetTagOfDesiredType(GameManager.TypeOfTag.Player) && !pickedUp)
        {
            pickedUp = true;
            StartCoroutine(PickUpCoroutine());
        }
        else
        {
            myRGB.isKinematic = true;
            GetComponent<Collider>().isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameManager.Instance.GetTagOfDesiredType(GameManager.TypeOfTag.Player) && !pickedUp)
        {
            pickedUp = true;
            StartCoroutine(PickUpCoroutine());
        }
    }
    #endregion
}
