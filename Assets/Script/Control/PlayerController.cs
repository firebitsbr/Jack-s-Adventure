using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;

namespace RPG.Control
{

    public class PlayerController : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
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
                if (Input.GetMouseButtonDown(1))
                {
                    if (GetComponent<Fighter>().canAttack(targ))
                    {
                        GetComponent<Fighter>().Attack(targ);
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