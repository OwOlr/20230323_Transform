using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objet : MonoBehaviour
{
    //상위에있는 class는 자식들이 공통으로 가지는 정보가 담겨야 한다.
    public enum EType { Pluged, Unpluged }

    [SerializeField]
    protected string productName = "Unknown";
    [SerializeField]
    protected EType type = EType.Unpluged;


}
