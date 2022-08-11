using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Golem : MonoBehaviour
{
    public float speed;
    public Vector3 velocity;
    private float inputX;
    private float inputZ;
    public float stateChangeTime;
    public Animator anim;

    public float jumpPower;

    public GameObject player;

    public Vector3 directionVec;
    private Rigidbody rigidbody;
    public int rayDistance;

    public bool stateChange;//State 바꾸기용 불변수
    public bool idle;
    public bool theRock;
    public bool tracking;

    public enum State { Idle, Tracking, TheRock, BoongBoong, ShotGun }

    public State state
    {
        set
        {
            switch (value)
            {
                case State.Idle:
                    idle = true;

                    theRock = false;
                    tracking = false;
                    break;

                case State.TheRock:
                    theRock = true;

                    idle = false;
                    tracking = false;
                    break;

                case State.Tracking:
                    tracking = true;

                    idle = false;
                    theRock = false;
                    break;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        
        float distance = Vector3.Distance(transform.position, player.transform.position); 

        if (tracking)
        {
            Trace();
        }
        else if(idle)
        {
            Idle();
            
        }
        else if(theRock)
        {
            Rock();
            Invoke("StopRock", 1.5f);
            velocity = new Vector3(0, 0, 0);
            transform.position += velocity * speed * Time.deltaTime;
        }
        
        
        if(distance < 200)
        {
            state = State.Tracking;
            if (distance < 150)
            {
                int randomNum = Random.Range(1, 11);
                if (randomNum <= 4)
                {
                    state = State.TheRock;
                }
            }
        }
    }
    
    public void Idle()
    {
        anim.SetBool("isWalk", false);
        velocity = new Vector3(0, 0, 0);
        transform.position += velocity * speed * Time.deltaTime;
        Direction();
    }

    void Trace()
    {
        velocity = new Vector3(Mathf.Clamp(player.transform.position.x - transform.position.x, -1.0f, 1.0f),
                               0,
                               Mathf.Clamp(player.transform.position.z - transform.position.z, -1.0f, 1.0f));

        transform.position += velocity * speed * Time.deltaTime;
        transform.LookAt(transform.position + velocity);
        anim.SetBool("isWalk", true);
    }

    void Rock()
    {
        anim.SetTrigger("isRock");
    }

    void StopRock()
    {
        anim.ResetTrigger("isRock");
    }

    public void Direction()
    {
        if (inputX == -1 && inputZ == 1)//UpLeft
        {
            directionVec = new Vector3(-1.14f, 0, 1.14f);
        }
        if (inputX == 0 && inputZ == 1)//Up
        {
            directionVec = new Vector3(0, 0, 2.0f);
        }
        if (inputX == 1 && inputZ == 1)//UpRight
        {
            directionVec = new Vector3(1.14f, 0, 1.14f);
        }
        if (inputX == -1 && inputZ == 0)//Left
        {
            directionVec = new Vector3(-2.0f, 0, 0);
        }
        if (inputX == 0 && inputZ == 0)//Idle
        {
            directionVec = new Vector3(0, 0, 0);
        }
        if (inputX == 1 && inputZ == 0)//Right
        {
            directionVec = new Vector3(2.0f, 0, 0);
        }
        if (inputX == -1 && inputZ == -1)//DownLeft
        {
            directionVec = new Vector3(-1.14f, 0, -1.14f);
        }
        if (inputX == 0 && inputZ == -1)//Down
        {
            directionVec = new Vector3(0, 0, -2.0f);
        }
        if (inputX == 1 && inputZ == -1)//DownRight
        {
            directionVec = new Vector3(1.14f, 0, -1.14f);
        }
    }
}
