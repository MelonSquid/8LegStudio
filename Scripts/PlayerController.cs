using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent (typeof (CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveVect;

    [SerializeField]
    private float moveSpeed;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move the player every frame
        controller.SimpleMove(moveVect * moveSpeed);
    }
    
    //updates moveVect according to user input
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        //Get input from InputActions
        Vector2 moveVect2 = context.ReadValue<Vector2>();
        moveVect = new Vector3(moveVect2.x, 0, moveVect2.y);

        //Rotate Player to face movement direction
        Vector3 lookDir = Vector3.RotateTowards(transform.forward, moveVect, 4, 0);
        transform.rotation = Quaternion.LookRotation(lookDir);

    }
}
