
using UnityEngine;
using UnityEngine.Events;


    /// <summary>
    /// A class for objects with hit point values.
    /// </summary>
    public class Health : MonoBehaviour
    {
        [Header("Health Values")]
        public float maxHP = 3;
        private float currentHP = 1;
        
        [Header("Death Behaviours")]
        [SerializeField] private DeathBehaviour deathBehaviour;
        private enum DeathBehaviour
        {
            DestroyOnDeath,
            DisableOnDeath,
            ResetOnDeath
        }
        
        [Header("Events")]
        public UnityEvent OnDeath = new UnityEvent();
        public UnityEvent OnDamage = new UnityEvent();

        [Header("Settings")] 
        public bool DEBUG_MODE = false;
        

        // Start is called before the first frame update
        void Start()
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
            OnDamage.Invoke();
            currentHP -= damage;

            // In debug mode, print a message in the console letting us know the value of currentHP.
            if (DEBUG_MODE)
            {
                Debug.Log(currentHP);
            }

            // Check if currentHP is still above 0.
            if (currentHP > 0)
            {
                // If so, return false.
                return false;
            }
            else
            {
                // If not, run DeathFromDamage() and return true;\
                DeathFromDamage();
                return true;
            }
        }

        /// <summary>
        /// Destroys the gameObject and runs any on-death effects.
        /// </summary>
        private void DeathFromDamage()
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

