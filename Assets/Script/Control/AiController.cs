using UnityEngine;
using RPG.Combat;

namespace RPG.ControlF
{
    public class AiController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        GameObject player;
        Fighter fighter;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
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
    }
}
