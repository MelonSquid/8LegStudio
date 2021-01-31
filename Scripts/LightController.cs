using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Light))]
public class LightController : MonoBehaviour
{
    private Vector3 initialPostision;

    [SerializeField]
    private float moveRange;
    [SerializeField]
    private float changeTime;
    [SerializeField]
    private float changeSpeed;
    [SerializeField]
    private float moveSpeed;

    private Vector3 moveVect;
    private Vector3 targetVect;
    private float timeSinceChange;

    private void Start()
    {
        initialPostision = transform.position;
        moveVect = Vector3.zero;
        targetVect = Vector3.zero;
        timeSinceChange = 10000;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Change target if time
        timeSinceChange += Time.fixedDeltaTime;
        if (timeSinceChange >= changeTime)
        {
            if (Vector3.Distance(initialPostision, transform.position) < moveRange)
            {
                targetVect = GetNewDirection();
            }
            else
            {
                targetVect = (initialPostision - transform.position).normalized;
            }

            timeSinceChange = 0;
        }

        //Calc next moveVect
        moveVect = moveVect.normalized;
        moveVect = Vector3.Lerp(moveVect, targetVect, changeSpeed * Time.fixedDeltaTime);

        //move in moveVect direction
        transform.position += moveVect * moveSpeed * Time.fixedDeltaTime;
    }

    private Vector3 GetNewDirection()
    {
        return new Vector3(Random.Range(-1,1), Random.Range(-1, 1), Random.Range(-1, 1)).normalized;
    }
}
