using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 상속(Ingeritance)  ( ':' 으로 표현)
// MonoBehaviour : add component를 통해서 실행
// 아닌 경우 : new했을 시기에 실행 (동적할당 가능)
public class PlayerController : MonoBehaviour
{
    //Delegate 대지자 : 하나의 자료형.여기에 함수 주소를 저장.
    public delegate void MLBDelegate();
    public delegate void MRBDelegate();
    private MLBDelegate mlbCallback = null;
    private MRBDelegate mrbCallback = null;

    // Class Member Variables 클래스 멤버 변수
    // private , public : 멤버 범위 접근자

    private Transform tr = null;
    private Rigidbody rb = null;

    //객체지향의 중점은 데이터를 보호해야 한다.
    //private이어서 안보이지만 SerializeField를 통해서 인스펙터창에 보이게 한다.
    // Range() : Attribute 속성
    [SerializeField][Range(10f,50f)]private float moveSpeed = 10f;
    [SerializeField] [Range(50, 100f)] private float rotSpeed = 10f;

    private void Awake()
    {
        //컴포넌트를 가져올 때는 Awke함수에.
        //언제실행하는지 몰라 사용자가 직접 표기해줘야 한다.
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        //tr = transform;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //값을 직접이 아닌 함수를 통해서만 바꿔야한다.
            //복사한 후 값을 계산한 후 갱신해줘야 한다.
            //회전 값 저장(X)
            //Vector3 newPos = transform.position;
            //newPos.z += moveSpeed * Time.deltaTime;
            //Time.deltaTime : 프레임단위로 보정 -> 컴퓨터 성능차이를 좁혀 딜레이가 없는 멀티플레이를 유지하기 위해서이다.
            //transform.position = newPos;

            //반향 지정.ver
            //Vector3 newPos = transform.position;
            //newPos += tr.forward * moveSpeed * Time.deltaTime;

            //transform.position = newPos;

            //충돌처리 보정.ver
            //Velocity -> 한 방향으로 계속 간다.
            //위에 방법은 위치이동이나 Velocity의 경우 물리처리이기에 반드시 충돌처리가 가능하다.
            //BUT Velocity 만발하게 된다면 연산이 많아지게 된다. -> 충돌이 없는 경우는 위치 기반을 추천
            rb.velocity = tr.forward * moveSpeed;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            //한 방향으로 계속 이동하는 것을 제한.
            rb.velocity = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.S))
        {

            // 위치 값 덮어쓰기
            Vector3 newPos = transform.position + (-tr.forward * moveSpeed * Time.deltaTime);  //현재 내위치 + (방향지정 * 속도 * 시간조정)
            transform.position = newPos;
        }
        if (Input.GetKey(KeyCode.A))
        {
            // 위치 값 변화량
            //Translate= 행렬 srt, 몇도 회전했는지 저장한다.
            tr.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            tr.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        //=============================================================
        //Pitch , Yaw, Roll 3차원 회전 축
        //오일러 앵글 : x, y, z 축으로 몇 도(각도)씩 옮기는 가에 따라 회전.
        //짐벌락 : 회전할 때 3축 -> 2축으로 축이 합쳐짐.
        //사원수 = Quaternion

        if (Input.GetKey(KeyCode.Q))
        {
            Vector3 newRot = tr.rotation.eulerAngles;
            newRot.y -= rotSpeed * Time.deltaTime;
            tr.rotation = Quaternion.Euler(newRot);
            //tr.rotation.eulerAngles = newRot;

        }

        if (Input.GetKey(KeyCode.E))
        {
            // Function OverLoading 함수 오버 로딩
            //up기준으로 speed만크 회전
            tr.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
        }

        //=============================================================
        //크기 Scale.

        //상대좌표 localposition
        //절대좌표 
        //+크기(Scale)는 절대 스케일이 없다.

        if (Input.GetKeyDown(KeyCode.R))
        {
            tr.localScale = Vector3.one * 2f;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            tr.localScale = Vector3.one;
        }

        //옛날방식
        if (Input.GetMouseButton(0))
        {
            if (mlbCallback != null) mlbCallback();
        }

        if(Input.GetMouseButtonDown(1))
        {
            // ? : 있냐 없냐의 유무 예외처리
            mrbCallback?.Invoke();
        }

    }
    
    public void SetMLBDelegate(MLBDelegate _callback)
    {
        mlbCallback = _callback;

    }
    public void SetMRBDelegate(MRBDelegate _callback)
    {
        mrbCallback = _callback;
    }
}
