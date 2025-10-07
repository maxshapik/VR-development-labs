using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FloatingCrate : MonoBehaviour
{
    public float amplitude = 0.2f;
    public float frequency = 1f;
    public float rotationStrength = 2f;

    private Rigidbody rb;
    private Vector3 startPos;
    private float randomOffset;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        randomOffset = Random.Range(0f, 10f);
    }

    void FixedUpdate()
    {
        float offsetY = Mathf.Sin(Time.time * frequency + randomOffset) * amplitude;
        Vector3 newPos = startPos + new Vector3(0, offsetY, 0);

        rb.MovePosition(newPos);

        Quaternion newRot = Quaternion.Euler(
            Mathf.Sin(Time.time * frequency + randomOffset) * rotationStrength,
            transform.rotation.eulerAngles.y,
            Mathf.Cos(Time.time * frequency + randomOffset) * rotationStrength
        );

        rb.MoveRotation(newRot);
    }
}

