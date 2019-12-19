using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        NavMeshAgent agent;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            updateAnimator();
        }

        public void Cancel()
        {
            agent.isStopped = true;
        }

        public void StartMovement(Vector3 dest)
        {
            GetComponent<ActionSchedular>().StartAction(this);
            GetComponent<Fighter>().Cancel();
            moveTo(dest);
        }

        public void moveTo(Vector3 dest)
        {
            agent.destination = dest;
            if (agent.transform.position == dest)
            {
                Cancel();
            }
            agent.isStopped = false;
        }

        private void updateAnimator()
        {
            Vector3 vel = agent.velocity;
            Vector3 localVel = transform.InverseTransformDirection(vel);
            float speed = localVel.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}
