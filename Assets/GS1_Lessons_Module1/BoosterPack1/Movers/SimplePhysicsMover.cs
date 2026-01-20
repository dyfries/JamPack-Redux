
using UnityEngine;

// This script moves an object Rigidbody using force over time
// in a fixed direction, or adds torque. 

// Requires a Rigidbody2D and a collider (of any type)
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class SimplePhysicsMover : MonoBehaviour
{
    [Header("--- Control Flags ---")]
    public bool enableMovement = true;
    public bool enableRotation = true;

    [Header(" --- Settings for Movement --- ")]
    // Speed - float
    public float force = 1.0f;
    // Direction
    public Vector2 direction = Vector2.up;

    // Enum to select the movement type. 
    // Relative moves relative to the current object orientation, 
    // World moves relative to the world. 
    // They will look identical if your object is not rotated. 
    public Space space = Space.Self;

    [Header(" --- Settings for Rotation --- ")]
    // Rotation Speed
    public float rotationTorque = 1.0f;

    // Rigidbody reference
    private Rigidbody2D rigid;

    public void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enableMovement)
        {
            MoveObject();
        }

        if (enableRotation)
        {
            RotateObject();
        }

    }

    // Moves the object using forces. 
    private void MoveObject()
    {
        //Vector3.up
        Vector2 movementVector = direction * force;

        // We don't need deltaTime here. 
        // World space will move relative to the world, self will move relative to the objects own rotation. 

        if (space == Space.World)
        {
            rigid.AddForce(movementVector);
        }
        else if (space == Space.Self)                // * Note: I don't need the if here its just here to demonstrate we are in "Self" space. 
        {
            rigid.AddRelativeForce(movementVector);
        }

        // Non physics version for comparison: 
        // transform.Translate(movementVector);
    }

    private void RotateObject()
    {   
        
        rigid.AddTorque(rotationTorque);

        // Just for comparison to the Non physics version. We don't need a vector because the
        // 2D Rigidbody only rotates around the Z axis. 
        //transform.Rotate(new Vector3(0, 0, rotationTorque) * Time.deltaTime);
    }
}
