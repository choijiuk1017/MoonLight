using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movePower = 1f;
    public float jumpPower = 1f;
    public int maxHealth = 100;

    public int health;


    Rigidbody2D rigid;

    Vector3 movement;
    bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        health = 30;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            isJumping = true;
        }
    }

    void FixedUpdate()
    {
        move();
        jump();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.tag == "Healpoint")
        {
            health = health + 40;
            Debug.Log("Health + 40");
            if (health > maxHealth || health == maxHealth)
            {
                health = maxHealth;
            }
        }
        
            
    }

    void move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if(Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;
        }

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    void jump()
    {
        if (!isJumping)
        {
            return;
        }

        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }
}
