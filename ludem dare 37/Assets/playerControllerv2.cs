using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class playerControllerv2 : MonoBehaviour {

    public float moveSpeed = 5;
    protected Rigidbody rigid;
    protected Vector3 rotationSmoothing;
    protected float accelerationTimeAirborn = 0.2f;
    protected float acclerationTimeGrounded = 0.1f;
    protected float velocitySmoothing;

    public static playerControllerv2 instance;
    public bool active = true;

    protected float velocity;
    public float jumpForce = 10;
    [HideInInspector]public bool isMoving;

    public AudioClip jumpFX;
    protected float distFromGround;
    [HideInInspector] public bool isJumping;

    public Animator anim;

    void Awake() {
        anim = GetComponentInChildren<Animator>();
        instance = this;
        distFromGround = GetComponent<Collider>().bounds.extents.y;
        rigid = GetComponent<Rigidbody>();
    }

    void Update() {
        isJumping = false;

        Vector3 input = new Vector2(-Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal"));
        if (input.magnitude > 0.1f && active) {
            input = Vector3.Normalize(input);
            movement(input);
        }
        this.transform.forward = direction;
    }

    [HideInInspector] public Vector3 direction;
    protected void movement(Vector3 input) {
        float targetVelocityX = Vector2.SqrMagnitude(input) * moveSpeed;
        direction = Vector3.SmoothDamp(direction, new Vector3(input.x, 0, input.y), ref rotationSmoothing, 0.01f);
        velocity = Mathf.SmoothDamp(velocity, targetVelocityX, ref velocitySmoothing, acclerationTimeGrounded);
        rigid.velocity = new Vector3(this.transform.forward.x * velocity, rigid.velocity.y, this.transform.forward.z * velocity);
    }

    void LateUpdate() {
        Vector3 input = new Vector2(-Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("velocity", input.magnitude);
    }
}
