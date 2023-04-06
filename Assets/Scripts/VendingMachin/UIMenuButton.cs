using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIMenuButton : MonoBehaviour
{
    public enum EMBInfo {    Name, Price, Stock    }

    private Button btn = null;

    private TextMeshProUGUI[] texts = null;

    //�ؽ�Ʈ ��������
    private void Awake()
    {
        //TextMeshProUGUI�� �ڽ� ��ü(+ �θ�͵� ������ ����)�� ������.
        //�迭�� �����ؼ� �迭�������� �����µ� �ε����� ���̶�Ű â ������� �ο�.
        texts = GetComponentsInChildren<TextMeshProUGUI>();

        btn = GetComponent<Button>();

        //�������� Ȯ��.
        //foreach(TextMeshProUGUI text in texts) Debug.Log(text.name);

    }

    //�׽�Ʈ��
    //private void Start()
    //{
    //    InitInfos("�ݶ�", 600, 3);
    //}

    //Initialization - �ʱ�ȭ
    public void InitInfos(string _name, int _price, int _stock,
        int _num,
        UIMenu.OnclickMenuDelegate _onClickCallback)
    {
        //UI�� �Է°��� �޾� ǥ�ø� ���ش�.
        texts[(int)EMBInfo.Name].text = _name;
        texts[(int)EMBInfo.Price].text = _price.ToString(); //ToString() : ���� -> ���ڿ� �� ��ȯ
        texts[(int)EMBInfo.Stock].text = _stock.ToString();
        btn.onClick.AddListener(
            () =>
                {

                    //if(_onClickCallback != null)_onClickCallback(i);
                    _onClickCallback?.Invoke(_num,this);
                    Invoke("_onClickCallback", 1f);
                }
            );

        btn.interactable = _stock > 0;

    }
    
    public void InitInfos(VendingMachine.SProductInfo _productInfo,
        int _num,
        UIMenu.OnclickMenuDelegate _onClickCallback)
    {
        InitInfos(
            VendingMachine.VMProductToName(_productInfo.product),
            _productInfo.price,
            _productInfo.stock,
            _num,
            _onClickCallback);

    }

    public void UpdateInfo(VendingMachine.SProductInfo _productInfo)
    {
        texts[(int)EMBInfo.Name].text = VendingMachine.VMProductToName(_productInfo.product);
        texts[(int)EMBInfo.Price].text = _productInfo.price.ToString();
        texts[(int)EMBInfo.Stock].text = _productInfo.stock.ToString();

        btn.interactable = _productInfo.stock > 0;
    }

}
