using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicPosDisplay : MonoBehaviour {

    [SerializeField]
    TextMesh uiLog;
    [SerializeField]
    MicPositionMirror micPositionMirror;

    Vector3 logPos;
    Quaternion logRot;

    int counter = 0;
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        LoggingSetting(counter);
        counter++;
	}

    void LoggingSetting(int counter)
    {
        micPositionMirror.GetSendInfo(out logPos, out logRot);

        uiLog.text = string.Format(@"{0}
Pos[x:{1} y:{2} z:{3}]
Rotate[x:{4} y:{5} z:{6} w:{7}]", DateTime.Now.ToString("MM/dd/HH/mm/ss.fff"), logPos.x.ToString("f2"), logPos.y.ToString("f2"), logPos.z.ToString("f2"),
logRot.x.ToString("f2"), logRot.y.ToString("f2"), logRot.z.ToString("f2"), logRot.w.ToString("f2"));
    }
}
