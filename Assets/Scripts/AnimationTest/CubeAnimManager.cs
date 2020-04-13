using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAnimManager : MonoBehaviour
{
    CubeParameter[] intObjs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Click");
            AnimationStarter();
        }
    }
    public void AnimationStarter()
    {
           StartCoroutine("ShowAnim");
    }

    private IEnumerator ShowAnim()
    {
        Debug.Log("coru-tine starrrt");
        int k = 0;
        intObjs = GetComponentsInChildren<CubeParameter>();
        while (k < 3)
        {
            UIManager.animNum = k;
            foreach (CubeParameter ins in intObjs)
            {
                Debug.Log("ShowAnim" + k);
                ins.ChangeSize(k);

            }
            k++;
            yield return new WaitForSeconds(UIManager.waitTime);
        }
        UIManager._playingNow = false;
        UIManager._playStart = false;

    }
}
