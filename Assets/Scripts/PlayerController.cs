
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveForce = 15f;
    public float jumpImpulse = 6f;
    public Transform cameraTransform;
    public float fallLimit = -10f;

    private Rigidbody rb;
    private Vector3 startPosition;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 forward = Vector3.forward;
        Vector3 right = Vector3.right;

        if (cameraTransform != null)
        {
            forward = Vector3.ProjectOnPlane(
                cameraTransform.forward, Vector3.up).normalized;

            right = Vector3.ProjectOnPlane(
                cameraTransform.right, Vector3.up).normalized;
        }

        Vector3 direction = forward * v + right * h;
        rb.AddForce(direction * moveForce);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpImpulse,
                ForceMode.Impulse);
            isGrounded = false;
        }

        if (transform.position.y < fallLimit)
        {
            Respawn();
        }
    }

    void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                return;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    void Respawn()
    {
        rb.isKinematic = true;
        transform.position = startPosition;
        rb.isKinematic = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
