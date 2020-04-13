using UnityEngine;

namespace uOSC
{

[RequireComponent(typeof(uOscClient))]
public class ClientTest : MonoBehaviour
{
        object[] sample = { 0.1f, 0.2f, 0.3f };
    void Update()
    {
        var client = GetComponent<uOscClient>();
            //client.Send("/uOSC/test", sample[0],10, "hoge", "hogehoge", 1.234f, 123f);
            client.Send("test", sample);
    }
}

}