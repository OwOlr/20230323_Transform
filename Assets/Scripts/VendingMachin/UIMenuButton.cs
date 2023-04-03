using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMenuButton : MonoBehaviour
{
    public enum EMBInfo {    Name, Price, Stock    }

    private TextMeshProUGUI[] texts = null;

    //텍스트 가져오기
    private void Awake()
    {
        //TextMeshProUGUI의 자식 객체(+ 부모것도 있으면 포함)를 가져옴.
        //배열로 선언해서 배열형식으로 들어오는데 인덱스는 하이라키 창 순서대로 부여.
        texts = GetComponentsInChildren<TextMeshProUGUI>();

        //가져오기 확인.
        //foreach(TextMeshProUGUI text in texts) Debug.Log(text.name);

    }

    //테스트용
    private void Start()
    {
        InitInfos("콜라", 600, 3);
    }

    //Initialization - 초기화
    public void InitInfos(string _name, int _price, int _stock)
    {
        //UI는 입력값을 받아 표시만 해준다.
        texts[(int)EMBInfo.Name].text = _name;
        texts[(int)EMBInfo.Price].text = _price.ToString(); //ToString() : 숫자 -> 문자열 로 변환
        texts[(int)EMBInfo.Stock].text = _stock.ToString();

    }

}
