using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    //��ư ������ ��������
    [SerializeField]
    private GameObject menuBtnPrefab = null;

    //��ư����Ʈ�� ��������
    private List<UIMenuButton> menuBtnList = null;

    public void BuildBtns(List<VendingMachine.SProductInfo> _productInfoList)
    {
        if (_productInfoList == null || _productInfoList.Count == 0) return;

        //Ʈ���ſ��� �� ������ �����ϱ� ������ �����ϱ� ���� ��ư ����(�����)
        if (menuBtnList != null && menuBtnList.Count > 0) ClearMenuButtonList();
        //����ó���� ���� ���� Ÿ�̹��� �n���.
        menuBtnList = new List<UIMenuButton>();

        int offsetX = 0;
        for(int i=0; i<_productInfoList.Count; ++i)
        {
            //�޴���ư ������ ����
            GameObject go = Instantiate(menuBtnPrefab);
            //ĵ���� �Ʒ� �ڽ����� ���� (ĵ���� 0,0�� ��ġ�ϰ� ��)
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
            
            //����ü�� �Ѱ��ֱ� ������ ����ǰ� �ִ�.
            btn.InitInfos(_productInfoList[i]);

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

}
