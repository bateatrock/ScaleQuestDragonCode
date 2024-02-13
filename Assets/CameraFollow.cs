using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the player's transform
    public float smoothSpeed = 0.125f; // Adjustable smoothing factor
    public Vector3 offset = new Vector3(0f, 2f, -5f); // Camera offset from the player

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired position of the camera with offset
            Vector3 desiredPosition = target.position + offset;

            // Smoothly interpolate between the current position and the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Set the camera's position to the smoothed position
            transform.position = smoothedPosition;

            // Make the camera look at the player
            transform.LookAt(target);
        }
    }
}