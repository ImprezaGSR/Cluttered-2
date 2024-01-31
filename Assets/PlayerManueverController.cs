using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManueverController : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    public float maxVelocity = 3;

    public float rotationSpeed = 3;
    // Start is called before the first frame update
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float yAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");

        ThrustForward(yAxis);
        Rotate(transform, xAxis * -rotationSpeed);
    }

    #region Maneuvering API

    private void ClampVelocity()
    {
        float x = Mathf.Clamp(rigidbody.velocity.x, -maxVelocity, maxVelocity);
        float y = Mathf.Clamp(rigidbody.velocity.y, -maxVelocity, maxVelocity);

        rigidbody.velocity = new Vector2(x,y);
    }

    private void ThrustForward(float amount)
    {
        Vector2 force = transform.up * amount;

        rigidbody.AddForce(force);
    }

    private void Rotate(Transform t, float amount)
    {
        t.Rotate(0, 0, amount);
    }

    #endregion
}
