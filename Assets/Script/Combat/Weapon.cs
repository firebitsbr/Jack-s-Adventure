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
        [SerializeField] bool isRightHand = true;

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