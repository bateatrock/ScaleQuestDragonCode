using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunyAI : MonoBehaviour
{
    public float roamingRadius = 5f;
    public float roamingSpeed = 1f;
    public float jumpForce = 5f;
    public float changeDirectionInterval = 2f;

    private bool isJumping = false;

    void Start()
    {
        StartCoroutine(Roam());
    }

    IEnumerator Roam()
    {
        while (true)
        {
            // Generate a random direction within the roaming radius
            Vector3 randomDirection = Random.insideUnitSphere * roamingRadius;
            randomDirection += transform.position;
            randomDirection.y = transform.position.y;

            // Move towards the random direction
            while (Vector3.Distance(transform.position, randomDirection) > 0.1f)
            {
                if (!isJumping && Random.value < 0.1f && IsGrounded() && IsMoving()) // Check if on the ground, moving, and not jumping
                {
                    // Start a jump
                    StartCoroutine(Jump());
                }

                transform.LookAt(randomDirection);
                transform.position = Vector3.MoveTowards(transform.position, randomDirection, roamingSpeed * Time.deltaTime);
                yield return null;
            }

            // Wait for a short interval before changing direction
            yield return new WaitForSeconds(changeDirectionInterval);
        }
    }

    IEnumerator Jump()
    {
        isJumping = true;

        // Apply a force to make the bunny jump
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // Wait for the jump to finish
        yield return new WaitForSeconds(0.5f); // Adjust the duration as needed

        isJumping = false;
    }

    public bool IsGrounded()
    {
        Debug.Log("on ground");
        // Check if there's ground beneath the bunny using a raycast
        return Physics.Raycast(transform.position, Vector3.down, 0.25f);
        
    }

    public bool IsMoving()
    {
        Debug.Log("Is moving");
        // Check if the bunny is actively moving
        return GetComponent<Rigidbody>().velocity.magnitude > 0.1f;
    }
}
