using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public Transform bulletPos;
    public GameObject bullet;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void use()
    {
        StartCoroutine("shot");
    }
    IEnumerator shot()
    {
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;

        yield return null;
    }
}
