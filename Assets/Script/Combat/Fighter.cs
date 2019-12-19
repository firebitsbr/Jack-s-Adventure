using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Combat;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        Transform target;
        float timeInterval = 0;
        [SerializeField] float range = 2f;
        [SerializeField] float attackTime = 1f;
        [SerializeField] float weaponDmg = 20f;

        private void Update()
        {
            timeInterval += Time.deltaTime;
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
            if (timeInterval > attackTime)
            {
                GetComponent<Animator>().SetTrigger("attack");
                timeInterval = 0;
                Health health = target.GetComponent<Health>();
                health.TakeDamage(weaponDmg);
            }

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