using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Golem : MonoBehaviour
{
    public float speed;
    private Vector3 velocity;
    private float inputX;
    private float inputZ;
    public float stateChangeTime;
    Animator anim;

    public float jumpPower;

    public LayerMask whatIsLayer;
    public float overlapRadius;

    public GameObject player;

    public Collider[] target;
    public Vector3 directionVec;
    private Rigidbody rigidbody;
    public int rayDistance;


    public bool stateChange;//State 바꾸기용 불변수
    public bool idle;
    public bool rush;
    public bool tracking;

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

        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, directionVec * rayDistance);
    }

    private void FixedUpdate()
    {
        target = Physics.OverlapSphere(transform.position, overlapRadius, whatIsLayer);
        if (tracking)
        {
            Trace();
            
        }
        

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

    void Trace()
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
