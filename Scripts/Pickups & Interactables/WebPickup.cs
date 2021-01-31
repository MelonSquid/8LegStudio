using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebPickup : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObject;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(playerObject))
        {
            PlayerController player = other.GetComponentInParent<PlayerController>();
            player.ResetWebShotRemaining();

            player.RemoveFromTriggerList(gameObject);
            Destroy(gameObject, .1f); //Delay added to ensure object is

        }
    }
}
