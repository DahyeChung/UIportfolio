using UnityEngine;

public class PlayerClick : MonoBehaviour
{
    public float rotationSpeed = 10f;
    private void OnMouseDrag()
    {
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, -mouseX * rotationSpeed, Space.World);
    }
}
