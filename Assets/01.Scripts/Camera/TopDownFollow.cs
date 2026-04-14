using UnityEngine;

public class TopDownFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 15, 0);
    public float smoothTime = 0.15f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        Vector3 desired = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desired, ref velocity, smoothTime);
    }
}
