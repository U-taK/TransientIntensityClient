using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uOSC
{

    public class PositionSender : MonoBehaviour
    {

        Vector3 sendPos;
        Quaternion sendRotate;

        [SerializeField]
        MicPositionMirror micPositionMirror;

        uOscClient client;

        //トリガーで座標送信する
        // Use this for initialization
        void Start()
        {
            client = GetComponent<uOscClient>();
          //  StartCoroutine("SendData");
        }
        public void SendtoPC()
        {
            StartCoroutine("SendData");
        }

        private IEnumerator SendData()
        {
            //yield return new WaitForSeconds(1/6);

            if (UIManager._measure)
                {
                    micPositionMirror.GetSendInfo(out sendPos, out sendRotate);
                    client.Send("PositionSender", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"), sendPos.x, sendPos.y, sendPos.z, sendRotate.x, sendRotate.y, sendRotate.z, sendRotate.w);
                    Debug.Log("send"+ sendPos.x+","+ sendPos.y+","+sendPos.z);
                }
            //計算終了まで変更しない
            //UIManager._measure = false;
            yield return null;
        }
        

        public void SendSetting()
        {
            client.Send("SettingSender", UIManager.ColorMapID, UIManager.LevelMin, UIManager.LevelMax, UIManager.ObjSIze);
            Debug.Log("Setting FInish");
        }

        public void CalcInstantEnd(int num)
        {
            //生成し終えた番号を送信
            client.Send("InstantEnd",num);
        }
    }
}