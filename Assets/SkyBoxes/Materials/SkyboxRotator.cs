using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    public float rotationSpeed = 1.0f; // Adjust this value to change the speed of rotation

    void Update()
    {
        // Get the current rotation of the skybox, assuming it's just a single float for rotation around Z-axis
        float currentRotation = RenderSettings.skybox.GetFloat("_Rotation");

        // Increment the rotation based on speed and time passed since last frame
        float newRotation = currentRotation + rotationSpeed * Time.deltaTime;

        // Apply the new rotation to the skybox material
        RenderSettings.skybox.SetFloat("_Rotation", newRotation);
    }
}