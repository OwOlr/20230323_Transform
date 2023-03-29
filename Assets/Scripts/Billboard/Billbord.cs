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

    //�÷��̾�� ���� �Ÿ� ��ȭ�ϸ� �ȵǱ⿡ ����� ����
    //Screen Play Distance
    //�б������� ����
    private readonly float scrPlayDist = 20f;

    private void Awake()
    {
        //getComponentInChildren 
        //getComponentsInChildren
        //transform.GetChild
        //GameObject.Fint = ã�� �� ���� ������ �ʿ��Ѵ�.
        pj = GetComponentInChildren<Panel_Joint>();
        screen = GetComponentInChildren<Screen>();
    }

    private void Start()
    {
        //����Ƽ������ Resources���� �ڷḦ ã�´�.
        //���ø� <> : ��ȣ ���� �ڷ��� �������� �ҷ��´�.
        VideoClip clip = Resources.Load<VideoClip>("Videos\\Zelda");    //  "\\" ������ ������ �����ϴ� ����.

        ////as :�θ������� ����ȯ�� �������� �����Ѵ�.
        //clip = Resources.Load("Videos\\Zelda") as VideoClip;

        ////���� ����ȯ
        //clip = (VideoClip)Resources.Load("Video\\Zelda");

        //Screen�� VideoClip�� �������ֱ�.
        
        screen.SetVideoClip(clip);
    }

    private void Update()
    {
        //����Ƽ������ �����ʿ��� 0�� ����
        //degree 90��, radian 180���� ����
        //+180������ �ϴµ� -> 3D ȯ�濡�� ����� �ݴ�� �𵨸��� ���� ������ ��������� �Ѵ�.
        float angleRad = CalcAngleToTarget();
        pj.Yaw((90f + 180f) - (angleRad * Mathf.Rad2Deg));   //������ �ٸ���.

        float dist = CalcDistanceWithTarget();
        
        if (dist < scrPlayDist) screen.Play();
        else screen.Pause();

        DebugDistance();
        

    }

    private float CalcAngleToTarget()
    {
        //���� ���� ���ϱ�
        Vector3 dirToTarget =
            targetTr.position - transform.position;     //Ÿ������ ������ ������ ������.
        //����ȭ ������ ���ؼ� ���̸� 1�� ������ش� -> �� ����(X,Y,X)�� �ڱ��ڽ��� ������ 1�� ���´�.
        dirToTarget.Normalize();

        //���� ���ϱ�
        //Atan(y / x) 0 ���� ������ �Ǹ� �ȵǴµ� ���� �� �� ����.
        //-> Atan2(x , y)�� ��ȭ. x�� y ���� �����ͼ� 0���� ���� ����ó�� �ϰڲ� ����.
        return Mathf.Atan2(dirToTarget.z, dirToTarget.x);

    }

    //�÷��̾�� �������� �Ÿ��� ���ϴ� �Լ�.
    private float CalcDistanceWithTarget()
    {
        Vector3 dirToTarget = targetTr.position - transform.position;
        
        //���1
        float dist = dirToTarget.magnitude;
        //���2
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
