using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//BROKEN

[RequireComponent (typeof (Rigidbody))]
public class PushBoxInteraction : MonoBehaviour, Interactable
{
    private bool interacting;
    private PlayerController player;
    private Rigidbody rb;
    

    private void Start()
    {
        interacting = false;
        player = null;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (interacting)
        {
            rb.MovePosition(player.GetMoveVectScaled());
        }
    }
    public void Interact(GameObject source)
    {
        if (!interacting)
        {
            interacting = true;
            player = source.GetComponent<PlayerController>();
            player.SetPushPull(true);
        }
        else
        {
            interacting = false;
            player = null;
        }
    }

    public void StopInteracting() { 
    }
}
