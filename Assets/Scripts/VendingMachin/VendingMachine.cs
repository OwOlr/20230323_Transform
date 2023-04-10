using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{

    private float angle = 0f;
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
        public void Sell()
        {

            if (CheckStock()) --stock;

        }
    }

    [SerializeField]
    private UIMenu uiMenu = null;

    //<T> : Template
    //유니티에서 기본 자료형은 진원해주지만 사용자 정의는 직렬화 시켜야 한다.
    [SerializeField]    //필드를 직렬화 시켜준다.
    private List<SProductInfo> productInfoList = new List<SProductInfo>();  //자판기의 한계를 없애기 위해 연결리스트로 사용

    private Player player = null;

    private void Awake()
    {
        if (uiMenu == null)
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
                uiMenu.gameObject.SetActive(true);
                uiMenu.BuildBtns(productInfoList,
                    OnClickMenu);

                //현재 상호작용 할 플레이어
                player = _other.GetComponent<Player>();
            }
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            if (uiMenu != null)
            {
                uiMenu.gameObject.SetActive(false);
                player = null;

            }

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

    public void OnClickMenu(int _btnNum, UIMenuButton _menuBtn)
    {
        //Debug.Log(productInfoList[_btnNum].product.ToString() +
        //    "(" + productInfoList[_btnNum].price +
        //    ") : " + productInfoList[_btnNum].stock);

        if (player == null) return;

        if (!productInfoList[_btnNum].CheckStock()) return;

        //잔액이 충분하지 않는 경우
        //if(돈이 충분한가?(금액))
        if (productInfoList[_btnNum].price > player.Money)
        {
            Debug.Log("!!잔액이 부족합니다!!");
            return;
        }


        //임시객체로 관리
        //--productInfoList[selectedIdx].stock;
        //함수방식으로 변경 -> 갯수변경이 안됐다.
        //productInfoList[_btnNum].Sell();
        //리스트는 목록형태로 관리
        //C++은 바로 구조체 접근이 가능하다.
        //C#은 실제 원본이 아닌 매번 구조체를 복사한다.
        //class 참조형식이라 가능하다.
        //함수형식도 복사본만 변경이 가능하다.
        //복사본을 저장하고 원본에 대시 저장해주는 방식으로 해결

        SProductInfo changeInfo = productInfoList[_btnNum];
        //--changeInfo.stock;
        changeInfo.Sell();
        productInfoList[_btnNum] = changeInfo;


        // 버튼 정보 갱신
        //uiMenu.UpdateButtonInfo(_btnNum, changeInfo);
        //객체지향이 깨지지만 퍼포먼스를 챙길 수 있다.
        _menuBtn.UpdateInfo(changeInfo);

        //상품만들기

        GameObject prefab = ProductSpawnManager.GetPrefab(
            productInfoList[_btnNum].product);


        if (prefab != null)
        {
            Transform vmTr = GetComponent<Transform>();
            //자판기 주변에 생성
            //자판기 주변 장애물을 피하면서 배치.
            Instantiate(prefab, GetValidSpawnPosition(),
                Quaternion.identity);

        }

        //보유 돈 차감
        player.Buy(productInfoList[_btnNum].price);



    }

    private Vector3 Set(float _angle)
    {
        //콜라이더를 이용한 충돌 알고리즘.
        //생성 되자마자 프리팹의 콜라이더가 충덜이 됐을 때 retrun.
        //return할 때 다른 위치에 다시 생성
        //생성한 후 물리 검사 - 한 프레임이 느리다는 단점.

        //2.physics에서 별도로 검사 
        //BoxCast는 생성되기 전에 생성 후 유무에 따라 그 자리에 생성
        //Box가 검사량이 많다, OBB검사 때문에 비용이 크나 정확도는 높다.

        //3.RayCast
        //생성할 위치에 반직선을 쏘고나서 걸리면 생성X
        //BoxCast에 비해 정확도가 떨어질 가능성이 있다.

        Transform vmTr = GetComponent<Transform>();


        float angle2Rad = _angle * Mathf.Deg2Rad;
        Vector3 anglePos = new Vector3(Mathf.Cos(angle2Rad), 0, Mathf.Sin(angle2Rad));



        return vmTr.position + (anglePos * 2);
    }

    private Vector3 GetValidSpawnPosition()
    {

        const float SPAWN_DIST = 3f;            // 생성 거리 = 반지름
        const float PI2 = Mathf.PI * 2f;        // 360도로 배치하기 위해
        const float POS_Y = 0.5f;               // 바닥에서 검사하면 정확도가 떨어질까봐 위로 띄움, 나중에 0으로 복구

        Vector3 startPos = transform.position;  // 레이쏘기 시작할 위치
        startPos.y = POS_Y;
        bool isValidPos = false;                // 유효한 위치인지
        float angle = 0f;                       // 랜덤 각도 임의저장
        RaycastHit hitInfo;                     // 레이 히트 정보

        Vector3 spawnPos = Vector3.zero;        // 생성할 위치
        while (!isValidPos)
        {
            // 랜덤 각도 얻기
            angle = Random.Range(0f, PI2);
            spawnPos = transform.position +
                new Vector3(
                    Mathf.Cos(angle) * SPAWN_DIST,
                    POS_Y,
                    Mathf.Sin(angle) * SPAWN_DIST
                );

            Vector3 dir = (spawnPos - startPos).normalized;
            //dir.normalized();
            // 생성할 위치 후보를 향해 레이 충돌검사
            //RayCast -> BoxCast로 변경이 가능함.
            if (Physics.Raycast(startPos, dir, out hitInfo, SPAWN_DIST))
            {
                // 충돌된 오브젝트가 있다면 다른 위치를 찾아야 됨
                Debug.Log("Raycast Hit: " + hitInfo.transform.name);
                continue;
            }

            isValidPos = true;
        }

        // 높이를 다시 0으로
        spawnPos.y = 0f;
        return spawnPos;
    }
}
