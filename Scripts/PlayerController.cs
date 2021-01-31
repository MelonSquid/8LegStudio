using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent (typeof (CharacterController))]
[RequireComponent (typeof (AudioSource))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField]
    private CanvasController canvas;
    [SerializeField]
    private TriggerBoxController interactionTrigger;
    [SerializeField]
    private AudioSource webSound;
    [SerializeField]
    private AudioSource happySpider;
    [SerializeField]
    private AudioSource walkingSpider;
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


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        webShotRemaining = webShotCapacity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!controller.isGrounded)
        {
            moveVect.y -= gravity * Time.fixedDeltaTime;
        }
        else
        {
            moveVect.y = -.5f;
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
    }

    //updates moveVect according to user input
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        //Get input from InputActions
        Vector2 moveVect2 = context.ReadValue<Vector2>();
        moveVect.x = moveVect2.x;
        moveVect.z = moveVect2.y;

        Vector3 moveVectNoY = new Vector3(moveVect.x, 0, moveVect.z);
        
        if(moveVectNoY.magnitude > 0.01)
        {
            walkingSpider.Play();
        }
        else
        {
            walkingSpider.Stop();
        }

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
            GameObject obj = interactionTrigger.GetFirstIndex();


            if (obj != null)
            {
                obj.GetComponent<Interactable>().Interact(gameObject);
            }
            
        }
    }


    //Music playing methods
    public void HappySpider()
    {
        happySpider.Play();
    }

    //IntereactionTrigger manager function
    public void RemoveFromTriggerList(GameObject obj)
    {
        interactionTrigger.RemoveFromList(obj);
    }


    //Get Sets
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
            webSound.Play();
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
