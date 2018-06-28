using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    public bool playerNear = false;
    public bool ballStopped;
    public float velocity = 1f;
    public float velocityIncrease = 1f;
    public float initialVelocity;

    private Transform wallHitPoint; //With raycast we will see the point where the ball will collide;
    int wallMask;
    Vector3 direction;
    public float BallHeight = 1;

    Ray frontRay;
    RaycastHit hitPoint;
    public float range = 200f;
    public Transform auxiliar;

    Vector3 reflection;

    public Material neutralMaterial, p1Material, p2Material;

    public GamMan gamMan;

    public bool hitted = false;
    public ParticleManager PM;

    void Awake()
    {
        wallMask = LayerMask.GetMask("Walls");
    }

	void Start () {
        ballStopped = true;
        direction = transform.forward;
        GetComponent<Renderer>().material = neutralMaterial;
        initialVelocity = velocity;
        
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void FixedUpdate()
    {
        #region adjusting values of position and rotation
        transform.position = new Vector3(transform.position.x, BallHeight, transform.position.z);
        transform.rotation = new Quaternion(0,transform.rotation.y,transform.rotation.z,transform.rotation.w);
        #endregion

        direction = Vector3.Normalize(transform.forward);
        //direction = Vector3.Normalize(transform.forward);
        if(ballStopped == false)
        {
            frontRay.origin = transform.position;
            frontRay.direction = direction;

            if(Physics.Raycast (frontRay, out hitPoint, range, wallMask))
            {
                
                

                #region Calculating new position and distance
                auxiliar.position=transform.position;
                auxiliar.Translate(direction * velocity * Time.deltaTime, Space.World);
                //print("Nuestra posición: "+transform.position+ "Sigiuente posicion: "+ auxiliar.position);
                float nextStepDist = Vector3.Distance(transform.position, auxiliar.position);
                float distToHit = Vector3.Distance(transform.position, hitPoint.point);
                //print("Distancia al sigiuente paso: " + nextStepDist);
                //print("Distancia al hit: " + distToHit);
                #endregion

                //print("Vector normal: " + hitPoint.normal);
                
                reflection =Vector3.Normalize(Vector3.Reflect(direction, hitPoint.normal));
                //print("Vector reflejado: "+reflection);

                if (nextStepDist >= distToHit)  //With the previous calculations, this will be the part of bouncing.
                {
                    shakeCameraWithWall();
                    Quaternion reflRotation = Quaternion.LookRotation(reflection);
                    //reflRotation.Set(reflection.x, reflection.y, reflection.z, 0);
                    //reflRotation = Quaternion.
                    changeDirection(reflRotation);
                    //transform.position = hitPoint.point + Vector3.Normalize( transform.forward);
                    transform.position = hitPoint.point;
                    auxiliar.position = transform.position;
                    direction = Vector3.Normalize(transform.forward);

                    //FindObjectOfType<AudioManager>().Play("BallHitsWall");
                    FindObjectOfType<AudioManager>().PlayRandomPitch("BallHitsWall");

                    //print("Se sale fuera");
                    Quaternion aux = Quaternion.LookRotation(hitPoint.normal);
                    PM.PlayWallSparks(this.transform, aux);

                }
                else
                {
                    transform.Translate(direction * velocity * Time.deltaTime, Space.World);
                }
                
            }
            else
            {
                //We can use this part to put the ball inside the map if, for any reason, it leaves the area.
                print("no choca");
            }

            auxiliar.Translate(direction * velocity * Time.deltaTime, Space.World); //this is for viewing in the editor.

            

        }
    }


    public bool CheckIfCrossField()
    {
        gamMan.LetTheBallCross();
        Vector3 aux = Vector3.Normalize(transform.forward);
        Ray auxRay = new Ray();
        RaycastHit auxHit;
        auxRay.origin = transform.position;
        auxRay.direction = aux;
        if (Physics.Raycast(auxRay, out auxHit, range, wallMask))
        {
            print(auxHit.transform.position);
            if(auxHit.transform.position.x <= 0) //Ray hits in the left.
            {
                if(gamMan.currentSideOfBall == 2)
                {
                    //print("Current side: "+ gamMan);
                    print(auxHit.transform.position.x);
                    print("DIBUJAMOS");
                    gamMan.DontLetTheBallCross();
                    return true;
                }
            } else if (auxHit.transform.position.x > 0) //Ray on the right
            {
                if (gamMan.currentSideOfBall == 1)
                {
                    //print("DIBUJAMOS");
                    gamMan.DontLetTheBallCross();

                    return true;
                }
            }
        }
        //print("NO DIBUJAMOS");
        gamMan.DontLetTheBallCross();
        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //print("el jugador está cerca");
            playerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //print("el jugador sale");
            playerNear = false;
        }
    }

    public void IncreaseSpeed()
    {
        velocity += velocityIncrease;
    }
    public void ResetVelocity()
    {
        velocity = initialVelocity;
    }

    public void changeDirection(Quaternion _rotation)
    {
        transform.rotation = _rotation;
    }

    private void shakeCameraWithWall()
    {
        gamMan.shakeCameraWithWall();
    }

    public void ChangeMaterial(string p)
    {
        if (p == "P1")
        {
            ChangeMaterialToP1();
            print("cambiamos material a p1");
        }
        else ChangeMaterialToP2();
    }

    private void ChangeMaterialToP1()
    {
        GetComponent<Renderer>().material = p1Material;
    }

    private void ChangeMaterialToP2()
    {
        GetComponent<Renderer>().material = p2Material;
    }
    public void ResetMaterial()
    {
        GetComponent<Renderer>().material = neutralMaterial;

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + direction * 2);
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, hitPoint.point);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(hitPoint.point, hitPoint.point + hitPoint.normal * 2);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(hitPoint.point, hitPoint.point+ Vector3.Normalize(reflection) *2 );
    }


}
