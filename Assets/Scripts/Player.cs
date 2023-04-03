using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerController playerCtrl = null;
    //동작시킬 오브제를 인지.
    private Electronics electronics = null;

    private void Awake()
    {
        playerCtrl = GetComponent<PlayerController>();
        playerCtrl.SetMLBDelegate(OnMLBDown);
        playerCtrl.SetMRBDelegate(OnMRBDown);
    }

    private void OnTriggerEnter(Collider _other)
    {
        //최상위의 오브젝트의 tag를 바꿔야 인식한다. (주의)
        //if(_other.gameObject.tag == "Electronics")
        if (_other.gameObject.CompareTag("Electronics"))
        {
            //플레이어는 전원만 넣는 활동만 하기 때문에 라디오 스크립트가 아니라 전자기기 스크립트를 가져온다.
            electronics = _other.GetComponent<Electronics>();
            Debug.Log("Get Electronics");
        }


        //멀어졌을 때 작동이 꺼진다.
    }
    private void OnTriggerExit(Collider _other)
    {
        if (_other.gameObject.CompareTag("Electronics"))
        {
            electronics = null;
        }
    }

    // player의 연락을 받을 함수
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

