using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnInteract : MonoBehaviour, Interactable
{



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact(GameObject source)
    {
        PlayerController player = source.GetComponent<PlayerController>();
        bool result = player.UseWebShot();
        if (result)
        {
            player.RemoveFromTriggerList(gameObject);
            Destroy(gameObject);

        }
    }
}
