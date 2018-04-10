using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prueba2 : MonoBehaviour {

    public Transform target;
    void Update()
    {
        Vector3 relativePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = rotation;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);
    }


}
