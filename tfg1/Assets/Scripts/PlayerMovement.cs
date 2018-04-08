using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody playerRB;
    Vector3 movement;
    Vector3 rotation;
    public int speed;
    private float lastV, lastH;
    public float slerpSpeed;

    public BallScript ballSript;
    public GameObject ball;
    Vector3 direction;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
       // direction = transform.forward;

    }

    void Start()
    {
        direction = transform.forward;
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);

        if(h==0 && v == 0)
        {
            //Rotate(lastH, lastV);
        }
        else
        {
            Rotate(h, v);
            lastH = h;
            lastV = v;
        }

    }

    private void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRB.MovePosition(transform.position + movement);
    }

    private void Rotate(float h, float v)
    {
        rotation.Set(h, 0f, v);
        Quaternion newRotation = Quaternion.LookRotation(rotation);
        playerRB.rotation = Quaternion.Slerp(playerRB.rotation, newRotation, slerpSpeed * Time.deltaTime);
        direction = transform.forward;
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            if (ballSript.playerNear)
            {
                //ball.transform.rotation = transform.rotation;
                ballSript.changeDirection(transform.rotation);
                ballSript.ballStopped = false;
            }
        }
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + direction * 2);
    }
}
