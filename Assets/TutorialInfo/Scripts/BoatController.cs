using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 100f;

    void Update()
    {
        float move = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(Vector3.forward * move);

        float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * turn);
    }
}

