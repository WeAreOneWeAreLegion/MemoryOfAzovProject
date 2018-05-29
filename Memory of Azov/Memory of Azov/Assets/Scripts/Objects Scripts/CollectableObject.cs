using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour {

    public enum CollectableType { Key, GreenLight, RedLight }

    public CollectableType currentCType;

	public void CollectObject()
    {
        switch (currentCType)
        {
            case CollectableType.Key:
                GameManager.Instance.GetKey();
                break;
            case CollectableType.GreenLight:
                GameManager.Instance.player.GetGreenLight();
                break;
            case CollectableType.RedLight:
                GameManager.Instance.player.GetRedLight();
                break;
        }
        gameObject.SetActive(false);
    }

    public void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

}
