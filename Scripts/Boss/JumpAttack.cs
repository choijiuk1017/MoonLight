using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : MonoBehaviour
{
    public GameObject monster;
    Rigidbody rigid;
    public float jumpPower;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("¾ß¹ß");
            Jump();
        }
    }
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
        monster.GetComponent<Animator>().SetTrigger("isJump");
    }
}
