using UnityEngine;
using RPG.Combat;
using RPG.Core;

namespace RPG.ControlF
{
    public class AiController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        GameObject player;
        Fighter fighter;
        Health health;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if (health.IsDead()) return;

            if (InRange() && fighter.canAttack(player))
            {
                GetComponent<Fighter>().Attack(player);
            }
            else
            {
                fighter.Cancel();
            }
        }

        private bool InRange()
        {
            return Vector3.Distance(transform.position, player.transform.position) < chaseDistance;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
