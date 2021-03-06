﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Saving;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, Saveable
    {
        NavMeshAgent agent;
        Health health;
        [SerializeField] float maxSpeed = 7f;

        private void Start()
        {
            health = GetComponent<Health>();
            agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            agent.enabled = !health.IsDead();
            updateAnimator();
        }

        public void Cancel()
        {
            agent.isStopped = true;
        }

        public void StartMovement(Vector3 dest)
        {
            GetComponent<ActionSchedular>().StartAction(this);
            moveTo(dest);
        }

        public void moveTo(Vector3 dest)
        {
            agent.destination = dest;
            agent.isStopped = false;
        }

        public void setSpeed(float speed)
        {
            agent.speed = speed;
        }

        private void updateAnimator()
        {
            Vector3 vel = agent.velocity;
            Vector3 localVel = transform.InverseTransformDirection(vel);
            float speed = localVel.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

        public object captureState()
        {
            return new SerializableVector(transform.position);
        }

        public void restoreState(object state)
        {
            SerializableVector position = (SerializableVector)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = position.toVector();
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<ActionSchedular>().CancelCurrentAction();
        }
    }
}
