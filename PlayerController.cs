using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    Rigidbody rb;
    Animator anim;

    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;

    float horizontal;
    float vertical;
    public  bool isGround = false;

    float xMinBound = -18f;
    float xMaxBound = 18f;
    float zMinBound = -21f;
    float zMaxBound = 21f;

    [SerializeField] Joystick Joystick;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        instance = this;

        rb.drag = 0f;
        rb.angularDrag = 0f;
        rb.useGravity = true;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    // Update is called once per frame
    void Update()
    {
        LimitMovement();
    }
    void Movement()
    {
        float joystickHorizontalMove = Joystick.Horizontal;
        float joystickVerticalMove = Joystick.Vertical;


        // Calculating movement dir based on joystick input
        Vector3 movement = new Vector3(joystickHorizontalMove, 0.0f, joystickVerticalMove);
        movement.Normalize();

        transform.Translate(movement * speed * Time.deltaTime);

        bool isRunning = joystickHorizontalMove != 0 || joystickVerticalMove != 0;
        anim.SetBool("run", isRunning);

        if (isRunning)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, speed * Time.deltaTime * 100);
        }
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

   public void Jump()
    {
        if (isGround)
        {
            anim.SetTrigger("jump");
            rb.AddForce(Vector3.up * jumpSpeed,ForceMode.Impulse);
        }
    }

    public void HitByRay()
    {
        GameManager.instance.GameLoseMethod();
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
