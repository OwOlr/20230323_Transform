using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{

    public delegate void OnclickMenuDelegate(int _btnNum);

    //버튼 프리팹 가져오기
    [SerializeField]
    private GameObject menuBtnPrefab = null;

    //버튼리스트를 가져오기
    private List<UIMenuButton> menuBtnList = null;

    

    public void BuildBtns(List<VendingMachine.SProductInfo> _productInfoList,
        OnclickMenuDelegate _onClickCallback)
    {
        if (_productInfoList == null || _productInfoList.Count == 0) return;

        //트리거엔터 될 때마다 생성하기 때문에 방지하기 위해 버튼 정리(지우기)
        if (menuBtnList != null && menuBtnList.Count > 0) ClearMenuButtonList();
        //예외처리를 위해 생성 타이밍을 늧춘다.
        menuBtnList = new List<UIMenuButton>();


        for(int i=0; i<_productInfoList.Count; ++i)
        {
            //메뉴버튼 프리팹 생성
            GameObject go = Instantiate(menuBtnPrefab);
            //캔버스 아래 자식으로 생성 (캔버스 0,0에 위치하게 됨)
            //go.transform.SetParent(transform);
            //go.transform.localPosition = Vector3.zero;
            RectTransform rectTr = go.GetComponent<RectTransform>();
            rectTr.SetParent(GetComponent<RectTransform>());
            rectTr.localPosition = CalcLocalPositionWithIndex(i, _productInfoList.Count);
  

            
            UIMenuButton btn = go.GetComponent<UIMenuButton>();
            //btn.InitInfos(_productInfoList[i].product.ToString(),
            //    _productInfoList[i].price,
            //    _productInfoList[i].stock);
            
            //구조체로 넘겨주기 때문에 복사되고 있다.
            btn.InitInfos(_productInfoList[i],i,_onClickCallback);

            //Button button = go.GetComponent<Button>();
            //button.onClick.AddListener(
            //    //람다식(Lambda Expression)
            //    ( ) =>
            //    {
            //        Debug.Log(i);
            //        //임시로 호출하는 함수
            //        _onClickCallback?.Invoke(i);
            //    }
            //);


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
    /// <summary>
    /// 버튼 위치 구하는 함수
    /// </summary>
    /// <param name="_idx">현재 인덱스</param>
    /// <param name="_totalCnt">버튼 전체 갯수</param>
    /// <returns></returns>
    private Vector3 CalcLocalPositionWithIndex2(int _idx, int _totalCnt)
    {
        //x축의 최대 칸 수
        const int COL_MAX = 3;

        Transform bgTr = transform.GetChild(0);
        RectTransform bgRtTr = bgTr.GetComponent<RectTransform>();
        //sizeDelta가 RectTransform의 사이즈를 구할 수 있는 함수.
        Vector2 bgSize = bgRtTr.sizeDelta;
        float bgWidth = bgSize.x;
        float bgHeight = bgSize.y;

        //버튼 사이즈 구하기
        Transform btnTr = transform.GetChild(_idx);
        RectTransform btnRtTr = btnTr.GetComponent<RectTransform>();
        Vector2 btnSize = btnRtTr.sizeDelta;
        float btnWidth = btnSize.x;
        float btnHeight = btnSize.y;

        //offset 구하기
        float offsetX = (bgWidth - btnWidth * _totalCnt)/(_totalCnt+1);


        float cnt = (_totalCnt % 2) > 0 ? 1 : 2;
        //시작지점 구하기
        Vector2 startPos = new Vector2 (((btnWidth+offsetX)/cnt),0);



        Vector3 pos = Vector3.zero;

        return pos;

    }

    /// <summary>
    /// 버튼 위치 구하는 함수
    /// </summary>
    /// <param name="_idx">현재 인덱스</param>
    /// <param name="_totalCnt">버튼 전체 갯수</param>
    /// <returns></returns>
    private Vector3 CalcLocalPositionWithIndex(int _idx, int _totalCnt)
    {
        if (_idx < 0 || _totalCnt < 1) return Vector3.zero;

        const int COL_MAX = 3;

        Vector2 bgSize = transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;
        Vector2 btnSize = menuBtnPrefab.GetComponent<RectTransform>().sizeDelta;

        int colCnt = Mathf.Clamp(_totalCnt, 1, COL_MAX);
        float btnTotalW = colCnt * btnSize.x;
        float totalOffsetW = bgSize.x - btnTotalW;

        int rowCnt = (int)Mathf.Ceil((float)_totalCnt / colCnt);
        float btnTotalH = rowCnt * btnSize.y;
        float totalOffsetH = bgSize.y - btnTotalH;

        Vector2 offset = Vector2.zero;
        offset.x = totalOffsetW / (float)(colCnt + 1);
        offset.y = totalOffsetH / (float)(rowCnt + 1);

        Vector2 btnDist = offset + btnSize;
        Vector2 startPos = new Vector2(
            -btnDist.x / (colCnt % 2 == 0 ? 2f : 1f),
            btnDist.y / (rowCnt % 2 == 0 ? 2f : 1f)
            );

        Vector3 pos = Vector3.zero;
        if (colCnt > 1) pos.x = startPos.x + ((_idx % COL_MAX) * btnDist.x);
        if (rowCnt > 1) pos.y = startPos.y - ((_idx / COL_MAX) * btnDist.y);

        return pos;
    }
}
