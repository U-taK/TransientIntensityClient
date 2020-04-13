using HoloToolkit.Examples.InteractiveElements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uOSC;

public class UIManager : MonoBehaviour {

    //colormapの種類を選択 0:grayscale,1:parula,2:jet
    public static int ColorMapID = 1;
    //表示したいレベルの範囲を指定
    public static float LevelMin = 60;
    public static float LevelMax = 105;
    //コーンのサイズ
    public static float ObjSIze = 0.05f;
    //計測可能か？
    public static bool _measure = false;
    public static bool _instance = false;
    public static bool _playMode = false;
    public static bool _playingNow = false;
    public static bool _playStart = false;
    public static float waitTime = 0.1f;
    public static int animNum = 0;
    //瞬時音響インテンシティ算出量
    public static int _instanceNum = 2048;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
