using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]   //실행 하기전에 프리뷰에 볼 수 있음.

public class AroundBall_C : MonoBehaviour
{
    //Rotation direction
    //CW : ClockWise 시계방향
    //CCW: Counter ClockWise 반시계방향
    private enum ERotDir { CW, CCW }
    private enum ERotType { Pitch, Yaw, Roll }

    #region Public Variables
    [Header("- GameObject -")]
    [SerializeField]
    private GameObject ballPrefab = null;
    [SerializeField]
    private Transform targetTr = null;

    [Header("- Values -")]
    [SerializeField, Range(0f, 300f)]
    private float speed = 100f;

    [SerializeField, Range(0f, 10f)]
    private float distance = 1f;    //Radius

    [Header("- Type -")]
    [SerializeField]
    private ERotDir rotDir = ERotDir.CCW;
    [SerializeField]
    private ERotType rotType = ERotType.Yaw;
    #endregion
    
    
    //자식에 있는 Speare 트랜스폼을 가져온다.
    private Transform ballTr = null;

    private float angle = 0f;

    public class TestClass
    {
        public int i;
    }
    public struct TestStruct
    {
        public int i;
    }
    private void Test()
    {
        //클래스와 구조체의 차이
        TestClass tc = new TestClass();
        tc.i = 1;
        TestClass cpyClass = tc;
        cpyClass.i = 10;
        Debug.Log("tc.i = " + tc.i);

        TestStruct ts = new TestStruct();
        ts.i = 1;
        TestStruct cpystruct = ts;
        cpystruct.i = 10;
        Debug.Log("ts.i = " + ts.i); 
    }
    

    private void Awake()
    {
        Test();
        //자식의  transform을 가져옴.
        //ballTr = transform.GetChild(0);
        //Debug.Log(ballTr.name);
        if(ballPrefab == null)
        {
            Debug.LogError("ballPrefab is missing!");
        }
        GameObject go = Instantiate(ballPrefab);
        //go.transform.parent = transform; //최근에 들어 없어진 기능.
        go.transform.SetParent(transform);  //최근에 기능 추가
        ballTr = go.transform;
    }
    private void Update()
    {

        if (targetTr == null) return;

        switch (rotDir)
        {
            case ERotDir.CW:
                angle -= Time.deltaTime * speed;
                if (angle < 0f) angle = 360f;
                break;

            case ERotDir.CCW:
                //1. 각도를 알아야 한다
                angle += Time.deltaTime * speed;
                //Clamp 사용 해보기 value, min, max vlaue 값이 min보다 작으면 min값으로 기입, max도 동일한 기능.
                if (angle > 360f) angle = 0f;
                break;
        }

        Vector3 anglePos = new Vector3();
        CalcAnglePosWithType(rotType, angle, ref anglePos);

        Vector3 targetPos = targetTr.position;    
        ballTr.position = targetPos +(anglePos * distance);

    }

    private void CalcAnglePosWithType(ERotType _rotType, float _angle, ref Vector3 _pos)
    {
        //Vector3는 구조체이다.
        //값이 변경 될 때 복사본을 생성하는 데 변경 값을 유지하게 되면 방향이 누적 되어 정확한 방향을 알 수 없다.
        //C#은 무조건 참조 값(주소) 방식으로 날린다.

        //함수 내부에서 함수 외부의 값 변경을 할 수 있다.
        //ref - 값을 바꾸지 않아도 된다.
        //out - 값을 무조건 바꿔야 한다.

        float angle2Rad = _angle * Mathf.Deg2Rad;
        switch (_rotType)
        {
            case ERotType.Pitch:
                _pos.x = 0f;
               _pos.y = Mathf.Sin(angle2Rad);
               _pos.z = Mathf.Cos(angle2Rad);
                break;
            case ERotType.Yaw:
                _pos.x = Mathf.Cos(angle2Rad); //anlge (X) , Radian(O) -> 호도법으로 계산.
                //Deg2Rad = Degree(각도) to Radian(호)
                _pos.y = 0f;
                _pos.z = Mathf.Sin(angle2Rad);
                break;
            case ERotType.Roll:
                _pos.x = Mathf.Cos(angle2Rad);
                _pos.y = Mathf.Sin(angle2Rad);
                _pos.z = 0f;

                break;
        }

        

    }

}
