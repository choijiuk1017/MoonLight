using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : MonoBehaviour
{
    public GameObject monster;
    public GameObject player;
    Rigidbody rigid;
    public float jumpPower;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.transform.position, transform.position) < 20f)
        {
            Debug.Log("tlqkf");
            Jump();
        }
    }

    void Jump()
    {
        rigid.AddForce(Vector3.up * jumpPower * 2, ForceMode.Impulse);
        monster.GetComponent<Animator>().SetTrigger("isJump");
    }
}
