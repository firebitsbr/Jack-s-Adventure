using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1)){
            moveOnClick();
        }
        updateAnimator();
    }

    private void moveOnClick(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        bool hasHit = Physics.Raycast(ray, out hit);
        if(hasHit){
            GetComponent<NavMeshAgent>().destination = hit.point;
        }
    }

    private void updateAnimator(){
        Vector3 vel = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVel = transform.InverseTransformDirection(vel);
        float speed = localVel.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }
}
