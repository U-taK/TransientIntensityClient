///<summary>
///マイクマーカによって取得した座標情報を取得し、空間基準マーカの相対座標を得る
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicPositionMirror : MonoBehaviour {

    [SerializeField]//マイクマーカによって取得する計測点情報
    GameObject micPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = micPos.gameObject.transform.position;
        this.transform.rotation = micPos.gameObject.transform.rotation;
	}

    //計測点の座標情報を逐次送信
    public void GetSendInfo(out Vector3 vector3, out Quaternion quaternion)
    {
        vector3 = this.transform.localPosition;
        quaternion = this.transform.localRotation;
    }
}
