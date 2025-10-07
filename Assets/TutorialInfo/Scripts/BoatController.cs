using UnityEngine;

/*public class BoatController : MonoBehaviour
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
}*/

public class BoatController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 100f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float move = Input.GetAxis("Vertical") * moveSpeed * Time.fixedDeltaTime;
        Vector3 moveVector = transform.forward * move;
        rb.MovePosition(rb.position + moveVector);

        float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;

        if (tag == "Buoy")
        {
            Debug.Log("Човен зіштовхнувся зі звичайним буєм!");
        }
        else if (tag == "ColorBuoy")
        {
            Debug.Log("Зміна кольору буя!");
            ChangeBuoyColor(collision.gameObject);
        }
        else if (tag == "GrowBuoy")
        {
            Debug.Log("Буй збільшується!");
            GrowBuoy(collision.gameObject);
        }
    }

    private void ChangeBuoyColor(GameObject buoy)
    {
        Renderer renderer = buoy.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = new Color(
                Random.value,
                Random.value,
                Random.value
              );
        }
    }
    
    private void GrowBuoy(GameObject buoy)
    {
       buoy.transform.localScale *= 1.2f;
    }
}


