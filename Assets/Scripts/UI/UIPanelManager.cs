using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelManager : MonoBehaviour {

    enum measureType
    {
        animation,measure
    }

    measureType type = measureType.measure;
    
    [SerializeField]
    GameObject measurePanel;
    [SerializeField]
    GameObject readyPanel;
    [SerializeField]
    GameObject animationPanel;
    [SerializeField]
    AnimManager anim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //計測準備完了
    public void ReadyMeasure()
    {
        measurePanel.SetActive(false);
        readyPanel.SetActive(true);
    }
    //計測スタート
    public void StartMeasure()
    {
        readyPanel.SetActive(false);
        UIManager._measure = true;
        UIManager._instance = true;
        this.gameObject.SetActive(false);
    }

    public void DisplayReady()
    {
        this.gameObject.SetActive(true);
        animationPanel.SetActive(true);
    }

    public void PlayStart()
    {
        UIManager._playMode = true;
        UIManager._playingNow = true;
        UIManager._playStart = true;
        UIManager._measure = false;
        this.gameObject.SetActive(false);
        anim.AnimationStarter();
    }
    //
}
