using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        Health target;
        float timeInterval = 0;
        [SerializeField] float range = 2f;
        [SerializeField] float attackTime = 1f;
        [SerializeField] float weaponDmg = 20f;

        private void Update()
        {
            timeInterval += Time.deltaTime;
            if (target == null) return;

            if (target.IsDead()) return;

            if (GetRange())
            {
                GetComponent<Mover>().moveTo(target.transform.position);
            }
            else
            {

                StartAttack();
                GetComponent<Mover>().Cancel();
            }
        }

        private void StartAttack()
        {
            if (timeInterval > attackTime)
            {
                GetComponent<Animator>().SetTrigger("attack");
                timeInterval = 0;
            }

        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionSchedular>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        private bool GetRange() 
        {
            return Vector3.Distance(transform.position, target.transform.position) >= range;
        }

        public void Cancel()
        {    
            // GetComponent<Animator>().SetTrigger("stopAttack");
            target = null;
        }

        void Hit()
        {
            target.TakeDamage(weaponDmg);
        }
    }
}