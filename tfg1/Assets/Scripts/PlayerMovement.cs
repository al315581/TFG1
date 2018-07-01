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
    public int PLAYER_NUMBER;

    public BallScript ballSript;
    public GameObject ball;
    Vector3 direction;

    public string HorizontalCtrl = "Horizontal_P1";
    public string VerticalCtrl = "Vertical_P1";
    public string FireCtrl = "Fire1_P1";


    public bool isHitting = false;
    public bool isOnGround = false;
    Animator anim;
    public bool defeated = false;

    //private AudioManager AM;
    public ParticleManager PM;
    public GamMan GM;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
       // direction = transform.forward;

    }

    void Start()
    {
        anim = GetComponent<Animator>();
        direction = transform.forward;
        //AM = FindObjectOfType<AudioManager>();
    }


    
    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw(HorizontalCtrl);
        float v = Input.GetAxisRaw(VerticalCtrl);

        //initially move() was here, but moved to the end

        if(h==0 && v == 0)
        {
            anim.SetBool("Running", false);
            //Rotate(lastH, lastV);
        }
        else
        {
            if (!isHitting && !isOnGround)
            {
                anim.SetBool("Running", true);
                Rotate(h, v);
                lastH = h;
                lastV = v;
            }

        }
        if (!isHitting && !isOnGround)
        {
            Move(h, v);
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
        /*
        print(anim.GetBool("Running"));
        if (anim.GetBool("Running"))
        {
            if (PLAYER_NUMBER == 1)
            {
                PM.StartRunParticlesP1();

            }
            else PM.StartRunParticlesP2();

        }
        else
        {
            PM.EndRunParticlesP1();

        }
        */
        if(PLAYER_NUMBER == 1)
        {
            if (anim.GetBool("Running"))
            {
                PM.StartRunParticlesP1();
            }
            else PM.EndRunParticlesP1();
        }
        else
        {
            if (anim.GetBool("Running"))
            {
                PM.StartRunParticlesP2();
            }
            else PM.EndRunParticlesP2();
        }

        if (Input.GetButtonDown(FireCtrl) && !defeated)
        {
            anim.SetTrigger("BaseballHit");
            //isHitting = true;
            //AM.Play("PlayerHits");
            //AM.PlayRandomPitch("PlayerHits");
            GameObject.FindObjectOfType<AudioManager>().PlayRandomPitch("PlayerHits");
        }

        if (ballSript.playerNear && isHitting)
        {
            GM.ShakeCameraPlayerHits();
            GameObject.FindObjectOfType<AudioManager>().PlayRandomPitch("HitBall");
            PM.PlayerHitsBall(ball.transform);

            ballSript.ChangeMaterial(this.gameObject.name);
            //ball.transform.rotation = transform.rotation;
            ballSript.changeDirection(transform.rotation);
            ballSript.ballStopped = false;
            ballSript.hitted = true;

            if (PLAYER_NUMBER == 1)
            {
                if (ballSript.CheckIfCrossField())
                    GameObject.FindObjectOfType<GroundEffectManagerScript>().StartLeftEffect(transform);



            }
            else
            {
                if (ballSript.CheckIfCrossField())
                    GameObject.FindObjectOfType<GroundEffectManagerScript>().StartRightEffect(transform);

            }

            if (GamMan.state == GamMan.stateOfMatch.running)
            {
                ballSript.IncreaseSpeed();
            }
            isHitting = false;
        }
    }

    public void EndAttack()
    {
        //print("Acaba ataque");
        isHitting = false;
    }
    public void StartAttack()
    {
        //print("Empieza ataque");
        isHitting = true;
    }

    public void HittedFront()
    {
        isOnGround = true;
        anim.SetTrigger("HitFront");
    }

    public void HittedBack()
    {
        isOnGround = true;
        anim.SetTrigger("HitBehind");
    }

    public void PlayerCanMoveAgain()
    {
        isOnGround = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + direction * 2);
    }
    public void RestartAnimations()
    {
        isHitting = false;

        anim.SetTrigger("RestartAnimation");
    }
}
