using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform subject;
    
    private Vector3 cameraOffest;
    
    //Alternate Idea for camera control (will take more time to implement):
    //Camera has a set distance from the player. Player can rotate camera at will.
    //Camera rotates around the player.
    //Max/Min constraints for camera pitch

    // Start is called before the first frame update
    void Start()
    {
        //Lock in camera offset to positon at start time
        cameraOffest = transform.position;
    }

    //Late update to avoid visual choppiness
    private void FixedUpdate()
    {
        transform.position = subject.position + cameraOffest;
    }
}
