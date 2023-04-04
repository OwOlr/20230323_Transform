using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    //버튼 프리팹 가져오기
    [SerializeField]
    private GameObject menuBtnPrefab = null;

    //버튼리스트를 가져오기
    private List<UIMenuButton> menuBtnList = null;

    public void BuildBtns(List<VendingMachine.SProductInfo> _productInfoList)
    {
        if (_productInfoList == null || _productInfoList.Count == 0) return;

        //트리거엔터 될 때마다 생성하기 때문에 방지하기 위해 버튼 정리(지우기)
        if (menuBtnList != null && menuBtnList.Count > 0) ClearMenuButtonList();
        //예외처리를 위해 생성 타이밍을 늧춘다.
        menuBtnList = new List<UIMenuButton>();

        int offsetX = 0;
        for(int i=0; i<_productInfoList.Count; ++i)
        {
            //메뉴버튼 프리팹 생성
            GameObject go = Instantiate(menuBtnPrefab);
            //캔버스 아래 자식으로 생성 (캔버스 0,0에 위치하게 됨)
            //go.transform.SetParent(transform);
            //go.transform.localPosition = Vector3.zero;
            RectTransform rectTr = go.GetComponent<RectTransform>();
            rectTr.SetParent(GetComponent<RectTransform>());
            //rectTr.localPosition =  Vector3.zero;
            rectTr.localPosition = new Vector3(offsetX, 0, 0);
            offsetX += 150*-1;

            
            UIMenuButton btn = go.GetComponent<UIMenuButton>();
            //btn.InitInfos(_productInfoList[i].product.ToString(),
            //    _productInfoList[i].price,
            //    _productInfoList[i].stock);
            
            //구조체로 넘겨주기 때문에 복사되고 있다.
            btn.InitInfos(_productInfoList[i]);

            menuBtnList.Add(btn);
        }



    }

    private void ClearMenuButtonList() 
    { 
        //Range Based Loop 범위 기반 반복문 : 목록에 있는 내용을 순회하는 반복문.
        //장점: 속도가 빠름, 무조건 순회
        foreach(UIMenuButton btn in menuBtnList)
        {
            //컴포넌트만 사라짐 (UIMenuButton 스크립트만 삭제된다.)
            //Destroy(btn);
            Destroy(btn.gameObject);
        }
        menuBtnList.Clear();
    }

    //버튼 프리팹 관리 (눌렀을 때 이벤트)

    //버튼 정렬하기 함수

}
