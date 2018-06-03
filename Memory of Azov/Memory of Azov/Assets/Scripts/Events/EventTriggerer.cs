using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerer : MonoBehaviour {

    public enum EventType { Individual, Multiple }
    public enum EventOrder { First, Trigger }

    #region Public Variables
    public EventClass myEvent;
    public EventType myType;
    public EventOrder myOrder;
    public List<EventTriggerer> myCoEvents;
    #endregion

    #region Private Variable
    private bool allowTrigger;
    #endregion

    public void ActiveTrigger()
    {
        allowTrigger = true;
    }

    private void ActiveTriggererEvent()
    {
        foreach (EventTriggerer et in myCoEvents)
            if (et.GetIsTriggerer())
                et.ActiveTrigger();
    }

    public bool GetIsTriggerer()
    {
        return myOrder == EventOrder.Trigger;
    }

    private void ActiveEvent()
    {
        myEvent.EventAction();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameManager.Instance.GetTagOfDesiredType(GameManager.TypeOfTag.Player))
        {
            if (myType == EventType.Individual)
                ActiveEvent();
            else
            {
                if (myOrder == EventOrder.First)
                    ActiveTriggererEvent();
                else if (allowTrigger)
                    ActiveEvent();
            }

        }
    }
}
