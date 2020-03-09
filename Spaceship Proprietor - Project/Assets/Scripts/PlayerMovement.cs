using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float flySpeed = 25f;
    public float turnSpeed = 20f;

    public float shipHull = 70f;
    public float shipArmor = 20f;
    public int shipShield = 3;

    public float drag = 1f;

    private Rigidbody2D rb;
    private Quaternion shipRotation;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.drag = drag;
        rb.gravityScale = 0f;
    }

    void Update()
    {
        //Shift key (pilot) movement change
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //Movement
            movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        //Standard (capitan) movement
        else
        {
            //Movement
            movement = new Vector2(0, Input.GetAxis("Vertical"));
            
            //Rotation
            shipRotation = transform.rotation;

            float z = shipRotation.eulerAngles.z;
            z -= Input.GetAxis("Horizontal") * turnSpeed * 10 * Time.deltaTime;

            shipRotation = Quaternion.Euler(0, 0, z);
            transform.rotation = shipRotation;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CharacterMovement(movement);
    }

    void CharacterMovement(Vector2 direction)
    {
        rb.AddForce(shipRotation * (direction * flySpeed * 10 * Time.fixedDeltaTime));
    }
}
