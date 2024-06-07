using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;

    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;

    float horizontal;
    float vertical;
    bool isGround = false;

    float xMinBound = -18f;
    float xMaxBound = 18f;  // Assuming you want a max bound for x as well
    float zMinBound = -21f;
    float zMaxBound = 21f;  // Assuming you want a max bound for z as well

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
        LimitMovement();
    }

    void Movement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * vertical * Time.deltaTime * speed);
        transform.Translate(Vector3.right * horizontal * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = true;
            Debug.Log("OnGround");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
            Debug.Log("OnGround is False");
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }
    }

    public void HitByRay()
    {
        Debug.Log("Laser Hit");
    }

    void LimitMovement()
    {
        // Clamp the position within the bounds
        float clampedX = Mathf.Clamp(transform.position.x, xMinBound, xMaxBound);
        float clampedZ = Mathf.Clamp(transform.position.z, zMinBound, zMaxBound);

        // Update the position to the clamped values
        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
    }
}
