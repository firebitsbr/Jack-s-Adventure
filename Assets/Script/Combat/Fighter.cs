using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        Transform target;
        [SerializeField] float range = 2f;

        private void Update()
        {
            if (target == null) return;

            if (GetRange())
            {
                GetComponent<Mover>().moveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                StartAttack();
            }
        }

        private void StartAttack()
        {
            GetComponent<Animator>().SetTrigger("attack");
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionSchedular>().StartAction(this);
            target = combatTarget.transform;
        }

        private bool GetRange()
        {
            return Vector3.Distance(transform.position, target.position) >= range;
        }

        public void Cancel()
        {
            target = null;
        }

        void Hit()
        {

        }
    }
}