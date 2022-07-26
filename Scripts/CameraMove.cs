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
            Vector3 rot = transform.rotation.eulerAngles; // 현재 카메라의 각도를 Vector3로 반환
            rot.y += Input.GetAxis("Mouse X") * rotateSpeed; // 마우스 X 위치 * 회전 스피드
            rot.x += -1 * Input.GetAxis("Mouse Y") * rotateSpeed; // 마우스 Y 위치 * 회전 스피드
            Quaternion q = Quaternion.Euler(rot); // Quaternion으로 변환
            q.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, q, 2f); // 자연스럽게 회전
        }
    }
}
