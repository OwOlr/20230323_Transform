using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIMoney : MonoBehaviour
{
    //UI��ġ ����

    private RectTransform rectTr = null;
    private TextMeshProUGUI textMoney = null;

    private void Awake()
    {
        rectTr = GetComponent<RectTransform>();
        textMoney = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void UpdatePosition(Vector3 _worldpos)
    {
        //View -> Screen������ Camera�� ����
        //World -> Screen ��ġ�� ����
        //m�������� �ȼ� �������� ����ȴ�.
        Vector3 w2s = Camera.main.WorldToScreenPoint(_worldpos);
        rectTr.position = w2s + new Vector3(0f,30f,0f);

    }
    public void UpdateMoney(int _money)
    {
        textMoney.text = _money.ToString();
    }

}
