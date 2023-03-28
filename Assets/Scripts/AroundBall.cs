using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundBall : MonoBehaviour
{

    //�ڽĿ� �ִ� Speare Ʈ�������� �����´�.
    private Transform ballTr = null;

    private float angle = 0f;
    private float speed = 100f;

    private void Awake()
    {
        //�ڽ���  transform�� ������.
        ballTr = transform.GetChild(0);
        Debug.Log(ballTr.name);
    }
    private void Update()
    {
        //1. ������ �˾ƾ� �Ѵ�
        angle += Time.deltaTime * speed;
        
        //Clamp ��� �غ���
        if (angle > 360f) angle = 0f;

        float x = Mathf.Cos(angle * Mathf.Deg2Rad); //anlge (X) , Radian(O) -> ȣ�������� ���.
        //Deg2Rad = Degree(����) to Radian(ȣ)
        float y = 0f;
        float z = Mathf.Sin(angle * Mathf.Deg2Rad);

        ballTr.position = new Vector3(x, y, z);

    }

}
