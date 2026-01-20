
using UnityEngine;

// This script moves an object Transform over time
// in a fixed direction. 
// Doesn't work with Rigidbodies, so just for things like background elements. 
public class SimpleMover : MonoBehaviour
{
    [Header("--- Control Flags ---")]
    public bool enableMovement = true;
    public bool enableRotation = true;

    [Header(" --- Settings for Movement --- ")]
    // Speed - float
    public float speed = 1.0f;
    // Direction
    public Vector2 direction = Vector2.up;
    // Local vs World Space
    public Space space = Space.Self;

    [Header(" --- Settings for Rotation --- ")]
    // Rotation Speed
    public float rotationSpeed = 1.0f;

    // Update is called once per frame
    void Update()
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

    // Rotate the object by speed units per second in direction.
    // Direction should be a length 1 vector to avoid weird outcomes, 
    // otherwise it will multiply with speed. 
    private void MoveObject()
    {
        //Vector3.up
        Vector2 movementVector = direction * Time.deltaTime * speed;
        // Without deltatime, we move 1 Unit per frame
        // with deltatime, we move 1 unit per second
        transform.Translate(movementVector, space);
    }

    // Rotate the object by rotationSpeed degrees per second counterclockwise. 
    private void RotateObject()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
    }
}
