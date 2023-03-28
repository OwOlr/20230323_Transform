using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundBall : MonoBehaviour
{

    //자식에 있는 Speare 트랜스폼을 가져온다.
    private Transform ballTr = null;

    private float angle = 0f;
    private float speed = 100f;

    private void Awake()
    {
        //자식의  transform을 가져옴.
        ballTr = transform.GetChild(0);
        Debug.Log(ballTr.name);
    }
    private void Update()
    {
        //1. 각도를 알아야 한다
        angle += Time.deltaTime * speed;
        
        //Clamp 사용 해보기
        if (angle > 360f) angle = 0f;

        float x = Mathf.Cos(angle * Mathf.Deg2Rad); //anlge (X) , Radian(O) -> 호도법으로 계산.
        //Deg2Rad = Degree(각도) to Radian(호)
        float y = 0f;
        float z = Mathf.Sin(angle * Mathf.Deg2Rad);

        ballTr.position = new Vector3(x, y, z);

    }

}
