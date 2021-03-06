using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        Health target;
        float timeInterval = Mathf.Infinity;

        [SerializeField] float attackTime = 1f;

        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;

        Weapon currentWeapon = null;


        private void Start()
        {
            SpawnWeapon(defaultWeapon);
        }

        private void Update()
        {
            timeInterval += Time.deltaTime;
            if (target == null) return;

            if (target.IsDead())
            {
                Cancel();
                return;
            }

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

        public void SpawnWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.spawn(rightHandTransform, leftHandTransform, animator);
        }

        private void StartAttack()
        {
            if (timeInterval > attackTime)
            {
                GetComponent<Animator>().SetTrigger("attack");
                timeInterval = 0;
            }

        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionSchedular>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
            transform.LookAt(target.transform);

        }

        public bool canAttack(GameObject targ)
        {
            Health currentTarget = targ.GetComponent<Health>();
            if (currentTarget.IsDead())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool GetRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) >= currentWeapon.getRange();
        }

        public void Cancel()
        {
            target = null;
            GetComponent<Mover>().Cancel();
        }

        void Hit()
        {
            if (target == null)
            {
                return;
            }


            if (currentWeapon.hasProjectile())
            {

                currentWeapon.launchProjectile(leftHandTransform, rightHandTransform, target);
            }
            else
            {
                target.TakeDamage(currentWeapon.getDmamage());
            }

        }

        void Shoot()
        {
            Hit();
        }
    }
}