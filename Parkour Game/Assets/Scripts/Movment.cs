using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    CharacterController controller;

    public Transform groundcheck;
    public LayerMask groundMask;
    public LayerMask wallMask;

    Vector3 move;
    Vector3 input;
    Vector3 Yvelocity;
    Vector3 forwardDirection;

    int jumpcharges;
    bool isGrounded;
    bool isSprinting;
    bool isCrouching;
    bool isSliding;
    bool isWallrunning;
    public float slidespeedincrease;
    public float slidespeeddecrease;
    public float wallrunspeedincrease;
    public float wallrunspeeddecrease;
    float speed;
    public float runspeed;
    public float crouchSpeed;
    public float sprintSpeed;
    public float airSpeed;
    float gravity;
    public float normalgravity;
    public float wallrungravity;
    public float jumpheight;
    float startHeight;
    float crouchHeight = 0.5f;
    Vector3 crouchingCenter = new Vector3(0,0.5f,0);
    Vector3 standingCenter = new Vector3(0,0,0);
    float slidetimer;
    public float maxslidetimer;
    bool onleftwall;
    bool onrightwall;
    RaycastHit leftwallhit;
    RaycastHit rightwallhit;
    Vector3 wallnormal;


    

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        startHeight = transform.localScale.y;
    }

    void IncreaseSpeed(float speedincrease){
        speed += speedincrease;
    }

    void DecreaseSpeed(float speeddecrease){
        speed -= speeddecrease * Time.deltaTime;
    }

    void CheckWallRun(){
        onleftwall = Physics.Raycast(transform.position, -transform.right, out leftwallhit, 0.7f, wallMask);
        onrightwall = Physics.Raycast(transform.position, transform.right, out rightwallhit, 0.7f, wallMask);
        
        if ((onrightwall || onleftwall) && !isWallrunning){
            WallRun();
        }
        if ((!onrightwall && !onleftwall) && isWallrunning){
            ExitWallRun();
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        CheckWallRun();
        if(isGrounded && !isSliding){
            GroundedMovment();
        }else if(!isGrounded && !isWallrunning){
            AirMovement();
        }else if(isSliding){
            SlideMovement();
            DecreaseSpeed(slidespeeddecrease);
            slidetimer -= 1f * Time.deltaTime;
            if(slidetimer < 0){
                isSliding = false;
            }
        }else if(isWallrunning){
            Debug.Log(jumpcharges);
            WallRunMovement();
            DecreaseSpeed(wallrunspeeddecrease);
        }
        checkGround();
        controller.Move(move * Time.deltaTime);
        ApplyGravity();
        
    }

    void HandleInput(){
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        input = transform.TransformDirection(input);
        input = Vector3.ClampMagnitude(input, 1f);

        if(Input.GetKeyDown(KeyCode.Space) && jumpcharges > 0){
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.LeftControl)){
            Crouch();
        }
        if(Input.GetKeyUp(KeyCode.LeftControl)){
            ExitCrouch();
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && isGrounded){
            isSprinting = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)){
            isSprinting = false;
        }
    }

    void SlideMovement(){
        move += forwardDirection;
        move = Vector3.ClampMagnitude(move, speed);
    }

    void GroundedMovment(){
        speed = isSprinting ? sprintSpeed : isCrouching ? crouchSpeed : runspeed;
        if(input.x != 0){
            move.x += input.x * speed;
        }else{
            move.x = 0;
        }
        if(input.z != 0){
            move.z += input.z * speed;
        }else{
            move.z = 0;
        }

        move = Vector3.ClampMagnitude(move, speed);
    }


    void Jump(){
        if(!isGrounded && !isWallrunning){
            jumpcharges -= 1;
        }
        else if(isWallrunning){
            ExitWallRun();
            IncreaseSpeed(wallrunspeedincrease);
        }
        Yvelocity.y = Mathf.Sqrt(jumpheight * -2f * normalgravity);
    }


    void checkGround(){
        isGrounded = Physics.CheckSphere(groundcheck.position, 0.2f, groundMask);
        if(isGrounded){
            jumpcharges = 1;
        }
    }

    void ApplyGravity(){
        gravity = isWallrunning ? wallrungravity : normalgravity;
        Yvelocity.y += gravity * Time.deltaTime;
        controller.Move(Yvelocity * Time.deltaTime);
    }

    void AirMovement(){
        move.x += input.x * airSpeed;
        move.z += input.z * airSpeed;

        move = Vector3.ClampMagnitude(move, speed);
    }

    void Crouch(){
        controller.height = crouchHeight;
        controller.center = crouchingCenter;
        transform.localScale = new Vector3(transform.localScale.x, crouchHeight, transform.localScale.z);
        isCrouching = true;
        if(speed >= runspeed && move != Vector3.zero){ //make > to >= to make it slide even if walking not running but fix sliding when standing still
            isSliding = true;
            forwardDirection = input; //change input to transfomr.forward to make player always slide forward
            if(isGrounded){
                IncreaseSpeed(slidespeedincrease);
            }
            slidetimer = maxslidetimer;
        }
    }

    void ExitCrouch(){
        controller.height = (startHeight * 2);
        controller.center = standingCenter;
        transform.localScale = new Vector3(transform.localScale.x, startHeight, transform.localScale.z);
        isCrouching = false;
        isSliding = false;
    }

    void WallRun(){
        isWallrunning = true;
        jumpcharges = 1;
        IncreaseSpeed(wallrunspeedincrease);
        Yvelocity = new Vector3(0f,0f,0f);

        forwardDirection = Vector3.Cross(wallnormal, Vector3.up);

        if(Vector3.Dot(forwardDirection, transform.forward) < 0){
            forwardDirection = -forwardDirection;
        }
    }

    void ExitWallRun(){
        isWallrunning = false;
    }

    void WallRunMovement(){
        if ((input.z > forwardDirection.z - 10f) && input.z < (forwardDirection.z + 10f)){
            move += forwardDirection;
        }else if((input.z < forwardDirection.z - 10f) && input.z > (forwardDirection.z + 10f)){
            move.x = 0f;
            move.z = 0f;
            ExitWallRun();
        }
        move.x += input.x * airSpeed;

        move = Vector3.ClampMagnitude(move, speed);
    }
}
