using UnityEngine;

public class BoatFloat : MonoBehaviour
{
    public float amplitude = 0.2f;
    public float frequency = 1f;
    private Vector3 startLocalPos;

    void Start()
    {
        startLocalPos = transform.localPosition;
    }

    void Update()
    {
        float offsetY = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.localPosition = startLocalPos + new Vector3(0, offsetY, 0);
    }
}

