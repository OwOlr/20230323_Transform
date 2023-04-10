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

    //��ǰ ����ü ����
    [System.Serializable]   // ����� ���� �������� �����Ͽ� �ƽ�Ű�ڵ�� ��ȯ
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
    //����Ƽ���� �⺻ �ڷ����� ������������ ����� ���Ǵ� ����ȭ ���Ѿ� �Ѵ�.
    [SerializeField]    //�ʵ带 ����ȭ �����ش�.
    private List<SProductInfo> productInfoList = new List<SProductInfo>();  //���Ǳ��� �Ѱ踦 ���ֱ� ���� ���Ḯ��Ʈ�� ���

    private Player player = null;

    private void Awake()
    {
        if (uiMenu == null)
        {
            Debug.LogError("UIMenu is missing!");
            return;
        }

    }

    //��ǰ �Ǹ�, ����


    //UI ��ȣ�ۿ�
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            if (uiMenu)
            {
                uiMenu.gameObject.SetActive(true);
                uiMenu.BuildBtns(productInfoList,
                    OnClickMenu);

                //���� ��ȣ�ۿ� �� �÷��̾�
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

    //�ѱ���ġ
    // static ������ ���ص� �̹� �޸𸮿� �ö� �ֱ� ������ ���� ��ȿ�ϴ�.
    //���� ��� ���� : ���� ��ü�� ���� ���� �ϳ��� �����ϰ� �ȴ�.
    //���� ��� �Լ� : �����Ҵ� ���ص� ������ �����ϴ�.
    //���� �Ķ���͸� �������� �ʰ� UIMenuButton��Ʈ��Ʈ���� ��� �����ϴ�.
    //���� : ���� �Լ��������� ���� ������ ��� �����ϴ�. =>���� �ñⰡ �ٸ��� ������
    public static string VMProductToName(EVMProduct _product)
    {
        switch (_product)
        {
            case EVMProduct.Coke:
                return "�ݶ�";
            case EVMProduct.Tissue:
                return "����";
            case EVMProduct.Bread:
                return "��";
            case EVMProduct.LetsBe:
                return "������";
            case EVMProduct.Vita500:
                return "��Ÿ500";
            case EVMProduct.OronaminC:
                return "���γ��� C";
            case EVMProduct.Sprite:
                return "��������Ʈ";
            case EVMProduct.SSOrange:
                return "�ٽ� ������";
            default:
                return "��?��?��";
        }
    }


    //������ ���� �ڵ� ����

    public void OnClickMenu(int _btnNum, UIMenuButton _menuBtn)
    {
        //Debug.Log(productInfoList[_btnNum].product.ToString() +
        //    "(" + productInfoList[_btnNum].price +
        //    ") : " + productInfoList[_btnNum].stock);

        if (player == null) return;

        if (!productInfoList[_btnNum].CheckStock()) return;

        //�ܾ��� ������� �ʴ� ���
        //if(���� ����Ѱ�?(�ݾ�))
        if (productInfoList[_btnNum].price > player.Money)
        {
            Debug.Log("!!�ܾ��� �����մϴ�!!");
            return;
        }


        //�ӽð�ü�� ����
        //--productInfoList[selectedIdx].stock;
        //�Լ�������� ���� -> ���������� �ȵƴ�.
        //productInfoList[_btnNum].Sell();
        //����Ʈ�� ������·� ����
        //C++�� �ٷ� ����ü ������ �����ϴ�.
        //C#�� ���� ������ �ƴ� �Ź� ����ü�� �����Ѵ�.
        //class ���������̶� �����ϴ�.
        //�Լ����ĵ� ���纻�� ������ �����ϴ�.
        //���纻�� �����ϰ� ������ ��� �������ִ� ������� �ذ�

        SProductInfo changeInfo = productInfoList[_btnNum];
        //--changeInfo.stock;
        changeInfo.Sell();
        productInfoList[_btnNum] = changeInfo;


        // ��ư ���� ����
        //uiMenu.UpdateButtonInfo(_btnNum, changeInfo);
        //��ü������ �������� �����ս��� ì�� �� �ִ�.
        _menuBtn.UpdateInfo(changeInfo);

        //��ǰ�����

        GameObject prefab = ProductSpawnManager.GetPrefab(
            productInfoList[_btnNum].product);


        if (prefab != null)
        {
            Transform vmTr = GetComponent<Transform>();
            //���Ǳ� �ֺ��� ����
            //���Ǳ� �ֺ� ��ֹ��� ���ϸ鼭 ��ġ.
            Instantiate(prefab, GetValidSpawnPosition(),
                Quaternion.identity);

        }

        //���� �� ����
        player.Buy(productInfoList[_btnNum].price);



    }

    private Vector3 Set(float _angle)
    {
        //�ݶ��̴��� �̿��� �浹 �˰���.
        //���� ���ڸ��� �������� �ݶ��̴��� ����� ���� �� retrun.
        //return�� �� �ٸ� ��ġ�� �ٽ� ����
        //������ �� ���� �˻� - �� �������� �����ٴ� ����.

        //2.physics���� ������ �˻� 
        //BoxCast�� �����Ǳ� ���� ���� �� ������ ���� �� �ڸ��� ����
        //Box�� �˻緮�� ����, OBB�˻� ������ ����� ũ�� ��Ȯ���� ����.

        //3.RayCast
        //������ ��ġ�� �������� ����� �ɸ��� ����X
        //BoxCast�� ���� ��Ȯ���� ������ ���ɼ��� �ִ�.

        Transform vmTr = GetComponent<Transform>();


        float angle2Rad = _angle * Mathf.Deg2Rad;
        Vector3 anglePos = new Vector3(Mathf.Cos(angle2Rad), 0, Mathf.Sin(angle2Rad));



        return vmTr.position + (anglePos * 2);
    }

    private Vector3 GetValidSpawnPosition()
    {

        const float SPAWN_DIST = 3f;            // ���� �Ÿ� = ������
        const float PI2 = Mathf.PI * 2f;        // 360���� ��ġ�ϱ� ����
        const float POS_Y = 0.5f;               // �ٴڿ��� �˻��ϸ� ��Ȯ���� ��������� ���� ���, ���߿� 0���� ����

        Vector3 startPos = transform.position;  // ���̽�� ������ ��ġ
        startPos.y = POS_Y;
        bool isValidPos = false;                // ��ȿ�� ��ġ����
        float angle = 0f;                       // ���� ���� ��������
        RaycastHit hitInfo;                     // ���� ��Ʈ ����

        Vector3 spawnPos = Vector3.zero;        // ������ ��ġ
        while (!isValidPos)
        {
            // ���� ���� ���
            angle = Random.Range(0f, PI2);
            spawnPos = transform.position +
                new Vector3(
                    Mathf.Cos(angle) * SPAWN_DIST,
                    POS_Y,
                    Mathf.Sin(angle) * SPAWN_DIST
                );

            Vector3 dir = (spawnPos - startPos).normalized;
            //dir.normalized();
            // ������ ��ġ �ĺ��� ���� ���� �浹�˻�
            //RayCast -> BoxCast�� ������ ������.
            if (Physics.Raycast(startPos, dir, out hitInfo, SPAWN_DIST))
            {
                // �浹�� ������Ʈ�� �ִٸ� �ٸ� ��ġ�� ã�ƾ� ��
                Debug.Log("Raycast Hit: " + hitInfo.transform.name);
                continue;
            }

            isValidPos = true;
        }

        // ���̸� �ٽ� 0����
        spawnPos.y = 0f;
        return spawnPos;
    }
}
