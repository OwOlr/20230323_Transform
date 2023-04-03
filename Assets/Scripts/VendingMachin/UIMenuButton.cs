using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMenuButton : MonoBehaviour
{
    public enum EMBInfo {    Name, Price, Stock    }

    private TextMeshProUGUI[] texts = null;

    //�ؽ�Ʈ ��������
    private void Awake()
    {
        //TextMeshProUGUI�� �ڽ� ��ü(+ �θ�͵� ������ ����)�� ������.
        //�迭�� �����ؼ� �迭�������� �����µ� �ε����� ���̶�Ű â ������� �ο�.
        texts = GetComponentsInChildren<TextMeshProUGUI>();

        //�������� Ȯ��.
        //foreach(TextMeshProUGUI text in texts) Debug.Log(text.name);

    }

    //�׽�Ʈ��
    private void Start()
    {
        InitInfos("�ݶ�", 600, 3);
    }

    //Initialization - �ʱ�ȭ
    public void InitInfos(string _name, int _price, int _stock)
    {
        //UI�� �Է°��� �޾� ǥ�ø� ���ش�.
        texts[(int)EMBInfo.Name].text = _name;
        texts[(int)EMBInfo.Price].text = _price.ToString(); //ToString() : ���� -> ���ڿ� �� ��ȯ
        texts[(int)EMBInfo.Stock].text = _stock.ToString();

    }

}
