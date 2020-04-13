using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimManager : MonoBehaviour
{
    IntensityObject[] intObjs;
    bool playNow = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimationStarter()
    {
        if (!playNow)
            StartCoroutine("ShowAnim");
    }

    private IEnumerator ShowAnim()
    {
        int k = 0;
        intObjs = GetComponentsInChildren<IntensityObject>();
        while (k < UIManager._instanceNum)
        {
            UIManager.animNum = k;
            foreach(IntensityObject ins in intObjs)
            {
                Debug.Log("ShowAnim" + k);
                ins.ShowAnimation(k);

            }
            k+=2;
            yield return new WaitForSeconds(UIManager.waitTime);
        }
        playNow = false;
        UIManager._playingNow = false;
        UIManager._playStart = false;

    }

}
