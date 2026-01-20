
using UnityEngine;

// This script is meant to be called by a UnityEvent and allows you to add arbitrary forces in different ways. 
// It won't do anything on it's own. 

// This script requires both a 2D Rigidbody and Collider2D (Any type)
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PhysicsBooster : MonoBehaviour
{
    private Rigidbody2D rigid;

    [Header("Movement Settings")]
    // This is the direction and magnitude (amount)
    public Vector2 addForceAmount = Vector2.one;

    [Header("Rotation Settings")]
    // Rotational force to add. Positive values rotate counterclockwise. 
    public float addTorqueAmount = 1.0f;

    [Header("Drag Settings")]
    // I provided two options for each as you might want to toggle between them (e.g brakes)
    public float dragSettingA = 1.0f;
    public float dragSettingB = 2.0f;

    public float angularDragSettingA = 1.0f;
    public float angularDragSettingB = 2.0f;


    [Header("Physics Settings")]
    // World vs Local Space. 
    public Space space = Space.World; 
    // ForceMode.Force will add force over time (force / fixed frame rate)
    // ForceMode.Impulse will add force all at once on one frame. 
    // Impulse is useful for jumping, launching things but Force is the default (usually)
    public ForceMode2D forceMode = ForceMode2D.Impulse;

    // Grab the reference to the Rigidbody. Your GameObject must contain it because of the RequireComponent above. 
    public void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // --- Methods that will fire to add force ---

    // Call this method from a UnityEvent.
    // Two settings are useful here: The space is (World/Local) and the forceMode (Force/Impulse)
    public void FirePhysicsBoost()
    {
        if (space == Space.World)
        {
            rigid.AddForce(addForceAmount, forceMode);
        }
        else if (space == Space.Self)
        {
            rigid.AddRelativeForce(addForceAmount, forceMode);
        }
    }

    // Adds Torque to an object. 
    public void FirePhysicsTorque()
    {
        rigid.AddTorque(addTorqueAmount, forceMode);
    }
    
    // --- Toggles for Drag settings ---
    // Drag Settings for movement. 
    public void SetDragA()
    {
        rigid.drag = dragSettingA;
    }
    public void SetDragB()
    {
        rigid.drag = dragSettingB;
    }

    // Drag Settings for Rotation
    public void SetAngularDragA()
    {
        rigid.angularDrag = angularDragSettingA;
    }

    public void SetAngularDragB()
    {
        rigid.angularDrag = angularDragSettingB;
    }

}
