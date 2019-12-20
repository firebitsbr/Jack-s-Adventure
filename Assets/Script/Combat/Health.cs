using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        float health = 100f;

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            if (health <= 0)
            {
                GetComponent<Animator>().SetTrigger("die");
            }
            print(health);
        }
    }
}