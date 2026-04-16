using UnityEngine;

public class TopDownFollow : MonoBehaviour
{
    public Transform target;
    public Rigidbody targetRb;

    public Vector3 offset = new Vector3(0f, 15f, 0f);

    [Header("Speed")]
    public float maxCameraSpeed = 12f;
    public float catchUpDistance = 5f;
    public float stopDistance = 0.05f;

    void LateUpdate()
    {
        if (target == null || targetRb == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 toTarget = desiredPosition - transform.position;

        // y축까지 포함하면 탑다운에서 어색할 수 있으니 XZ 거리만 기준
        Vector3 flatToTarget = new Vector3(toTarget.x, 0f, toTarget.z);
        float distance = flatToTarget.magnitude;

        if (distance <= stopDistance)
        {
            transform.position = new Vector3(desiredPosition.x, transform.position.y, desiredPosition.z);
            return;
        }

        // 플레이어 실제 이동 속도
        Vector3 targetVelocity = targetRb.velocity;
        float playerSpeed = new Vector3(targetVelocity.x, 0f, targetVelocity.z).magnitude;

        // 거리 비율 (0 ~ 1)
        float t = Mathf.Clamp01(distance / catchUpDistance);

        // 가까우면 playerSpeed, 멀면 maxCameraSpeed
        float cameraSpeed = Mathf.Lerp(playerSpeed, maxCameraSpeed, t);

        // 플레이어보다 느려지지 않게
        cameraSpeed = Mathf.Max(cameraSpeed, playerSpeed);

        // 최대 속도 초과 방지
        cameraSpeed = Mathf.Min(cameraSpeed, maxCameraSpeed);

        Vector3 move = flatToTarget.normalized * cameraSpeed * Time.deltaTime;

        // overshoot 방지
        if (move.magnitude > distance)
            move = flatToTarget;

        transform.position += new Vector3(move.x, 0f, move.z);

        // 높이는 고정
        transform.position = new Vector3(transform.position.x, desiredPosition.y, transform.position.z);
    }
}