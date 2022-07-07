using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public GameObject[] weapons;
    public bool[] hasWeapons;

    float hAxis;
    float vAxis;


    Vector3 moveVec;

    bool shift;
    bool fDown;
    bool xDown;
    bool isFireReady;


    Weapon equipWeapon;
    int equipWeaponIndex = -1;
    float fireDelay;


    Rigidbody rigid;
    Animator anim;

    GameObject nearObject;


    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        GetInput();
        Move();
        Interaction();
        Attack();
    }

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


    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        shift = Input.GetButton("Run");
        fDown = Input.GetButtonDown("Interaction");
        xDown = Input.GetButtonDown("Fire1");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;


        transform.position += moveVec * speed * Time.deltaTime;

        anim.SetBool("isWalk", moveVec != Vector3.zero);
        anim.SetBool("isRun", shift);

        transform.LookAt(transform.position + moveVec);
    }

    void Attack()
    {
        if (equipWeapon == null)
        {
            return;
        }

        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;

        if(xDown && isFireReady)
        {
            Debug.Log("Attack");
            equipWeapon.Use();
            anim.SetTrigger("doSwing");
            fireDelay = 0;
        }
    }

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
}
