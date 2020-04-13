using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntensityObject : MonoBehaviour
{
    //public List<Vector3> tranIntensity;
    //public List<Color> colors;

    public Vector3[] tranIntensity = new Vector3[UIManager._instanceNum];
    public Color[] colors = new Color[UIManager._instanceNum];
    public Vector3[] scales = new Vector3[UIManager._instanceNum];
    public GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowAnimation(int k)
    {
        Debug.Log("ShowAnimation");
        if (tranIntensity[k].x != 0)
        {
            child.transform.localRotation = Quaternion.LookRotation(10000000000 * tranIntensity[k]);
            child.transform.GetComponent<Renderer>().material.color = colors[k];
            child.transform.localScale = scales[k]*UIManager.ObjSIze;                
        }
        else
        {
            child.transform.localScale = Vector3.zero;
        }
    }
}
