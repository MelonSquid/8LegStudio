using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent (typeof (CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField]
    private CanvasController canvas;
    [SerializeField]
    private TriggerBoxController interactionTrigger;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float pushPower;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private int webShotCapacity;

    private int webShotRemaining;
    private Vector3 moveVect;
    
    //for moving blocks... rework
    private bool pushPull;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        webShotRemaining = webShotCapacity;
        interactionTrigger.RemoveFromList(canvas.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider c = GetComponent<Collider>();
        Vector3 bottom = c.bounds.center - new Vector3 (0, c.bounds.extents.y, 0);
        bool isGrounded = Physics.Raycast(bottom, Vector3.down, .01f, ~layerMask);
        if(!isGrounded)
        {
            moveVect.y -= gravity * Time.fixedDeltaTime;
        }
        else
        {
            moveVect.y = 0;
        }

        controller.Move(moveVect * moveSpeed * Time.fixedDeltaTime);
    }

    //Allow for pushing of Rigidbodies
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody hitBody = hit.collider.attachedRigidbody;
        if (hitBody == null)
        {
            return; //do nothing if the hit body has no rigidbody
        }
        //remove y component from hit ?

        hitBody.velocity = hit.moveDirection * pushPower;

        //hitBody.AddForce(hit.moveDirection * pushPower, ForceMode.Force);
    }

    //updates moveVect according to user input
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        //Get input from InputActions
        Vector2 moveVect2 = context.ReadValue<Vector2>();
        moveVect.x = moveVect2.x;
        moveVect.z = moveVect2.y;

        Vector3 moveVectNoY = new Vector3(moveVect.x, 0, moveVect.z);

        //Rotate Player to face movement direction
        Vector3 lookDir = Vector3.RotateTowards(transform.forward, moveVectNoY, 4, 0);
        transform.rotation = Quaternion.LookRotation(lookDir);

    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        bool buttonPress = context.ReadValueAsButton();
        //Only on button press -- not on button release
        if (buttonPress)
        {
            interactionTrigger.InTriggerCount();
            GameObject obj = interactionTrigger.GetFirstIndex();

            if (obj != null)
            {
                obj.GetComponent<Interactable>().Interact(gameObject);
            }
            
        }
    }


    
    public void RemoveFromTriggerList(GameObject obj)
    {
        interactionTrigger.RemoveFromList(obj);
    }


    //Get Sets
    public void SetPushPull(bool value)
    {
        pushPull = value;
    }

    public bool GetPushPull()
    {
        return pushPull;
    }

    public int GetWebShotRemaining()
    {
        return webShotRemaining;
    }

    public bool UseWebShot()
    {
        if (webShotRemaining > 0)
        {
            webShotRemaining--;
            canvas.WebUsed();
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public int GetWebShotCapacity()
    {
        return webShotCapacity;
    }

    public void ResetWebShotRemaining()
    {
        canvas.WebRefilled();
        webShotRemaining = webShotCapacity;
    }
}
