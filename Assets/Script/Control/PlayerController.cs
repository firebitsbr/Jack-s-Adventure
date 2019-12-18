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
            combatMovement();
            mouseClickMovement();

        }

        private void combatMovement()
        {
            RaycastHit[] hitResult = Physics.RaycastAll(getMouseRay());
            foreach (RaycastHit item in hitResult)
            {
                Target targ = item.transform.GetComponent<Target>();
                if (targ == null)
                {
                    continue;
                }
                if (Input.GetMouseButtonDown(1))
                {
                    GetComponent<Fighter>().Attact(targ);
                }
            }
        }

        private void mouseClickMovement()
        {
            if (Input.GetMouseButton(1))
            {
                moveOnClick();
            }
        }

        private void moveOnClick()
        {
            RaycastHit hit;

            bool hasHit = Physics.Raycast(getMouseRay(), out hit);
            if (hasHit)
            {
                GetComponent<Mover>().moveTo(hit.point);
            }
        }

        private static Ray getMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}