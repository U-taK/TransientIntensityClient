using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace uOSC
{

    [RequireComponent(typeof(uOscServer))]
    public class CatchParamLog : MonoBehaviour
    {
        void Start()
        {
            var server = GetComponent<uOscServer>();
            server.onDataReceived.AddListener(OnDataReceived);
        }

        void OnDataReceived(Message message)
        {
            // address
            var msg = message.address + ": ";
            if (msg == "PositionSender: ")
            {
                // timestamp
                //msg += "(" + message.timestamp.ToLocalTime() + ") ";

                // values
                foreach (var value in message.values)
                {
                    msg += value.GetString() + " ";
                }

                Debug.Log(msg);
            }
        }
    }

}