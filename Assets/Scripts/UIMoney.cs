using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIMoney : MonoBehaviour
{
    //UI위치 갱신

    private RectTransform rectTr = null;
    private TextMeshProUGUI textMoney = null;

    private void Awake()
    {
        rectTr = GetComponent<RectTransform>();
        textMoney = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void UpdatePosition(Vector3 _worldpos)
    {
        //View -> Screen까지는 Camera가 관리
        //World -> Screen 위치로 변경
        //m단위에서 픽셀 기준으로 변경된다.
        Vector3 w2s = Camera.main.WorldToScreenPoint(_worldpos);
        rectTr.position = w2s + new Vector3(0f,30f,0f);

    }
    public void UpdateMoney(int _money)
    {
        textMoney.text = _money.ToString();
    }

}
