using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Collider))]
public class TriggerBoxController : MonoBehaviour
{
    private List<GameObject> inTriggerList;

    private void Start()
    {
        inTriggerList = new List<GameObject>();
    }

    //Trigger events
    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Player"))
        {
            inTriggerList.Add(other.gameObject);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.tag.Equals("Player"))
        {
            inTriggerList.Remove(other.gameObject);
        }
    }



    //Methods to access inTriggerList
    public int InTriggerCount()
    {
        return inTriggerList.Count;
    }

    public bool IsEmpty()
    {
        return inTriggerList.Count == 0;
    }

    //Return index 0. If empty, return null.
    public GameObject GetFirstIndex()
    {
        if(IsEmpty())
        {
            return null;
        }

        return inTriggerList[0];
    }

    public void RemoveFromList(GameObject other)
    {
        if (inTriggerList.Contains(other))
        {
            inTriggerList.Remove(other);
        }
    }
}
