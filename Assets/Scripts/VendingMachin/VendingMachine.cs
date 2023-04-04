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

    [SerializeField]
    private UIMenu uiMenu = null;

    //<T> : Template
    //����Ƽ���� �⺻ �ڷ����� ������������ ����� ���Ǵ� ����ȭ ���Ѿ� �Ѵ�.
    [SerializeField]    //�ʵ带 ����ȭ �����ش�.
    private List<SProductInfo> productInfoList = new List<SProductInfo>();  //���Ǳ��� �Ѱ踦 ���ֱ� ���� ���Ḯ��Ʈ�� ���

    private void Awake()
    {
        if(uiMenu == null)
        {
            Debug.LogError("UIMenu is missing!");
            return;
        }

    }

    //��ǰ �Ǹ�, ����


    //UI ��ȣ�ۿ�
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            if (uiMenu)
            {
                uiMenu.BuildBtns(productInfoList);
                uiMenu.gameObject.SetActive(true);

            }
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            if (uiMenu)
                uiMenu.gameObject.SetActive(false);
        }
        
    }

    //�ѱ���ġ
    // static ������ ���ص� �̹� �޸𸮿� �ö� �ֱ� ������ ���� ��ȿ�ϴ�.
    //���� ��� ���� : ���� ��ü�� ���� ���� �ϳ��� �����ϰ� �ȴ�.
    //���� ��� �Լ� : �����Ҵ� ���ص� ������ �����ϴ�.
    //���� �Ķ���͸� �������� �ʰ� UIMenuButton��Ʈ��Ʈ���� ��� �����ϴ�.
    //���� : ���� �Լ��������� ���� ������ ��� �����ϴ�. =>���� �ñⰡ �ٸ��� ������
    public static string VMProductToName(EVMProduct _product)
    {
        switch (_product) 
        {
            case EVMProduct.Coke:
                return "�ݶ�";
            case EVMProduct.Tissue:
                return "����";
            case EVMProduct.Bread:
                return "��";
            case EVMProduct.LetsBe:
                return "������";
            case EVMProduct.Vita500:
                return "��Ÿ500";
            case EVMProduct.OronaminC:
                return "���γ��� C";
            case EVMProduct.Sprite:
                return "��������Ʈ";
            case EVMProduct.SSOrange:
                return "�ٽ� ������";
            default:
                return "��?��?��";
        }
    }

}
