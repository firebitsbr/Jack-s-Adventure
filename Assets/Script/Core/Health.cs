using UnityEngine;
using RPG.Saving;

namespace RPG.Core
{
    public class Health : MonoBehaviour, Saveable
    {
        [SerializeField] float health = 100f;
        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            if (health <= 0 && !isDead)
            {
                death();
            }
        }

        private void death()
        {
            GetComponent<Animator>().SetTrigger("die");
            isDead = true;
            GetComponent<ActionSchedular>().CancelCurrentAction();
        }
        public object captureState()
        {
            return health;
        }

        public void restoreState(object state)
        {
            health = (float)state;

            if (health == 0)
            {
                death();
            }
        }
    }
}