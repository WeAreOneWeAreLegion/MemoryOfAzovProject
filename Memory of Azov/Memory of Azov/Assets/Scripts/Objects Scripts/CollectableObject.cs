using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour {

    public enum CollectableType { Key, GreenLight, RedLight, MemoryOfAzov }

    public CollectableType currentCType;

    private float timeToShow = 2.25f;

	public void CollectObject()
    {
        Debug.Log("Collected");

        switch (currentCType)
        {
            case CollectableType.Key:
                GameManager.Instance.GetKey();
                break;
            case CollectableType.GreenLight:
                StartCoroutine(PickUpCoroutine(PlayerController.ObjectPickedUp.GreenLight));
                GameManager.Instance.player.GetGreenLight();
                break;
            case CollectableType.RedLight:
                StartCoroutine(PickUpCoroutine(PlayerController.ObjectPickedUp.RedLight));
                GameManager.Instance.player.GetRedLight();
                break;
            case CollectableType.MemoryOfAzov:
                GameManager.Instance.GetMemoryOfAzov();
                break;
        }
        GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private IEnumerator PickUpCoroutine(PlayerController.ObjectPickedUp pick)
    {
        GameManager.Instance.player.PickUpObject(pick);
        yield return new WaitForSeconds(timeToShow);
        GameManager.Instance.player.StopShowingObject();
        CameraBehaviour.Instance.StopShowingItem();
        gameObject.SetActive(false);
        yield return null;
    }

}
