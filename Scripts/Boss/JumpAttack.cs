using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : MonoBehaviour
{
    public GameObject monster;
    Rigidbody rigid;
    public float jumpPower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Jump()
    {
        rigid.AddForce(Vector3.up * jumpPower * 2, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            Jump();
        }
    }
}
