using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Jack's Adventure/Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController weaponOveride = null;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] float range = 2f;
        [SerializeField] float weaponDmg = 20f;
        [SerializeField] bool isRightHand = true;
        [SerializeField] Projectile projectile = null;

        public void spawn(Transform rightHandTransform, Transform leftHandTransform, Animator animator)
        {
            if (weaponPrefab != null)
            {
                Transform handTransform = isRightHand ? rightHandTransform : leftHandTransform;
                Instantiate(weaponPrefab, handTransform);
            }
            if (weaponOveride != null)
            {
                animator.runtimeAnimatorController = weaponOveride;

            }
        }

        public bool hasProjectile()
        {
            return projectile != null;
        }

        public void launchProjectile(Transform rightHandTransform, Transform leftHandTransform, Health target)
        {
            Transform handTransform = isRightHand ? rightHandTransform : leftHandTransform;

            Projectile projectileItem = Instantiate(projectile, handTransform.position, Quaternion.identity);
            projectileItem.setTarget(target, weaponDmg);
        }

        public float getRange()
        {
            return range;
        }

        public float getDmamage()
        {
            return weaponDmg;
        }
    }
}