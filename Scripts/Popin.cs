using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Popin : MonoBehaviour
{
    public GameObject player;

    private GameObject lockTarget;
    private NavMeshAgent nvAgent;

    private Transform playerTransform;

    Vector3 moveVec;

    public float speed = 10;

    [SerializeField]
    float scanRange = 10;

    [SerializeField]
    float attackRange = 2;


    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        lockTarget = null;

        nvAgent = gameObject.GetComponent<NavMeshAgent>();

        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();

        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateIdle();
        //UpdateMoving();
    }
    
    void UpdateIdle()
    {
        if(player == null)
        {
            return;
        }

        float distance = Vector3.Distance(player.transform.position, transform.position);

        if(distance <= scanRange)
        {
            transform.LookAt(transform.position + player.transform.position);
            lockTarget = player;
            
        }
        else
        {
            lockTarget = null;
        }
    }

    void UpdateMoving()
    {
        if(lockTarget == null)
        {
            return;
        }

        Vector3 dir = playerTransform.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);

        anim.SetBool("isWalk", moveVec != Vector3.zero);
    }

    void jump()
    {
        
    }
}
