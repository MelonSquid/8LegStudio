using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CustomController
{
    void Move(Vector3 moveVect);
    bool IsGrounded();
}
