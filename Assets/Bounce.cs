using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;
    public bool HasBeenPushed;

    void Start()
    {
        HasBeenPushed = false;
    }


    public bool IsGrounded()
    {
        // Raycast downward to check if the object is grounded
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }
}
