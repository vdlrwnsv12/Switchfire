using UnityEngine;

public class TopDownAimer : MonoBehaviour
{
    public Transform aimPivot;
    public Camera topDownCam;
    public float aimRotateSpeed = 30f;

    private Rigidbody rb;
    private Quaternion targetRot;

    void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    void Update()
    {
        Ray ray = topDownCam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, rb.position.y, 0));

        if (groundPlane.Raycast(ray, out float dist))
        {
            Vector3 worldPoint = ray.GetPoint(dist);
            Vector3 origin = new Vector3(rb.position.x, worldPoint.y, rb.position.z);
            Vector3 dir = worldPoint - origin;

            if (dir.sqrMagnitude > 0.001f)
                targetRot = Quaternion.LookRotation(dir);
        }

        // 시각적인 총 방향은 부드럽게
        aimPivot.rotation = Quaternion.Slerp(aimPivot.rotation, targetRot, aimRotateSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        // 플레이어 몸통 물리 회전은 FixedUpdate
        rb.MoveRotation(targetRot);
    }
}
