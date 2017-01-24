using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour {

    public float jumpHeight = 4;
    public float timeToJumpApex = 0.4f;

    public float moveSpeed;
    protected float accelerationTimeAirborn = 0.2f;
    protected float acclerationTimeGrounded = 0.1f;

    protected float gravity = -20;
    protected float jumpVelocity = 8;
    protected Vector2 velocity;
    protected float velocityXSmoothing;
    protected float velocityZSmoothing;
    protected Vector3 rotationSmoothing;

    protected PlayerPhysics physics;
    public bool active = true;

    public AudioClip jumpFX;
    public static PlayerController instance;

    public Animator anim;

    [HideInInspector]
    public bool moving;

    void Awake() {
        anim = GetComponentInChildren<Animator>();
        instance = this;
        physics = GetComponent<PlayerPhysics>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    void Update() {
        movement();
    }

    void movement() {
        if (physics.collisions.above || physics.collisions.below)
        {
            velocity.y = 0;
        }

        isJumping = false;
        if (active && Input.GetButtonDown("Jump") && (physics.collisions.below || physics.collisions.descendingSlope || physics.collisions.climbingSlope)) {
            //jump();
        }

        Vector2 input = (active) ? new Vector2(-Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal")) : Vector2.zero;
        moving = false;

        anim.SetFloat("velocity", input.magnitude);

        if (input.magnitude > 0.1f) {
            input = Vector3.Normalize(input);
            moving = true;
        }
        this.transform.forward = Vector3.SmoothDamp(this.transform.forward, new Vector3(input.x, 0, input.y), ref rotationSmoothing, 0.02f);

        float targetVelocityX = Vector2.SqrMagnitude(input) * moveSpeed;
        velocity.x = Mathf.Abs(velocity.x);
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (physics.collisions.below) ? acclerationTimeGrounded : accelerationTimeAirborn);

        velocity.y += gravity * Time.deltaTime;
        physics.Move(velocity * Time.deltaTime);
    }

    [HideInInspector]public bool isJumping;
    public void jump() {
        isJumping = true;
        velocity.y = jumpVelocity;
        physics.Move(velocity * Time.deltaTime);
        GetComponent<audioManager>().Play(jumpFX);
    }
}

