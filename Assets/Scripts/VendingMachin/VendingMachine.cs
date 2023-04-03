using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    public enum EVMProduct 
    {
        Coke,
        Tissue,
        Bread,
        LetsBe,
        Vita500,
        OronaminC,
        Sprite,
        SSOrange
    }

    //��ǰ ����ü ����
    [System.Serializable]   // ����� ���� �������� �����Ͽ� �ƽ�Ű�ڵ�� ��ȯ
    public struct SProductInfo
    {
        public EVMProduct product;
        public int price;
        public int stock;
    }

    //<T> : Template
    //����Ƽ���� �⺻ �ڷ����� ������������ ����� ���Ǵ� ����ȭ ���Ѿ� �Ѵ�.
    [SerializeField]    //�ʵ带 ����ȭ �����ش�.
    private List<SProductInfo> productInfoList = new List<SProductInfo>();  //���Ǳ��� �Ѱ踦 ���ֱ� ���� ���Ḯ��Ʈ�� ���


    //��ǰ �Ǹ�, ����


    //UI ��ȣ�ۿ�

}
