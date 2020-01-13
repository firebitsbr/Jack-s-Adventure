using System;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] Transform targetTransform = null;
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

        private Vector3 getAimLocation()
        {
            CapsuleCollider targetCollider = targetTransform.GetComponent<CapsuleCollider>();
            if (targetCollider == null)
            {
                return targetTransform.position;
            }
            print(targetCollider.height * Vector3.up /2);
            return targetTransform.position + Vector3.up * targetCollider.height / 2;
        }
    }
}