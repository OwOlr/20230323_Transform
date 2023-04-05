using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{

    public delegate void OnclickMenuDelegate(int _btnNum);

    //��ư ������ ��������
    [SerializeField]
    private GameObject menuBtnPrefab = null;

    //��ư����Ʈ�� ��������
    private List<UIMenuButton> menuBtnList = null;

    

    public void BuildBtns(List<VendingMachine.SProductInfo> _productInfoList,
        OnclickMenuDelegate _onClickCallback)
    {
        if (_productInfoList == null || _productInfoList.Count == 0) return;

        //Ʈ���ſ��� �� ������ �����ϱ� ������ �����ϱ� ���� ��ư ����(�����)
        if (menuBtnList != null && menuBtnList.Count > 0) ClearMenuButtonList();
        //����ó���� ���� ���� Ÿ�̹��� �n���.
        menuBtnList = new List<UIMenuButton>();


        for(int i=0; i<_productInfoList.Count; ++i)
        {
            //�޴���ư ������ ����
            GameObject go = Instantiate(menuBtnPrefab);
            //ĵ���� �Ʒ� �ڽ����� ���� (ĵ���� 0,0�� ��ġ�ϰ� ��)
            //go.transform.SetParent(transform);
            //go.transform.localPosition = Vector3.zero;
            RectTransform rectTr = go.GetComponent<RectTransform>();
            rectTr.SetParent(GetComponent<RectTransform>());
            rectTr.localPosition = CalcLocalPositionWithIndex(i, _productInfoList.Count);
  

            
            UIMenuButton btn = go.GetComponent<UIMenuButton>();
            //btn.InitInfos(_productInfoList[i].product.ToString(),
            //    _productInfoList[i].price,
            //    _productInfoList[i].stock);
            
            //����ü�� �Ѱ��ֱ� ������ ����ǰ� �ִ�.
            btn.InitInfos(_productInfoList[i],i,_onClickCallback);

            //Button button = go.GetComponent<Button>();
            //button.onClick.AddListener(
            //    //���ٽ�(Lambda Expression)
            //    ( ) =>
            //    {
            //        Debug.Log(i);
            //        //�ӽ÷� ȣ���ϴ� �Լ�
            //        _onClickCallback?.Invoke(i);
            //    }
            //);


            menuBtnList.Add(btn);
        }



    }

    private void ClearMenuButtonList() 
    { 
        //Range Based Loop ���� ��� �ݺ��� : ��Ͽ� �ִ� ������ ��ȸ�ϴ� �ݺ���.
        //����: �ӵ��� ����, ������ ��ȸ
        foreach(UIMenuButton btn in menuBtnList)
        {
            //������Ʈ�� ����� (UIMenuButton ��ũ��Ʈ�� �����ȴ�.)
            //Destroy(btn);
            Destroy(btn.gameObject);
        }
        menuBtnList.Clear();
    }

    //��ư ������ ���� (������ �� �̺�Ʈ)

    //��ư �����ϱ� �Լ�
    /// <summary>
    /// ��ư ��ġ ���ϴ� �Լ�
    /// </summary>
    /// <param name="_idx">���� �ε���</param>
    /// <param name="_totalCnt">��ư ��ü ����</param>
    /// <returns></returns>
    private Vector3 CalcLocalPositionWithIndex2(int _idx, int _totalCnt)
    {
        //x���� �ִ� ĭ ��
        const int COL_MAX = 3;

        Transform bgTr = transform.GetChild(0);
        RectTransform bgRtTr = bgTr.GetComponent<RectTransform>();
        //sizeDelta�� RectTransform�� ����� ���� �� �ִ� �Լ�.
        Vector2 bgSize = bgRtTr.sizeDelta;
        float bgWidth = bgSize.x;
        float bgHeight = bgSize.y;

        //��ư ������ ���ϱ�
        Transform btnTr = transform.GetChild(_idx);
        RectTransform btnRtTr = btnTr.GetComponent<RectTransform>();
        Vector2 btnSize = btnRtTr.sizeDelta;
        float btnWidth = btnSize.x;
        float btnHeight = btnSize.y;

        //offset ���ϱ�
        float offsetX = (bgWidth - btnWidth * _totalCnt)/(_totalCnt+1);


        float cnt = (_totalCnt % 2) > 0 ? 1 : 2;
        //�������� ���ϱ�
        Vector2 startPos = new Vector2 (((btnWidth+offsetX)/cnt),0);



        Vector3 pos = Vector3.zero;

        return pos;

    }

    /// <summary>
    /// ��ư ��ġ ���ϴ� �Լ�
    /// </summary>
    /// <param name="_idx">���� �ε���</param>
    /// <param name="_totalCnt">��ư ��ü ����</param>
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
