using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecHand : MonoBehaviour
{
   
    [SerializeField]
    private GameObject secpPrefab = null;
    [SerializeField]
    private Transform targetTr = null;

    [Header("- Values -")]
    //[SerializeField, Range(0f,300f)]
    //private float speed = 10f;
    [SerializeField, Range(0f,10f)]
    private float dis = 1f;

    
    private Transform pointTr = null;
    private float angle = 90f;
    //private int hour = 0;
    //private int min = 0;
    private int sec = 0;
    

    private void Awake()
    {
        //부모 아래에서 프리팹 생성 (ex. AroundBall)
        GameObject go_sec = Instantiate(secpPrefab);
        //SetParent : 부모의 위치에 설정하기. (AroundBall.transform)
        go_sec.transform.SetParent(transform);
        //go_sec 함수 내에서 생성 되었기에 외부로 던지기 위해 pointTr이라는 transform변수에 넣는다.
        pointTr = go_sec.transform;  
        
    }

    private void Update()
    {
        //전체시간을 초 단위로 반환하기 때문에 실제 사용할 시, 분, 초로 환산
        double dayTime = System.DateTime.Now.TimeOfDay.TotalSeconds;
        sec = (int)(dayTime % 60.0);
        
        angle = -(sec * 6f);
        if (angle > 270) angle = -90;


        float x = Mathf.Cos((angle * Mathf.Deg2Rad));
        float y = 0f;
        float z = Mathf.Sin((angle * Mathf.Deg2Rad));

        Vector3 target = targetTr.position;
        pointTr.position = target + new Vector3(x, y, z) * dis;
        
    }
}
