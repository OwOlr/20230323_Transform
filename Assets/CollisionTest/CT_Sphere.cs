using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CT_Sphere : MonoBehaviour
{
    //Callback Funtion : ����ڰ� �ƴ� �ý����� ȣ���ϴ� ��.
    //+Delegate : �ݹ� �Լ��� ����.
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("CT_Sphere Collision!");
    }
}
