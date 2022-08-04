using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Hp;
    public float speed;
    public float jumpPower;
    //public GameObject[] weapons;
    //public bool[] hasWeapons;

    float hAxis;
    float vAxis;


    Vector3 moveVec;

    bool shift;
    bool fDown;
    bool leftMouseDown;
    bool cDown;
    bool spaceBar;

    bool isFireReady;
    bool isEvadeReady;
    bool isJumpReady;


    public float attackRate;
    float attackDelay;

    public float evadeRate;
    float evadeDelay;

    public float jumpRate;
    float jumpDelay;

    [SerializeField]
    private Transform cameraTransform;

    Rigidbody rigid;
    Animator anim;

    GameObject nearObject;

    public Shot shot;

    public Camera followCamera;


    void Start()
    {
        Hp = 100;
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float vertiacalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, vertiacalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);


        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        GetInput();
        Move();
        Run();
        //Interaction();
        Attack();
        Evade();
        //Jump();
    }
    /*
    void Interaction()
    {
        if(fDown && nearObject != null)
        {
            if(nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;

                Destroy(nearObject);

                weapons[0].SetActive(true);
            }
        }
    }
    */
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        shift = Input.GetButton("Run");
        fDown = Input.GetButtonDown("Interaction");
        leftMouseDown = Input.GetMouseButtonDown(0);
        cDown = Input.GetKeyDown(KeyCode.C);
        spaceBar = Input.GetKeyDown(KeyCode.Space);
    }
    void Turn()
    {
        if(leftMouseDown)
        {
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if(Physics.Raycast(ray, out rayHit, 100))
            {
                Vector3 nextVec = rayHit.point - transform.position;
                transform.LookAt(transform.position + nextVec);
            }
        }
    }
    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * Time.deltaTime;

        anim.SetBool("isWalk", moveVec != Vector3.zero);

        transform.LookAt(transform.position + moveVec);   
    }

    void Run()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        if(shift == true)
        {
            transform.position += moveVec * speed * 1.5f * Time.deltaTime;
        }
        anim.SetBool("isRun", shift);
        transform.LookAt(transform.position + moveVec);
    }


    void Attack()
    {
        attackDelay += Time.deltaTime;
        isFireReady = attackRate < attackDelay;
        if(leftMouseDown && isFireReady && !spaceBar)
        {
            shot.use();
            anim.SetTrigger("doSwing");
            attackDelay = 0;
        }
    }

    void Evade()
    {
        evadeDelay += Time.deltaTime;
        isEvadeReady = evadeRate < evadeDelay;
        if (spaceBar && isEvadeReady && !cDown)
        {
            anim.SetTrigger("doEvade");
            evadeDelay = 0;
        }
    }

    /*
    void Jump()
    {
        jumpDelay += Time.deltaTime;
        isJumpReady = jumpRate < jumpDelay;
        if(cDown && isJumpReady && !spaceBar)
        {
            anim.SetTrigger("doJump");
            rigid.AddForce(Vector3.up * jumpPower * 2, ForceMode.Impulse);
            jumpDelay = 0;
        }
    }
    */

    /*
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Weapon")
            nearObject = other.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
            nearObject = null;
    }

    */
}
