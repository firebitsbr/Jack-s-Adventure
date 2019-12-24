using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;

        private void Start()
        {
            health = GetComponent<Health>();
        }
        // Update is called once per frame
        private void Update()
        {
            if (health.IsDead()) return;
            if (combatMovement()) return;
            if (mouseClickMovement()) return;
        }

        private bool combatMovement()
        {
            RaycastHit[] hitResult = Physics.RaycastAll(getMouseRay());
            foreach (RaycastHit item in hitResult)
            {
                CombatTarget targ = item.transform.GetComponent<CombatTarget>();
                if (targ == null) continue;
                if (Input.GetMouseButton(1))
                {
                    if (GetComponent<Fighter>().canAttack(targ.gameObject))
                    {
                        GetComponent<Fighter>().Attack(targ.gameObject);
                    }
                }
                return true;
            }
            return false;
        }

        private bool mouseClickMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(getMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(1))
                {
                    Debug.DrawLine(getMouseRay().origin, hit.point);
                    GetComponent<Mover>().StartMovement(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray getMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}