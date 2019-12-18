using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {

        // Update is called once per frame
        void Update()
        {

            updateAnimator();
        }



        public void moveTo(Vector3 dest)
        {
            GetComponent<NavMeshAgent>().destination = dest;
        }

        private void updateAnimator()
        {
            Vector3 vel = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVel = transform.InverseTransformDirection(vel);
            float speed = localVel.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}
