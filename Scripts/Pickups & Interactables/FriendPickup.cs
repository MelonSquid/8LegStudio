using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendPickup : MonoBehaviour
{
    [SerializeField]
    private CanvasController canvas;
    [SerializeField]
    private GameObject playerObject;
    [SerializeField]
    private List<ParticleSystem> spawnOnPickup;
    [SerializeField]
    private Vector3 effectScale;

    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.Equals(playerObject))
        {
            PlayerController player = other.GetComponentInParent<PlayerController>();
            
            canvas.FriendFound();
            for (int i = 0; i < spawnOnPickup.Count; i++)
            {
                ParticleSystem effect = Instantiate(spawnOnPickup[i], transform.position, Quaternion.identity);
                effect.scalingMode = ParticleSystemScalingMode.Hierarchy;
                effect.transform.localScale = effectScale;
                Destroy(effect, 3); //Destroy effect once it has finished playing
            }

            player.RemoveFromTriggerList(gameObject);
            Destroy(gameObject, .1f); //Delay added to ensure object is removed from trigger list

        }
    }
}
