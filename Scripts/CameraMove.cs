using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public float rotateSpeed = 6.0f;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
        Rotate();
    }

    private void Rotate()
    {
        if(Input.GetMouseButton(1))
        {
            Vector3 rot = transform.rotation.eulerAngles; // ���� ī�޶��� ������ Vector3�� ��ȯ
            rot.y += Input.GetAxis("Mouse X") * rotateSpeed; // ���콺 X ��ġ * ȸ�� ���ǵ�
            rot.x += -1 * Input.GetAxis("Mouse Y") * rotateSpeed; // ���콺 Y ��ġ * ȸ�� ���ǵ�
            Quaternion q = Quaternion.Euler(rot); // Quaternion���� ��ȯ
            q.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, q, 2f); // �ڿ������� ȸ��
        }
    }
}
