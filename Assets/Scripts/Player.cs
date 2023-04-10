using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Player : MonoBehaviour
{
    private PlayerController playerCtrl = null;
    //���۽�ų �������� ����.
    private Electronics electronics = null;

    //�κ��丮
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
        //�ֻ����� ������Ʈ�� tag�� �ٲ�� �ν��Ѵ�. (����)
        //if(_other.gameObject.tag == "Electronics")
        if (_other.gameObject.CompareTag("Electronics"))
        {
            //�÷��̾�� ������ �ִ� Ȱ���� �ϱ� ������ ���� ��ũ��Ʈ�� �ƴ϶� ���ڱ�� ��ũ��Ʈ�� �����´�.
            electronics = _other.GetComponent<Electronics>();
            Debug.Log("Get Electronics");
        }

        //��ǰ�� ��� �κ��丮 �ֱ�
        if (_other.gameObject.CompareTag("Product"))
        {
            // GetProduct(_other.GetComponent<Product>());
            Product product = _other.GetComponent<Product>();
            GetProduct(product.GetProductType());
            Destroy(product.gameObject);
        }


        //�־����� �� �۵��� ������.
    }
    private void OnTriggerExit(Collider _other)
    {
        if (_other.gameObject.CompareTag("Electronics"))
        {
            electronics = null;
        }
    }

    // player�� ������ ���� �Լ�
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
            //string��ü�� �ΰ� ���� ��ģ �� �ٽ� string��ü�� ���ѱ�� ���̶� ������.
            //sb.Append(product.name + " - ");
            //StringBuilder�� ����ϰ� �Ǹ� �ӵ��� ������ �ȴ�. �� +�����ڸ� ����� �Ұ����ϴ�.
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

