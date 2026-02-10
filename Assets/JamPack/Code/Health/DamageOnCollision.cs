using UnityEngine;

    /// <summary>
    /// A class for objects capable of applying damage to other objects on collision.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class DamageOnCollision : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("The Amount of Damage this object will deal when colliding with another object.")]
        public float damageToDeal = 1f;
        
        [Tooltip("Show Debug messages in the console.")]
        public bool DEBUG_MODE = false;
    
        // ---- COLLISION EFFECTS ----
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Find the collision object's health component.
            Health collisionObjectHealth = collision.gameObject.GetComponent<Health>();
            
            
            // If the object has a health component, apply damage to it.
            if (collisionObjectHealth != null)
            {
                collisionObjectHealth.TakeDamage(damageToDeal);
            }
    
            
            // In debug mode, print a message to the console letting us know a collision has occured.
            if (DEBUG_MODE) 
            { 
                Debug.Log("Hit Detected"); 
            }
        } 
    }


