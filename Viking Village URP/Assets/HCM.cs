using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HCM : MonoBehaviour
{
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("1")) {
            GetComponent<UniversalAdditionalCameraData>().SetRenderer(0);
        }
        if(Input.GetKeyDown("2")) {
            GetComponent<UniversalAdditionalCameraData>().SetRenderer(2);
        }
        if(Input.GetKeyDown("3")) {
            SetLayerRecursively(obj, LayerMask.GetMask("Background"));
        }
    }
    void SetLayerRecursively(GameObject obj, int newLayer  )
    {
        obj.layer = newLayer;
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            SetLayerRecursively( obj.transform.GetChild(i).gameObject, newLayer );
        }
    }
}
