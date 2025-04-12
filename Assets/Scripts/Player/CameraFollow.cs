using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Player
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    [Header("Look Ahead")]
    public float lookAheadDistance = 2f;
    public float lookSmoothTime = 0.3f;
    private Vector3 currentVelocity;
    private Vector3 lookAheadOffset;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        // Basic follow position
        Vector3 desiredPosition = target.position + offset;

        // Look-ahead logic (optional)
        float dir = Input.GetAxisRaw("Horizontal");
        lookAheadOffset = new Vector3(lookAheadDistance * dir, 0f, 0f);

        desiredPosition += lookAheadOffset;

        // Smooth movement
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;
    }
}