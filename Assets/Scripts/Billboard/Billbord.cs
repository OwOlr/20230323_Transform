using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Billbord : MonoBehaviour
{

    [SerializeField]
    private Transform targetTr = null;

    private Panel_Joint pj = null;
    private Screen screen = null;

    //플레이어간의 인지 거리 변화하면 안되기에 상수로 선언
    //Screen Play Distance
    //읽기전용을 선언
    private readonly float scrPlayDist = 20f;

    private void Awake()
    {
        //getComponentInChildren 
        //getComponentsInChildren
        //transform.GetChild
        //GameObject.Fint = 찾을 때 많은 연산이 필요한다.
        pj = GetComponentInChildren<Panel_Joint>();
        screen = GetComponentInChildren<Screen>();
    }

    private void Start()
    {
        //유니티에서는 Resources에서 자료를 찾는다.
        //템플릿 <> : 괄호 안의 자료형 전용으로 불러온다.
        VideoClip clip = Resources.Load<VideoClip>("Videos\\Zelda");    //  "\\" 폴더와 파일을 구별하는 문자.

        ////as :부모형으로 형변환이 가능하지 검증한다.
        //clip = Resources.Load("Videos\\Zelda") as VideoClip;

        ////강제 형변환
        //clip = (VideoClip)Resources.Load("Video\\Zelda");

        //Screen에 VideoClip을 셋팅해주기.
        
        screen.SetVideoClip(clip);
    }

    private void Update()
    {
        //유니티에서는 오른쪽에서 0을 시작
        //degree 90도, radian 180으로 보정
        //+180돌려야 하는데 -> 3D 환경에서 정면과 반대로 모델링해 놨기 때문에 뒤집어줘야 한다.
        float angleRad = CalcAngleToTarget();
        pj.Yaw((90f + 180f) - (angleRad * Mathf.Rad2Deg));   //단위가 다르다.

        float dist = CalcDistanceWithTarget();
        
        if (dist < scrPlayDist) screen.Play();
        else screen.Pause();

        DebugDistance();
        

    }

    private float CalcAngleToTarget()
    {
        //방향 벡터 구하기
        Vector3 dirToTarget =
            targetTr.position - transform.position;     //타켓으로 지정된 방향이 나오낟.
        //정규화 과정을 통해서 길이를 1로 만들어준다 -> 각 성분(X,Y,X)에 자기자신을 나누면 1이 나온다.
        dirToTarget.Normalize();

        //각도 구하기
        //Atan(y / x) 0 으로 나누게 되면 안되는데 구별 할 수 없어.
        //-> Atan2(x , y)로 변화. x와 y 값을 가져와서 0인지 먼저 예외처리 하겠끔 구성.
        return Mathf.Atan2(dirToTarget.z, dirToTarget.x);

    }

    //플레이어와 빌보드의 거리를 구하는 함수.
    private float CalcDistanceWithTarget()
    {
        Vector3 dirToTarget = targetTr.position - transform.position;
        
        //방법1
        float dist = dirToTarget.magnitude;
        //방법2
        dist = Vector3.Distance(targetTr.position, transform.position);

        return dist;

    }

    private void DebugDistance()
    {
        Vector3 dirToTarget = targetTr.position - transform.position;

        Color color = Color.white;

        if (scrPlayDist < dirToTarget.magnitude)
            color = Color.yellow;
        else
            color = Color.red;

        Debug.DrawLine(targetTr.position, transform.position,color);
    }

}
