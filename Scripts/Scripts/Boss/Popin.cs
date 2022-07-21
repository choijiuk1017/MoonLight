using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Popin : MonoBehaviour
{
    public float speed;
    private Vector3 velocity;
    public float stateChangeTime;
    private float inputX;
    private float inputZ;

    public float jumpPower;

    public bool stateChange;//State 바꾸기용 불변수
    public bool idle;
    public bool rush;
    public bool tracking;

    int randomNum;

    public Vector3 directionVec;
    private Rigidbody rigidbody;
    public int rayDistance;

    //Target()용 변수들
    public Collider[] target;
    public LayerMask whatIsLayer;
    public float overlapRadius;

    public GameObject player;

    Animator anim;


    enum State { Idle, Rush, Tracking }

    private State state
    {
        set
        {
            switch (value)
            {
                case State.Idle:
                    idle = true;

                    rush = false;
                    tracking = false;
                    break;

                case State.Rush:
                    rush = true;

                    idle = false;
                    tracking = false;
                    break;

                case State.Tracking:
                    tracking = true;

                    idle = false;
                    rush = false;
                    break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        rayDistance = 1;

        state = State.Idle;
        stateChange = false;

        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Debug.DrawRay(transform.position, directionVec * rayDistance);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        randomNum = Random.Range(0, 2);
        if (tracking)
        {
            Tracking();
            if (randomNum == 0)
            {
                Rush();
            }
            else if (randomNum == 1)
            {
                Jump();
            }
        }
        if (idle)
        {
            Idle();
        }
        

        /*
        if (idle)
        {
            Idle();
        }
        else if(moving)
        {
            Moving();
        }
        else if(tracking)
        {
            Tracking();
        }
        */

        target = Physics.OverlapSphere(transform.position, overlapRadius, whatIsLayer);


        if (target.Length > 0)
        {
            state = State.Tracking;
        }
        else
        {
            //추적 종료 시 Idle, Moving 상태로 랜덤하게 돌입
            state = (State)Random.Range(0, 2);
            stateChange = false;
            return;
        }

    }

    public void Idle()
    {
        if (!stateChange) StartCoroutine(StateChange());

        velocity = new Vector3(0, 0, 0);
        transform.position += velocity * speed * Time.deltaTime;

        anim.SetBool("isWalk", false);

        Direction();
    }

    public void Rush()
    {
        if(Vector3.Distance(player.transform.position, transform.position) < 30f)
        {
            velocity = new Vector3(Mathf.Clamp(target[0].transform.position.x - transform.position.x, -1.0f, 1.0f),
                               0,
                               Mathf.Clamp(target[0].transform.position.z - transform.position.z, -1.0f, 1.0f));

            transform.position += velocity * speed * 2 * Time.deltaTime;

            transform.LookAt(transform.position + velocity);
            if (Vector3.Distance(player.transform.position, transform.position) < 20f)
            {
                Invoke("AnimeRush", 0.5f);
            }
        }
    }

    void AnimeRush()
    {
        anim.SetBool("isWalk", false);
        anim.SetBool("isRush", true);
    }

    void AnimeJump()
    {
        anim.SetBool("isWalk", false);
        anim.SetBool("isJump", true);
    }

    void Jump()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 30f)
        {
            velocity = new Vector3(Mathf.Clamp(target[0].transform.position.x - transform.position.x, -1.0f, 1.0f),
                               0,
                               Mathf.Clamp(target[0].transform.position.z - transform.position.z, -1.0f, 1.0f));

            transform.position += velocity * speed * 2 * Time.deltaTime;

            transform.LookAt(transform.position + velocity);
            if (Vector3.Distance(player.transform.position, transform.position) < 20f)
            {
                rigidbody.AddForce(Vector3.up * jumpPower * 2, ForceMode.Impulse);
                Invoke("AnimeJump", 0.3f);
            }
        }
    }

    public void Tracking()
    {      
        velocity = new Vector3(Mathf.Clamp(target[0].transform.position.x - transform.position.x, -1.0f, 1.0f),
                               0,
                               Mathf.Clamp(target[0].transform.position.z - transform.position.z, -1.0f, 1.0f));

        transform.position += velocity * speed * Time.deltaTime;
        transform.LookAt(transform.position + velocity);
        anim.SetBool("isWalk", true); 
    }

    public void Direction()
    {
        if (inputX == -1 && inputZ == 1)//UpLeft
        {
            //left = true;
            //up = true;
            directionVec = new Vector3(-1.14f, 0, 1.14f);

            //down = false;
            //right = false;
        }
        if (inputX == 0 && inputZ == 1)//Up
        {
            //up = true;
            directionVec = new Vector3(0, 0, 2.0f);

            //down = false;
            //right = false;
            //left = false;
        }
        if (inputX == 1 && inputZ == 1)//UpRight
        {
            //right = true;
            //up = true;
            directionVec = new Vector3(1.14f, 0, 1.14f);

            //left = false;
            //down = false;
        }
        if (inputX == -1 && inputZ == 0)//Left
        {
            //left = true;
            directionVec = new Vector3(-2.0f, 0, 0);

            //right = false;
            //up = false;
            //down = false;
        }
        if (inputX == 0 && inputZ == 0)//Idle
        {
            //up = false;
            //down = false;
            //right = false;
            //left = false;
            directionVec = new Vector3(0, 0, 0);
        }
        if (inputX == 1 && inputZ == 0)//Right
        {
            //right = true;
            directionVec = new Vector3(2.0f, 0, 0);

            //up = false;
            //down = false;
            //left = false;
        }
        if (inputX == -1 && inputZ == -1)//DownLeft
        {
            //left = true;
            //down = true;
            directionVec = new Vector3(-1.14f, 0, -1.14f);

            //up = false;
            //right = false;
        }
        if (inputX == 0 && inputZ == -1)//Down
        {
            //down = true;
            directionVec = new Vector3(0, 0, -2.0f);

            //up = false;
            //right = false;
            //left = false;
        }
        if (inputX == 1 && inputZ == -1)//DownRight
        {
            //right = true;
            //down = true;
            directionVec = new Vector3(1.14f, 0, -1.14f);

            //up = false;
            //left = false;
        }
    }

    IEnumerator StateChange()
    {
        stateChange = true;

        yield return new WaitForSeconds(stateChangeTime);

        //State.Idle = 0, State.Moving = 1, State.Tracking = 2
        //0과 1까지만 대입
        state = (State)Random.Range(0, 2);
        stateChange = false;
    }

    void OnDrawGizmosSelected()
    {
        //충돌범위 기즈모
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, overlapRadius);
    }
}
