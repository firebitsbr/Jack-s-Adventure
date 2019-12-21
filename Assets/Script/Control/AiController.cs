using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class AiController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float wanderTime = 3.5f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float pointDistance = 3f;
        [SerializeField] float dwellingTime = 2f;

        GameObject player;
        Fighter fighter;
        Health health;
        Mover mover;
        Vector3 guardLocation;
        float timeSinceLastPlayer = Mathf.Infinity;
        float timeSinceLastPoint = Mathf.Infinity;
        int currentPointIndex = 0;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            guardLocation = transform.position;
        }

        private void Update()
        {
            if (health.IsDead()) return;

            if (InRange() && fighter.canAttack(player)) //Attacking
            {
                timeSinceLastPlayer = 0;
                GetComponent<Fighter>().Attack(player);
            }
            else if (timeSinceLastPlayer < wanderTime) //Wandering
            {
                GetComponent<ActionSchedular>().CancelCurrentAction();
            }
            else
            {
                Patrolling();//Patrolling

            }
            timeSinceLastPlayer += Time.deltaTime;
            timeSinceLastPoint += Time.deltaTime;
        }

        private void Patrolling()
        {
            Vector3 nextPosition = guardLocation;
            if (patrolPath != null)
            {
                if (AtPoint())
                {
                    TravelPoint();
                    timeSinceLastPoint = 0;
                }
                nextPosition = GetCurrentPoint();
            }
            if (timeSinceLastPoint > dwellingTime)
            {
                mover.StartMovement(nextPosition);
            }
        }

        private bool AtPoint()
        {
            float distanceToPoint = Vector3.Distance(transform.position, GetCurrentPoint());
            return distanceToPoint < pointDistance;
        }

        private void TravelPoint()
        {
            currentPointIndex = patrolPath.getNextIndex(currentPointIndex);
        }

        private Vector3 GetCurrentPoint()
        {
            return patrolPath.getPoint(currentPointIndex);
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
