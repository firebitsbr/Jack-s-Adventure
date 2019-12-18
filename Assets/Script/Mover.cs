﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target; 

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1)){
            moveOnClick();
        }

       
    }

    private void moveOnClick(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        bool hasHit = Physics.Raycast(ray, out hit);
        if(hasHit){
            GetComponent<NavMeshAgent>().destination = hit.point;
        }
    }
}
