using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    private float rotationSpeed = 5f;

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
