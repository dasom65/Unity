using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerController : BaseController
{
    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);
    PlayerStat _stat;
    bool _stopSkill = false;
    //bool _moveToDest = false;

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Player;
        _stat = gameObject.GetComponent<PlayerStat>();
        //Managers.Input.KeyAction -= OnKeyboard;
        //Managers.Input.KeyAction += OnKeyboard;//key가 눌리면 onkeyboard 작동
        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;
        //Managers.Resource.Instantiate("UI/UI_Button");
        //Managers.UI.ShowPopupUI<UI_Button>();


        //Managers.UI.ClosePopupUI(ui);
        //TEMP
        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);

    }
   
    protected override void UpdateMoving()
    {
        //몬스터가 내 사정거리보다 가까우면 공격
        if(_lockTarget!=null)
        {
            _destPos = _lockTarget.transform.position;
            float distance=  (_destPos - transform.position).magnitude;
            if(distance<=1)
            {
                State = Define.State.Skill;
                return;
            }
        }

        //이동
        Vector3 dir = _destPos - transform.position;
        dir.y = 0;
        if (dir.magnitude < 0.1f)
        {
            State = Define.State.Idle;
        }
        else
        {
            
            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);
            if(Physics.Raycast(transform.position+Vector3.up*0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if(Input.GetMouseButton(0)==false)
                    State = Define.State.Idle;
                return;
            }
            //transform.position += dir.normalized * moveDist;
            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            
        }
        //애니메이션
        //wait_run_ratio = Mathf.Lerp(wait_run_ratio, 1, 10.0f * Time.deltaTime);
       
        //anim.SetFloat("wait_run_ratio", wait_run_ratio);
        //anim.Play("WAIT_RUN");
        //현재 게임 상태에 대한 정보를 넘긴다
      
    }


    protected override void UpdateSkill()
    {
       if(_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
        
    }
    void OnHitEvent()
    {
       if(_lockTarget !=null)
        {
           Stat targetStat= _lockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat);
        }
        if(_stopSkill)
        {
            State = Define.State.Idle;
        }
        else
        {
            State = Define.State.Skill;
        }
        
    }
   

    //void OnKeyboard()
    //{
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        //transform.rotation = Quaternion.LookRotation(Vector3.forward);//월드좌표
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 1.0f);
    //        transform.position += Vector3.forward * Time.deltaTime * _speed;
    //    }
    //    if (Input.GetKey(KeyCode.S))
    //    {

    //        //transform.rotation = Quaternion.LookRotation(Vector3.back);//월드좌표
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 1.0f);
    //        transform.position += Vector3.back * Time.deltaTime * _speed;
    //    }
    //    if (Input.GetKey(KeyCode.A))
    //    {

    //        //transform.rotation = Quaternion.LookRotation(Vector3.left);//월드좌표
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 1.0f);
    //        transform.position += Vector3.left * Time.deltaTime * _speed;
    //    }
    //    if (Input.GetKey(KeyCode.D))
    //    {

    //        //transform.rotation = Quaternion.LookRotation(Vector3.right);//월드좌표
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 1.0f);
    //        transform.position += Vector3.right * Time.deltaTime * _speed;
    //    }
    //    _moveToDest = false;
    //}
    
    void OnMouseEvent(Define.MouseEvent evt)
    {
        switch(State)
        {
            case Define.State.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Skill:
                {
                    if (evt == Define.MouseEvent.PointerUp)
                        _stopSkill = true;
                }
                break;

        }
    }
    void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);

        switch (evt)
        {
            case Define.MouseEvent.PointerDown:
                {
                    _destPos = hit.point;
                    State = Define.State.Moving;
                    _stopSkill = false;
                    if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                    {
                        _lockTarget = hit.collider.gameObject;
                       
                    }
                    else
                    {
                        _lockTarget = null;
                    }
                }
                break;
            case Define.MouseEvent.Press:
                {
                    if (_lockTarget != null &&raycastHit)
                            _destPos = hit.point;
                }
                break;
            case Define.MouseEvent.PointerUp:
                _stopSkill = true;
                break;
        }
    }
}


//struct MyVector
//{
//    //위치백터, 방향벡터 
//    public float x;
//    public float y;
//    public float z;

//    public float magnitude { get { return Mathf.Sqrt(x*x+y*y+z*z); } }
//    public MyVector normalized  { get { return new MyVector(x / magnitude , y / magnitude ,z / magnitude); } }

//    public MyVector(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }
//    public static MyVector operator +(MyVector a, MyVector b)
//    {
//        return new MyVector(a.x + b.x, a.y + b.y, a.z + b.z);
//    }
//    public static MyVector operator -(MyVector a, MyVector b)
//    {
//        return new MyVector(a.x - b.x, a.y - b.y, a.z - b.z);
//    }
//    public static MyVector operator *(MyVector a, float d)
//    {
//        return new MyVector(a.x * d, a.y * d, a.z * d);
//    }

//}
//void Start()
//{
//    //    MyVector to = new MyVector(10.0f, 10.0f, 0.0f);
//    //    MyVector from = new MyVector(5.0f, 0.0f, 0.0f);
//    //    MyVector dir = to - from;//

//    //    dir = dir.normalized;
//    //    MyVector newPos = from + dir * _speed;
//    //    //방향벡터=거리, 실제 방향 

//}