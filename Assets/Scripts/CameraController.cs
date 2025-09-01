using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private float speed = 2f;
    private Vector3 offset = new Vector3(0, 0, -10);
    
    void LateUpdate()
    {
        Vector3 targetPosition = playerTransform.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
