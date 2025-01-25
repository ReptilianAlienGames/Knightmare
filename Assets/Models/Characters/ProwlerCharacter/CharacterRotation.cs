using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    public float rotationSpeed = 100f; // Rotation speed in degrees per second

    [Header("Rotation Toggles")]
    public bool rotateAroundX = false; // Toggle for X-axis rotation
    public bool rotateAroundY = true;  // Toggle for Y-axis rotation
    public bool rotateAroundZ = false; // Toggle for Z-axis rotation

    void Update()
    {
        Vector3 rotation = Vector3.zero;

        // Check which axes are toggled and add their respective rotation
        if (rotateAroundX)
        {
            rotation += Vector3.right * rotationSpeed * Time.deltaTime;
        }
        if (rotateAroundY)
        {
            rotation += Vector3.up * rotationSpeed * Time.deltaTime;
        }
        if (rotateAroundZ)
        {
            rotation += Vector3.forward * rotationSpeed * Time.deltaTime;
        }

        // Apply the rotation to the transform
        transform.Rotate(rotation);
    }
}
