using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    //1. 나 혹은 상대한테 rigibody 있어야함
    //2. 나한테 collider가 있어야함(is triger off)
    //3. 상대에게 collider가 있어야함 (is triger off)
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision!@{collision.gameObject.name}");
    }

    // 1.둘다 collider가 있어야함
    // 2.둘중 하나는 istriger On
    //3. 둘중 하나는 rigibody가 있어야함
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger{other.gameObject.name}");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

            LayerMask mask=LayerMask.GetMask("Monster");
            //int mask = (1 << 8)|(1<<9);//1을 왼쪽으로 팔칸 옮김 256 동일
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f, mask))
            {
                Debug.Log($"Raycast Camera{hit.collider.gameObject.name}");
            }
            //Debug.Log(Input.mousePosition); 스크린좌표 픽셀좌표
            //Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition));//viewport 0~1사이 화면 비율 표시
        }
    }
}
//if (Input.GetMouseButtonDown(0))
//{
//    Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
//    Vector3 dir = mousePos - Camera.main.transform.position;
//    dir = dir.normalized;
//    Debug.DrawRay(Camera.main.transform.position, dir * 100.0f, Color.red, 1.0f);
//    RaycastHit hit;
//    if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 100.0f))
//    {
//        Debug.Log($"Raycast Camera{hit.collider.gameObject.name}");
//    }
//}