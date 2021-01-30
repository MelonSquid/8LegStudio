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
    private float moveSpeed;
    [SerializeField]
    private int webShotCapacity;

    private int webShotRemaining;
    private Vector3 moveVect;
    
    //for moving blocks... rework
    private Vector3 moveVectScaled;
    private bool pushPull;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        webShotRemaining = webShotCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        //Move the player every frame
        moveVectScaled = moveVect * moveSpeed * Time.deltaTime; //Set for use of other functions
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

    public Vector3 GetMoveVectScaled()
    {
        return moveVectScaled;
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
