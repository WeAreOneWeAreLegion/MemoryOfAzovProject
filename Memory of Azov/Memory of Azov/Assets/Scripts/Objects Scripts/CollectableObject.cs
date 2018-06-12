using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour {

    public enum CollectableType { Key, GreenLight, RedLight, MemoryOfAzov }

    public CollectableType currentCType;

    public float timeToShow = 2.25f;

    public void Start()
    {
        if (currentCType == CollectableType.MemoryOfAzov)
            GameManager.Instance.IncreaseMaxGems(gameObject);
    }

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
                StartCoroutine(PickUpCoroutine(PlayerController.ObjectPickedUp.Azov));
                break;
        }
        GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!GetComponent<Rigidbody>().isKinematic)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            if (currentCType == CollectableType.GreenLight)
            {
                //Sonido
                SoundManager.Instance.ScenarioSoundEnum(SoundManager.SoundRequestScenario.S_LightbulbFalls, this.gameObject.transform);
            }
        }
    }

    private IEnumerator PickUpCoroutine(PlayerController.ObjectPickedUp pick)
    {
        GameManager.Instance.player.PickUpObject(pick);
        yield return new WaitForSeconds(timeToShow);
        if (currentCType == CollectableType.MemoryOfAzov)
            GameManager.Instance.GetMemoryOfAzov();
        else
        {
            GameManager.Instance.player.StopShowingObject();
            CameraBehaviour.Instance.StopShowingItem();
        }

        if (currentCType == CollectableType.GreenLight)
            GameManager.Instance.player.ChangePlayerState(PlayerController.State.Cinematic);

        gameObject.SetActive(false);
        yield return null;
    }

}
