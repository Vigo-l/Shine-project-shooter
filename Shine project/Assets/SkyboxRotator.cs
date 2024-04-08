using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    public float rotationSpeed = 1.0f;  // Speed of rotation in degrees per second

    void Update()
    {
        // Rotate the skybox
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}
