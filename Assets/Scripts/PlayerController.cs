using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���(Ingeritance)  ( ':' ���� ǥ��)
// MonoBehaviour : add component�� ���ؼ� ����
// �ƴ� ��� : new���� �ñ⿡ ���� (�����Ҵ� ����)
public class PlayerController : MonoBehaviour
{
    //Delegate ������ : �ϳ��� �ڷ���.���⿡ �Լ� �ּҸ� ����.
    public delegate void MLBDelegate();
    public delegate void MRBDelegate();
    private MLBDelegate mlbCallback = null;
    private MRBDelegate mrbCallback = null;

    // Class Member Variables Ŭ���� ��� ����
    // private , public : ��� ���� ������

    private Transform tr = null;
    private Rigidbody rb = null;

    //��ü������ ������ �����͸� ��ȣ�ؾ� �Ѵ�.
    //private�̾ �Ⱥ������� SerializeField�� ���ؼ� �ν�����â�� ���̰� �Ѵ�.
    // Range() : Attribute �Ӽ�
    [SerializeField][Range(10f,50f)]private float moveSpeed = 10f;
    [SerializeField] [Range(50, 100f)] private float rotSpeed = 10f;

    private void Awake()
    {
        //������Ʈ�� ������ ���� Awke�Լ���.
        //���������ϴ��� ���� ����ڰ� ���� ǥ������� �Ѵ�.
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        //tr = transform;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //���� ������ �ƴ� �Լ��� ���ؼ��� �ٲ���Ѵ�.
            //������ �� ���� ����� �� ��������� �Ѵ�.
            //ȸ�� �� ����(X)
            //Vector3 newPos = transform.position;
            //newPos.z += moveSpeed * Time.deltaTime;
            //Time.deltaTime : �����Ӵ����� ���� -> ��ǻ�� �������̸� ���� �����̰� ���� ��Ƽ�÷��̸� �����ϱ� ���ؼ��̴�.
            //transform.position = newPos;

            //���� ����.ver
            //Vector3 newPos = transform.position;
            //newPos += tr.forward * moveSpeed * Time.deltaTime;

            //transform.position = newPos;

            //�浹ó�� ����.ver
            //Velocity -> �� �������� ��� ����.
            //���� ����� ��ġ�̵��̳� Velocity�� ��� ����ó���̱⿡ �ݵ�� �浹ó���� �����ϴ�.
            //BUT Velocity �����ϰ� �ȴٸ� ������ �������� �ȴ�. -> �浹�� ���� ���� ��ġ ����� ��õ
            rb.velocity = tr.forward * moveSpeed;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            //�� �������� ��� �̵��ϴ� ���� ����.
            rb.velocity = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.S))
        {

            // ��ġ �� �����
            Vector3 newPos = transform.position + (-tr.forward * moveSpeed * Time.deltaTime);  //���� ����ġ + (�������� * �ӵ� * �ð�����)
            transform.position = newPos;
        }
        if (Input.GetKey(KeyCode.A))
        {
            // ��ġ �� ��ȭ��
            //Translate= ��� srt, � ȸ���ߴ��� �����Ѵ�.
            tr.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            tr.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        //=============================================================
        //Pitch , Yaw, Roll 3���� ȸ�� ��
        //���Ϸ� �ޱ� : x, y, z ������ �� ��(����)�� �ű�� ���� ���� ȸ��.
        //������ : ȸ���� �� 3�� -> 2������ ���� ������.
        //����� = Quaternion

        if (Input.GetKey(KeyCode.Q))
        {
            Vector3 newRot = tr.rotation.eulerAngles;
            newRot.y -= rotSpeed * Time.deltaTime;
            tr.rotation = Quaternion.Euler(newRot);
            //tr.rotation.eulerAngles = newRot;

        }

        if (Input.GetKey(KeyCode.E))
        {
            // Function OverLoading �Լ� ���� �ε�
            //up�������� speed��ũ ȸ��
            tr.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
        }

        //=============================================================
        //ũ�� Scale.

        //�����ǥ localposition
        //������ǥ 
        //+ũ��(Scale)�� ���� �������� ����.

        if (Input.GetKeyDown(KeyCode.R))
        {
            tr.localScale = Vector3.one * 2f;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            tr.localScale = Vector3.one;
        }

        //�������
        if (Input.GetMouseButton(0))
        {
            if (mlbCallback != null) mlbCallback();
        }

        if(Input.GetMouseButtonDown(1))
        {
            // ? : �ֳ� ������ ���� ����ó��
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
