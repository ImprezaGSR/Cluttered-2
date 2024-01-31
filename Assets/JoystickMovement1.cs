using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMovement1 : MonoBehaviour
{
    public Joystick joystick;
    private Rigidbody2D rigidbody;
    public float speed = 3;
    public float dragForce = 0.3f;
    public Vector2 movement;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public ParticleSystem effect;
    private bool played = false;

    // Start is called before the first frame update
    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("Ambient");
        rigidbody = GetComponent<Rigidbody2D>();
        effect.Stop();
    }
    private void Update()
    {
        movement = new Vector2(joystick.Horizontal, joystick.Vertical);
        FindObjectOfType<AudioManager>().SetVolume("Jetpack", FindObjectOfType<AudioManager>().getVolume() * movement.magnitude * 0.2f);
        FindObjectOfType<AudioManager>().SetVolume("Ambient", FindObjectOfType<AudioManager>().getVolume() * gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 0.1f);
        if (movement.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(-movement.x, movement.y) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        if(FindObjectOfType<GameManager>().gameIsPaused == true){
            rigidbody.velocity = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        if (movement != Vector2.zero){
            moveCharacter(movement);
            if (played == false){
                played = true;
                effect.Play();
                FindObjectOfType<AudioManager>().Play("Jetpack");
            }
        }else{
            rigidbody.drag = dragForce;
            if (played == true){
                played = false;
                effect.Stop();
                FindObjectOfType<AudioManager>().Stop("Jetpack");
            }
        }
    }

    private void moveCharacter(Vector2 direction)
    {
        // rigidbody.MovePosition(rigidbody.position + (direction * speed * Time.fixedDeltaTime));
        rigidbody.AddForce(direction * speed);
        //rigidbody.velocity = direction * speed;
    }

    public void SpeedLevel(int level){
        speed = 3 + (2 * level);
        dragForce = 0.3f + (0.1f * level);
    }

}
