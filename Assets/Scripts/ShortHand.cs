using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortHand : MonoBehaviour
{
    private Transform shTr = null;
    private float angle = 0f;
    private float speed = 10f;
    private float dis = 10f;

    private void Awake()
    {
        shTr = transform.GetChild(0);
    }

    private void Update()
    {
        angle += Time.deltaTime * speed;
        if (angle > 360) angle = 0;
        Debug.Log("angle = "+ angle);

        float x = Mathf.Cos((angle * Mathf.Deg2Rad));
        float y = 0.5f;
        float z = Mathf.Sin((angle * Mathf.Deg2Rad));

        shTr.position = new Vector3(x*dis, y, z*dis);
        
    }
}
