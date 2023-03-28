using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CT_Sphere : MonoBehaviour
{
    //Callback Funtion : 사용자가 아닌 시스템이 호출하는 것.
    //+Delegate : 콜백 함수를 생성.
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("CT_Sphere Collision!");
    }
}
