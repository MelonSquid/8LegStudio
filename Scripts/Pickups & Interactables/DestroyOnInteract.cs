using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnInteract : MonoBehaviour, Interactable
{
    [SerializeField]
    private GameObject spawnOnDestroy;
    [SerializeField]
    private Transform spawnTransform;
    [SerializeField]
    private Vector3 spawnScale;
    [SerializeField]
    private Vector3 spawnRot;

    public void Interact(GameObject source)
    {
        PlayerController player = source.GetComponent<PlayerController>();
        bool result = player.UseWebShot();
        if (result)
        {

            GameObject web = Instantiate(spawnOnDestroy, spawnTransform.position, Quaternion.Euler(spawnRot));
            web.transform.localScale = spawnScale;
            player.RemoveFromTriggerList(gameObject);
            Destroy(gameObject);

        }
    }
}
