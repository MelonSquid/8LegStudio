using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (SphereCollider))]
public class SpiderController : MonoBehaviour, CustomController
{
    private SphereCollider collider;
    private const float IS_GROUNDED_COLLISION_ANGLE = 88; //the threshhold for determining if the character is grounded.

    private List<Collision> collisionList;

    private void Start()
    {
        collider = GetComponent<SphereCollider>();
        collisionList = new List<Collision>();
    }

    private void Update()
    {
        Debug.Log("Is grounded: " + IsGrounded());
    }


    //NOT WORKING
    public bool IsGrounded()
    {
        foreach (Collision collision in collisionList)
        {
            int contactCount = collision.contactCount;
            for (int i = 0; i < contactCount; i++)
            {
                ContactPoint cp = collision.GetContact(i);

                //If a contact point is beneath the player, they are grounded.
                if (PointInCone(transform.position, -transform.up, IS_GROUNDED_COLLISION_ANGLE, cp.point))
                {
                    return true;
                }
            }
        }

        //If no contact points were below the character, they are not grounded.
        return false;
    }

    public void Move(Vector3 moveVect)
    {
        throw new System.NotImplementedException();
    }

    //Manage Collision list's contents. It should contain only active collisions.
    private void OnCollisionEnter(Collision collision)
    {
        collisionList.Add(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        collisionList.Remove(collision);
    }


    //////
    ///AUXILARY METHODS
    //////
    
    //Tells if a given point is within a given cone
    public bool PointInCone(Vector3 origin, Vector3 direction, float coneAngle, Vector3 point)
    {
        float angleToPoint = Vector3.Angle(direction, point - origin);

        Debug.DrawRay(origin, direction, Color.red);
        Debug.DrawRay(origin, point - origin, Color.yellow);

        return angleToPoint <= coneAngle;
    }
}
