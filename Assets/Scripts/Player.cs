using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Player : MonoBehaviour
{
    private PlayerController playerCtrl = null;
    //동작시킬 오브제를 인지.
    private Electronics electronics = null;

    //인벤토리
    [SerializeField]
    //private Queue<Product> inventory = new Queue<Product>();
    private Queue<VendingMachine.EVMProduct> inventory = new Queue<VendingMachine.EVMProduct>();

    [SerializeField]private UIMoney uiMoney = null;
    private int money = 10000;

    public int Money
    {
        get { return money; }
    }


    private void Awake()
    {
        playerCtrl = GetComponent<PlayerController>();
        playerCtrl.SetMLBDelegate(OnMLBDown);
        playerCtrl.SetMRBDelegate(OnMRBDown);
        playerCtrl.SetUseDelegate(UseProduct);
    }

    private void Start()
    {
        uiMoney.UpdatePosition(transform.position);
        uiMoney.UpdateMoney(money);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //    UseProduct();
        //}

        uiMoney.UpdatePosition(transform.position);
    }

    private void OnTriggerEnter(Collider _other)
    {
        //최상위의 오브젝트의 tag를 바꿔야 인식한다. (주의)
        //if(_other.gameObject.tag == "Electronics")
        if (_other.gameObject.CompareTag("Electronics"))
        {
            //플레이어는 전원만 넣는 활동만 하기 때문에 라디오 스크립트가 아니라 전자기기 스크립트를 가져온다.
            electronics = _other.GetComponent<Electronics>();
            Debug.Log("Get Electronics");
        }

        //상품일 경우 인벤토리 넣기
        if (_other.gameObject.CompareTag("Product"))
        {
            // GetProduct(_other.GetComponent<Product>());
            Product product = _other.GetComponent<Product>();
            GetProduct(product.GetProductType());
            Destroy(product.gameObject);
        }


        //멀어졌을 때 작동이 꺼진다.
    }
    private void OnTriggerExit(Collider _other)
    {
        if (_other.gameObject.CompareTag("Electronics"))
        {
            electronics = null;
        }
    }

    // player의 연락을 받을 함수
    public void OnMLBDown()
    {
        //Power On/Off

        if (electronics)
        {
            if (electronics.GetIsPowerOn())
                electronics.PowerOff();
            else
                electronics.PowerOn();

        }

        //Debug.Log("On Mouse Left Button");

    }

    public void OnMRBDown()
    {
        //Use
        if(electronics != null)
        {
            electronics.Use();
        }
        //Debug.Log("On Mouse Right Button");
    }

    public void UseProduct()
    {
        if (inventory.Count == 0) return;
        //Product product = inventory.Dequeue();
        //product.Use();

        VendingMachine.EVMProduct product = inventory.Dequeue();
        Product.UseWithType(product, this);

        //Product.UseWityType(inventory.Dequeue(), this);
    }

    public void GetProduct(VendingMachine.EVMProduct _product)
    {
        inventory.Enqueue(_product);

        StringBuilder sb = new StringBuilder();
        foreach(VendingMachine.EVMProduct product in inventory)
        {
            //string객체를 두개 만들어서 합친 후 다시 string객체를 떠넘기는 식이라 느리다.
            //sb.Append(product.name + " - ");
            //StringBuilder를 사용하게 되면 속도가 빠르게 된다. 단 +연산자를 사용이 불가능하다.
            //sb.Append(product.name);
            //sb.Append(" - ");
            sb.Append(product.ToString());
            sb.Append(" - ");
        }
        sb.Append("(");
        sb.Append(inventory.Count);
        sb.Append(")");
        Debug.Log(sb.ToString());
    }

    public void Buy(int _price)
    {
        if (_price > money) return;
        money -= _price;
        if (uiMoney) uiMoney.UpdateMoney(money);
    }

}

