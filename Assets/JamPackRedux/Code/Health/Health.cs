
using UnityEngine;
using UnityEngine.Events;


    /// <summary>
    /// A class for objects with hit point values.
    /// </summary>
    public class Health : MonoBehaviour
    {
        [Header("Health Values")]
        [Tooltip("The Maximum HP this object can have. Also sets the starting HP of the object.")]
        public float maxHP = 3;
        private float currentHP = 1;
        
        [Header("Death Behaviours")]
        [Tooltip("Determines what will happen to the object when its health reaches 0.")]
        [SerializeField] private DeathBehaviour deathBehaviour;
        private enum DeathBehaviour
        {
            DestroyOnDeath,
            DisableOnDeath,
            ResetOnDeath,
            DoNothingOnDeath,
        }
        
        [Header("Events")]
        [Tooltip("The event that will run when the object reaches 0 HP.")]
        public UnityEvent OnDeath = new UnityEvent();
        
        [Tooltip("The event that will run when the object takes damage.")]
        public UnityEvent OnDamage = new UnityEvent();

        [Header("Settings")] 
        [Tooltip("Show Debug messages in the console.")]
        public bool DEBUG_MODE = false;
        

        // Start is called before the first frame update
        void Awake()
        {
            // Ensure our currentHP equals our maxHP 
            currentHP = maxHP;
        }

        /// <summary>
        /// Applies damage to currentHP, then checks if it has been reduced below 0. Runs DeathFromDamage() if it has.
        /// </summary>
        /// <param name="damage">The damage applied to currentHP.</param>
        /// <returns>Whether currentHP has been reduced to 0 or lower.</returns>
        public bool TakeDamage(float damage)
        {
            // Apply damage.
            currentHP -= damage;

            // In debug mode, print a message in the console letting us know the value of currentHP.
            if (DEBUG_MODE)
            {
                Debug.Log(currentHP);
            }

            // Invoke the OnDamage Event
            OnDamage.Invoke();
            
            // Check if currentHP is still above 0.
            if (currentHP > 0)
            {
                // If so, return false.
                return false;
            }
            else
            {
                // If not, run Death() and return true;
                Death();
                return true;
            }
        }

        /// <summary>
        /// Destroys the gameObject and runs any on-death effects.
        /// </summary>
        private void Death()
        {
            OnDeath.Invoke();
            switch (deathBehaviour)
            {
                case DeathBehaviour.DestroyOnDeath:
                    Destroy(gameObject);
                    break;
                case DeathBehaviour.ResetOnDeath:
                    ResetHealth();
                    break;
                case DeathBehaviour.DisableOnDeath:
                    gameObject.SetActive(false);
                    break;
            }
        }

        /// <summary>
        /// Runs any on death effects. Can be called from anywhere if need be.
        /// </summary>
        public void DeathFromOtherMeans()
        {
            Death();
        }
        
        
        /// <summary>
        /// Returns the current health property of the component.
        /// </summary>
        /// <returns></returns>
        public float GetCurrentHealth()
        {
            return currentHP;
        }

        /// <summary>
        /// Resets health to the max value when called.
        /// </summary>
        public void ResetHealth()
        {
            currentHP = maxHP;
        }
        
    }

