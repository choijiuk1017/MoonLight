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


    Rigidbody rigid;
    Animator anim;

    GameObject nearObject;
    public GameObject melee;


    void Start()
    {
        Hp = 100;
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
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
            anim.SetTrigger("doSwing");
            melee.SetActive(true);
            attackDelay = 0;
            Invoke("resetAttack", 0.6f);
        }
    }

    void resetAttack()
    {
        melee.SetActive(false);
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
