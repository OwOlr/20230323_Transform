using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Electronics : Objet
{
    [SerializeField]
    private int price = 0;

    //전원 케이블이 꽃혀있는지 여부
    private bool isPluged = true;
    private bool isPowerOn = false;

    private int Price { get { return price; } }

    //InLine  Function : 본 함수명이 호출되지 않고 returnd의 함수를 호출한다.
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


    //  추상 : abstract void Use();  //부모에서 정의 되어 있지 않으며, 자식에서 무조건 정의를 해야하고, 호출도 자삭이 먼저 호출된다.
    //  가상 : virtual void Use() {}; //부모에서 정의 되어있기 때문에 자식이 없을 경우 부모클래스의 함수가 호출된다.
    public abstract void Use();

}
