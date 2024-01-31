using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperyManuerver : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public float speed = 10;
    public float dragForce = 20;
    public Vector2 movement;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public HealthScript healthScript;
    
    // Start is called before the first frame update
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (this.transform.Find("Player") == null){
            movement = Vector2.zero;
            rigidbody.velocity = Vector3.zero;
        }

        if (movement.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(-movement.x, movement.y) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
        if (movement == Vector2.zero){
            rigidbody.drag = dragForce;
        }
    }

    private void moveCharacter(Vector2 direction)
    {
        // rigidbody.MovePosition(rigidbody.position + (direction * speed * Time.fixedDeltaTime));
        rigidbody.AddForce(direction * speed);
        //rigidbody.velocity = direction * speed;
    }

    // private void stopCharacter(Vector2 direction)
    // {
    //     rigidbody.drag = direction * dragForce;
    // }
}
