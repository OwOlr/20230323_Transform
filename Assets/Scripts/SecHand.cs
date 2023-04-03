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
        //�θ� �Ʒ����� ������ ���� (ex. AroundBall)
        GameObject go_sec = Instantiate(secpPrefab);
        //SetParent : �θ��� ��ġ�� �����ϱ�. (AroundBall.transform)
        go_sec.transform.SetParent(transform);
        //go_sec �Լ� ������ ���� �Ǿ��⿡ �ܺη� ������ ���� pointTr�̶�� transform������ �ִ´�.
        pointTr = go_sec.transform;  
        
    }

    private void Update()
    {
        //��ü�ð��� �� ������ ��ȯ�ϱ� ������ ���� ����� ��, ��, �ʷ� ȯ��
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
