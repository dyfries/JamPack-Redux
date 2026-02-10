using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Player Movement Script
    This script gives movement to an object when input is pressed.
    It includes 3 different types of movement; WorldForce, RelativeForce, and Constant
    There is an enum for these different movement types
    Has floats for: x & y input, acceleration, defualt speed and max speed, as well as, rotation speed and jump force
    Bools for: locking y input, enabling jumping, and isJumping which is for jump logic
    
 */
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private enum MovementType { WorldForce, RelativeForce, Constant }
    //WorldForce will add force relative to the world; up is up, left is left
    //Good for platformer movement if you lock yInput and enableJump

    //RelativeForce will add force relative to the player's direction; up is forward, left will turn to the left
    //Good for ship/tank controls

    //Constant doesn't add force, instead it directly sets the player's velocity, direction is relative to the world
    //Good for top down movement, don't use gravity if using this type



    //This determines which type of movement is applied to the character
    [SerializeField]
    private MovementType movementType = MovementType.WorldForce;

    [SerializeField] 
    private float xInput = 0;

    [SerializeField]
    private float yInput = 0;

    [SerializeField]
    private float acceleration = 5f;


    //this will get set to the acceleration on Start
    //this is so that if the acceleration is affected by something on the outside,
    //it can easily be set back to the default
    private float defaultSpeed;


    [SerializeField]
    private float maxSpeed = 10f;  

    [SerializeField]
    private float rotationSpeed = 5f;

    [SerializeField]
    //This will prevent the player from adding input in the y direction if true
    private bool lockYInput = false;

    [SerializeField]
    private bool enableJump = false;

    private bool isJumping = false;

    [SerializeField]
    private float jumpForce = 5f;

    //rigidbody reference
    //will be set at the start of the game
    private Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        // This is get a reference to the local Rigidbody
        rigid = GetComponent<Rigidbody2D>();

        //setting the default speed to the acceleration at the start of the game.
        defaultSpeed = acceleration;

        //freezing rotation unless movement type is relative, which is the only type that needs rotation
        if (movementType != MovementType.RelativeForce)
            rigid.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");


        //will not change yInput if lockYInput is enabled.
        if (!lockYInput)
            yInput = Input.GetAxis("Vertical");

        else
            yInput = 0;

        //will only jump if enableJump is true
        if(enableJump && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
       
    }
  
    void FixedUpdate()
    {
        // The movement style that is applied to the player is determined by the movementType enum 

        switch (movementType)
        {
            case MovementType.WorldForce:
                MovePlayerWorld();
                break;

            case MovementType.RelativeForce:
                MovePlayerRelative();
                break;

            case MovementType.Constant:
                MovePlayerConstant();
                break;

        }
    }

    // add force relative to the world
    private void MovePlayerWorld()
    {

        //Movement input is combined into vector
        Vector2 worldForce = new Vector2(xInput, yInput);

        // movement vector is multiplied by acceleration  
        rigid.AddForce(worldForce * acceleration);

        ClampVelocity();

        
        //Adding impulse for jump
        if (isJumping) 
        {
            Vector2 jumpVector = new Vector2(0, 1);
            rigid.AddForce(jumpVector * jumpForce, ForceMode2D.Impulse);

            if (isJumping)
                isJumping = false;
        }
    }

    //add force relative to self
    private void MovePlayerRelative()
    {
        //no x input in this vector because x handles rotation
        Vector2 relativeForce = new Vector2(0, yInput);

        // The Add Relative Force uses the relative orientation of the physics object, so +Y means Forward       
        rigid.AddRelativeForce(relativeForce * acceleration);

        ClampVelocity();

        // Rotation
        // Add Torque to rotate the object
        rigid.AddTorque(rotationSpeed * xInput * -1f);

        

        //Jumping acts more like a dash with relative movement 
        if (isJumping)
        {
            Vector2 jumpVector = new Vector2(0, 1);
            rigid.AddRelativeForce(jumpVector * jumpForce, ForceMode2D.Impulse);

            if (isJumping)
                isJumping = false;
        }

    }

    //set velocity based on input
    //no jumping for this one because it can't use forces 
    private void MovePlayerConstant()
    {
        //This moves the player without using force
        // The velocity on the rigid body is set to the acceleration 
        // This makes movement very precise and snappy

        Vector2 movementVector = new Vector2(xInput, yInput);


        //directly setting velocity
        rigid.velocity = movementVector * acceleration;     


    }


    //Sets the velocity to the max speed if it goes over 
    private void ClampVelocity()
    {
        if (rigid.velocity.x < -maxSpeed)
        {
            rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
        }
        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        if (rigid.velocity.y < -maxSpeed)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, -maxSpeed);
        }
        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, maxSpeed);
        }
    }

    public float GetPlayerSpeed()
    {
        return acceleration;
    }
    
    public void SetPlayerSpeed(float speed)
    {
        acceleration = speed;
    }

    //no setter for defualt speed because it should never be changed
    public float GetPlayerDefaultSpeed()
    {
        return defaultSpeed;
    }
}
