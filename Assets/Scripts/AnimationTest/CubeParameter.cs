using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeParameter : MonoBehaviour
{
    public GameObject child;
    Vector3[] scales = new Vector3[3];

    // Start is called before the first frame update
    void Start()
    {
        scales[0] = Vector3.one * 0.8f;
        scales[1] = Vector3.one * 0.5f;
        scales[2] = Vector3.one * 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSize(int k)
    {
        Debug.Log("ChangeSize");
        child.transform.localScale = scales[k];
        
    }
}
