using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Product : MonoBehaviour
{
    [SerializeField]
    private VendingMachine.EVMProduct productType;

    public
    VendingMachine.EVMProduct
    GetProductType
    ()
    {
        return productType;
    }
    public abstract void Use();

    public static void UseWithType(VendingMachine.EVMProduct _productType, Player _player)
    {
        switch (_productType)
        {
            case VendingMachine.EVMProduct.Coke:
                Debug.Log("Drink - Coke");
                break;
            case VendingMachine.EVMProduct.Tissue:
                Debug.Log("Use - Tissue");
                break;
            case VendingMachine.EVMProduct.Bread:
                Debug.Log("Eat _ Bread");
                break;
            case VendingMachine.EVMProduct.LetsBe:
                Debug.Log("Drink - LetsBe");
                break;
            case VendingMachine.EVMProduct.Vita500:
                Debug.Log("Drink - Vita500");
                break;
            case VendingMachine.EVMProduct.OronaminC:
                Debug.Log("Drink - OronaminC");
                break;
            case VendingMachine.EVMProduct.Sprite:
                Debug.Log("Drink - Sprite");
                break;
            case VendingMachine.EVMProduct.SSOrange:
                Debug.Log("Drink - SSOrange");
                break;
        }
    }
}
