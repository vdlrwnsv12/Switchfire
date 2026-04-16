using UnityEngine;

public class TopDownAimer : MonoBehaviour
{
    public Transform aimPivot;
    public float aimRotateSpeed = 30f;

    private Rigidbody rb;
    private Quaternion targetRot = Quaternion.identity;
    private Camera topDownCam;

    void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
        topDownCam = Camera.main;
    }

    void Update()
    {
        if (topDownCam == null)
            topDownCam = Camera.main;

        Ray ray = topDownCam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0f, rb.position.y, 0f));

        if (groundPlane.Raycast(ray, out float dist))
        {
            Vector3 worldPoint = ray.GetPoint(dist);
            Vector3 dir = worldPoint - rb.position;
            dir.y = 0f;

            if (dir.sqrMagnitude > 0.001f)
                targetRot = Quaternion.LookRotation(dir);
        }

        if (aimPivot != null)
        {
            aimPivot.rotation = Quaternion.Slerp(
                aimPivot.rotation,
                targetRot,
                aimRotateSpeed * Time.deltaTime
            );
        }
    }

    void FixedUpdate()
    {
        rb.MoveRotation(targetRot);
    }
}