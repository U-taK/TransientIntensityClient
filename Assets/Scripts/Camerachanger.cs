///<summary>
///シェアリング側はアンカーポイントを決めたらARカメラを切る
/// 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Camerachanger : MonoBehaviour {


    [SerializeField]
    VuforiaBehaviour vuforiaBehaviour;
    [SerializeField]
    GameObject standardMarker;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (standardMarker != null)
        {
            this.transform.position = standardMarker.transform.position;
            this.transform.localRotation = standardMarker.transform.localRotation;
        }
	}

    public void VuforiaDown()
    {
        vuforiaBehaviour.enabled = false;
    }
}
