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
    }

    //<T> : Template
    //유니티에서 기본 자료형은 진원해주지만 사용자 정의는 직렬화 시켜야 한다.
    [SerializeField]    //필드를 직렬화 시켜준다.
    private List<SProductInfo> productInfoList = new List<SProductInfo>();  //자판기의 한계를 없애기 위해 연결리스트로 사용


    //상품 판매, 생성


    //UI 상호작용

}
