using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 5;
    public ViewSwitcher viewSwitcher;

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir;

        if (viewSwitcher.Current == ViewSwitcher.ViewMode.FirstPerson)
        {
            Vector3 forward = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
            Vector3 right = new Vector3(transform.right.x, 0, transform.right.z).normalized;
            dir = (forward * v + right * h).normalized;
        }
        else
        {
            dir = new Vector3(h, 0, v).normalized;
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = speed * 1.5f;
        }
        else
        {
            speed = 5;
        }

        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
    }
}
