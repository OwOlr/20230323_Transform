using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerController playerCtrl = null;
    //���۽�ų �������� ����.
    private Electronics electronics = null;

    private void Awake()
    {
        playerCtrl = GetComponent<PlayerController>();
        playerCtrl.SetMLBDelegate(OnMLBDown);
        playerCtrl.SetMRBDelegate(OnMRBDown);
    }

    private void OnTriggerEnter(Collider _other)
    {
        //�ֻ����� ������Ʈ�� tag�� �ٲ�� �ν��Ѵ�. (����)
        //if(_other.gameObject.tag == "Electronics")
        if (_other.gameObject.CompareTag("Electronics"))
        {
            //�÷��̾�� ������ �ִ� Ȱ���� �ϱ� ������ ���� ��ũ��Ʈ�� �ƴ϶� ���ڱ�� ��ũ��Ʈ�� �����´�.
            electronics = _other.GetComponent<Electronics>();
            Debug.Log("Get Electronics");
        }


        //�־����� �� �۵��� ������.
    }
    private void OnTriggerExit(Collider _other)
    {
        if (_other.gameObject.CompareTag("Electronics"))
        {
            electronics = null;
        }
    }

    // player�� ������ ���� �Լ�
    public void OnMLBDown()
    {
        //Power On/Off

        if (electronics)
        {
            if (electronics.GetIsPowerOn())
                electronics.PowerOff();
            else
                electronics.PowerOn();

        }

        //Debug.Log("On Mouse Left Button");

    }

    public void OnMRBDown()
    {
        //Use
        if(electronics != null)
        {
            electronics.Use();
        }
        //Debug.Log("On Mouse Right Button");
    }

}

