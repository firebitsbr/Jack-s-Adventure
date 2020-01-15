using System;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        Health targetTransform = null;
        [SerializeField] float arrowSpeed = 1;

        private void Update()
        {
            if (targetTransform == null)
            {
                return;
            }
            transform.LookAt(getAimLocation());
            transform.Translate(Vector3.forward * Time.deltaTime * arrowSpeed);
        }

        public void setTarget(Health targ)
        {
            targetTransform = targ;
        }

        private Vector3 getAimLocation()
        {
            CapsuleCollider targetCollider = targetTransform.GetComponent<CapsuleCollider>();
            if (targetCollider == null)
            {
                return targetTransform.transform.position;
            }
            Vector3 height = new Vector3(0, 1.5f, 0);
            return targetTransform.transform.position + Vector3.up * height.y;
        }
    }
}