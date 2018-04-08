using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    public bool playerNear = false;
    public bool ballStopped;
    public float velocity = 1f;

    private Transform wallHitPoint; //With raycast we will see the point where the ball will collide;
    int wallMask;
    Vector3 direction;

    Ray frontRay;
    RaycastHit hitPoint;
    public float range = 200f;

    void Awake()
    {
        wallMask = LayerMask.GetMask("Walls");
    }

	void Start () {
        ballStopped = true;
        direction = transform.forward;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void FixedUpdate()
    {
        direction = Vector3.Normalize(transform.forward);
        //direction = Vector3.Normalize(transform.forward);
        if(ballStopped == false)
        {
            frontRay.origin = transform.position;
            frontRay.direction = direction;

            if(Physics.Raycast (frontRay, out hitPoint, range, wallMask))
            {
                print(hitPoint.point);

            }
            else
            {
                //We can use this part to put the ball inside the map if, for any reason, it leaves the area.
                print("no choca");
            }

            //It works this way, but Vector3.forward is (0,0,1)... WHY????????????????????
            //transform.Translate(Vector3.forward * velocity * Time.deltaTime);
            transform.Translate(direction * velocity * Time.deltaTime, Space.World);
           
        }
    }



    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            print("el jugador está cerca");
            playerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            print("el jugador sale");
            playerNear = false;
        }
    }

 

    public void changeDirection(Quaternion _rotation)
    {
        transform.rotation = _rotation;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + direction * 2);
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, hitPoint.point);
    }


}
