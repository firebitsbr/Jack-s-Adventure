using UnityEngine;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        Transform target;
        [SerializeField] float range = 2f;

        private void Update()
        {
            if (target != null && GetRange())
            {
                GetComponent<Mover>().moveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Stop();
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
        }

        private bool GetRange(){
            return Vector3.Distance(transform.position, target.position) >= range);
        }

        public void Cancel(){
            target = null;
        }
    }
}