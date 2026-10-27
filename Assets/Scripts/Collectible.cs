
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Collectible : MonoBehaviour
{
    public float spinSpeed = 90f;
    public float bobHeight = 0.25f;
    public float bobSpeed = 2f;

    private Vector3 basePosition;

    void Start()
    {
        basePosition = transform.position;
    }

    void Update()
    {
        transform.Rotate(
            0f, spinSpeed * Time.deltaTime, 0f, Space.World);

        float offset =
            Mathf.Sin(Time.time * bobSpeed) * bobHeight;

        transform.position =
            basePosition + Vector3.up * offset;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(1);
            Destroy(gameObject);
        }
    }
}

