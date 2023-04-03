using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Electronics : Objet
{
    [SerializeField]
    private int price = 0;

    //���� ���̺��� �����ִ��� ����
    private bool isPluged = true;
    private bool isPowerOn = false;

    private int Price { get { return price; } }

    //InLine  Function : �� �Լ����� ȣ����� �ʰ� returnd�� �Լ��� ȣ���Ѵ�.
    public bool GetIsPowerOn(){   return isPowerOn;   }

    public virtual void Awake()
    {
        type = EType.Pluged;
    }
    public void PowerOn()
    {
        if (isPluged)
        {
            isPowerOn = true;
        }
        Debug.Log(productName + "Power On");
    }

    public void PowerOff()
    {
        if (isPowerOn)
        {
            isPowerOn = false;
            Debug.Log(productName + "Power Off");
        }
    }


    //  �߻� : abstract void Use();  //�θ𿡼� ���� �Ǿ� ���� ������, �ڽĿ��� ������ ���Ǹ� �ؾ��ϰ�, ȣ�⵵ �ڻ��� ���� ȣ��ȴ�.
    //  ���� : virtual void Use() {}; //�θ𿡼� ���� �Ǿ��ֱ� ������ �ڽ��� ���� ��� �θ�Ŭ������ �Լ��� ȣ��ȴ�.
    public abstract void Use();

}
