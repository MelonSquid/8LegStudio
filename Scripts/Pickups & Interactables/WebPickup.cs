using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebPickup : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            PlayerController player = other.GetComponentInParent<PlayerController>();
            player.ResetWebShotRemaining();

            player.RemoveFromTriggerList(gameObject);
            Destroy(gameObject, .1f); //Delay added to ensure object is

        }
    }
}
