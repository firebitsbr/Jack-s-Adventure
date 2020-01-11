using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Jack's Adventure/Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController weaponOveride = null;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] float range = 2f;
        [SerializeField] float weaponDmg = 20f;

        public void spawn(Transform handTransform, Animator animator)
        {
            if (weaponPrefab != null)
            {
                Instantiate(weaponPrefab, handTransform);
            }
            if (weaponOveride != null)
            {
                animator.runtimeAnimatorController = weaponOveride;

            }
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