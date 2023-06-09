using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductSpawnManager
{

    //Singleton Pattern 싱글톤 패턴
    private static List<GameObject> productPrefabList = null;

    //파일을 불러오는 함수
    private static void LoadPrefabs()
    {
        //if (productPrefabList != null) return;
        if (!System.Object.ReferenceEquals(productPrefabList, null)) return;
        
        GameObject[] prefabs = Resources.LoadAll<GameObject>(
            "Prefabs\\Products"
            ); ;

        productPrefabList = new List<GameObject>(prefabs);

    }

    public static GameObject GetPrefab(VendingMachine.EVMProduct _productType)
    {

        if(productPrefabList == null)
            LoadPrefabs();

        //switch (_productType)
        //{
        //    case VendingMachine.EVMProduct.Coke:
        //        productPrefabList[(int)Coke]
        //        break;
        //}
        foreach(GameObject prefab in productPrefabList)
        {
            Product product = prefab.GetComponent<Product>();
            if(product.GetProductType() == _productType)
            {
                return prefab;
            }
        }
        return null;
    }
}
