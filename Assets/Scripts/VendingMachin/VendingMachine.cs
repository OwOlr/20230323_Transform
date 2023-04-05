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

    //상품 구조체 생성
    [System.Serializable]   // 사용자 정의 변수들을 분해하여 아스키코드로 변환
    public struct SProductInfo
    {
        public EVMProduct product;
        public int price;
        public int stock;

        public bool CheckStock() { return stock > 0; }
        public void Sell() { 
               
            if(CheckStock()) --stock; 
        
        }
    }

    [SerializeField]
    private UIMenu uiMenu = null;

    //<T> : Template
    //유니티에서 기본 자료형은 진원해주지만 사용자 정의는 직렬화 시켜야 한다.
    [SerializeField]    //필드를 직렬화 시켜준다.
    private List<SProductInfo> productInfoList = new List<SProductInfo>();  //자판기의 한계를 없애기 위해 연결리스트로 사용

    private void Awake()
    {
        if(uiMenu == null)
        {
            Debug.LogError("UIMenu is missing!");
            return;
        }

    }

    //상품 판매, 생성


    //UI 상호작용
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            if (uiMenu)
            {
                uiMenu.BuildBtns(productInfoList,
                    OnClickMenu);
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

    //한글패치
    // static 접근을 안해도 이미 메모리에 올라가 있기 때문에 값이 유효하다.
    //정적 멤버 변수 : 여러 객체를 만들어도 변수 하나로 공유하게 된다.
    //정적 멤버 함수 : 동적할당 안해도 접근이 가능하다.
    //따로 파라매터를 선언하지 않고도 UIMenuButton스트립트에서 사용 가능하다.
    //주의 : 정적 함수내에서는 정적 변수만 사용 가능하다. =>생성 시기가 다르기 떄문에
    public static string VMProductToName(EVMProduct _product)
    {
        switch (_product) 
        {
            case EVMProduct.Coke:
                return "콜라";
            case EVMProduct.Tissue:
                return "휴지";
            case EVMProduct.Bread:
                return "빵";
            case EVMProduct.LetsBe:
                return "레쓰비";
            case EVMProduct.Vita500:
                return "비타500";
            case EVMProduct.OronaminC:
                return "오로나민 C";
            case EVMProduct.Sprite:
                return "스프라이트";
            case EVMProduct.SSOrange:
                return "쌕쌕 오렌지";
            default:
                return "몰?루?ㅋ";
        }
    }


    //연락을 받을 코드 공간

    public void OnClickMenu(int _btnNum)
    {

        Debug.Log(productInfoList[_btnNum].product.ToString() +
            "(" + productInfoList[_btnNum].price +
            ") : " + productInfoList[_btnNum].stock);

        if (!productInfoList[_btnNum].CheckStock()) return;

        //임시객체로 관리
        //productInfoList[selectedIdx].stock = "1";
        productInfoList[_btnNum].Sell();
        
        
        //상품만들기
        //보유 돈 차감
        // 버튼 정보 갱신



    }

}
