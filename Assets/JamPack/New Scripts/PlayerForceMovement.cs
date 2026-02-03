using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * In this Demo we will look at using the AddRelativeForce method and Add Torque method to 
 * make a player character that will move relative to its own orientation, rather then the world orientation. 
 * This is typical of games such as Asteroids, or racing/driving/flying games. 
 */
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerForceMovement : MonoBehaviour
{
    private enum MovementType { WorldForce, RelativeForce, Constant }

    [SerializeField]
    private MovementType movementType = MovementType.WorldForce;

    [SerializeField] // This will allow us to see our private variables, without being able to access them. 
    private float xInput = 0;
    [SerializeField]
    private float yInput = 0;

    [SerializeField]
    private float acceleration = 5f;

    [SerializeField]
    private float maxSpeed = 10f;

    [SerializeField]
    private float rotationSpeed = 5f;

    // Physics References
    // Note: I used Private here as we don't want people assigning this value in the editor, 
    //      We will grab the reference with code in the Start Method
    private Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        // This is get a reference to the local Rigidbody
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }

    // Physics Update: Provide Forces in this loop instead. 
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

    // Move the player relative to the world
    private void MovePlayerWorld()
    {
        // Add Force to the object in the world direction & orientation. 
        // This is useful if you want to have a simple control scheme where the input directly 
        //   relates to the movement in "World Space". This is useful for top down games,
        //   and can be manipulated into other genres (beat em up, fighting, etc)

        //Movement input is combined into vector
        Vector2 worldForce = new Vector2(xInput, yInput);

        // movement vector is multiplied by acceleration
        // force is not applied if velocity is greater than the max speed
        if (Mathf.Abs(rigid.velocity.magnitude) < maxSpeed)
            rigid.AddForce(worldForce * acceleration);
    }

    private void MovePlayerRelative()
    {
        // Add Force to the object in the direction it is facing. Ignore Strafe movement for now

        Vector2 relativeForce = new Vector2(0, yInput);

        // The Add Relative Force uses the relative orientation of the physics object, so +Y means Forward
        // Is also clamped by max speed
        if (Mathf.Abs(rigid.velocity.magnitude) < maxSpeed)
            rigid.AddRelativeForce(relativeForce * acceleration);


        // Rotation
        // Add Torque to rotate the object
        // In 2D, the only axis we can rotate on is Z (think of rotating AROUND that axis)
        // Note the xInput goes from (-1,+1)
        // the -1f multiplier is because by default we will rotate the wrong way. 
        rigid.AddTorque(rotationSpeed * xInput * -1f);

    }

    private void MovePlayerConstant()
    {
        //This moves the player without using force
        // The velocity on the rigid body is set to the acceleration 
        // This makes movement very precise and snappy

        Vector2 movementVector = new Vector2(xInput, yInput);

        rigid.velocity = movementVector * acceleration;
    }

    public float GetPlayerSpeed()
    {
       return rigid.velocity.magnitude;
    }

    public void SetPlayerSpeed(float speed)
    {
       acceleration = speed;
    }
}
