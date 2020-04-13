using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uOSC
{
    public class InstanceManager : MonoBehaviour
    {

        //表示するプレファブ
        public GameObject cone;
        //空間基準マーカ
        [SerializeField]
        GameObject standardMarker;
        static int measureNo = 0;
        //計測番号と計測結果の連想配列
        Dictionary<int,GameObject> intensities = new Dictionary<int, GameObject>();

        //オブジェクト生成
        public GameObject CreateInstantObj(int No, Vector3 micPos, Quaternion micRot, Vector3 intensity, Color vecColor, float objSize)
        {
            GameObject msPoint = new GameObject("measurepoint" + No);
            msPoint.transform.parent = standardMarker.transform;
            msPoint.transform.localPosition = micPos;
            msPoint.transform.localRotation = micRot;
            intensities.Add(No, msPoint);
            GameObject VectorObj = Instantiate(cone) as GameObject;
            VectorObj.transform.localScale = new Vector3(objSize, objSize, objSize * 4f);
            VectorObj.transform.parent = msPoint.transform;
            VectorObj.transform.localPosition = Vector3.zero;
            VectorObj.transform.localRotation = Quaternion.LookRotation(10000000000 * intensity);
            VectorObj.transform.GetComponent<Renderer>().material.color = vecColor;
            VectorObj.name = "IntensityObject";
            measureNo = No;
            var storage = msPoint.AddComponent<IntensityObject>();
            storage.child = VectorObj;
            return msPoint;
        }
        //瞬時音響インテンシティ受信時の対応
        public void PushIntensityObj(int No, int part, List<Vector3> insIntensity, List<Color> colors, List<Vector3> scales)
        {
            var pushObj = intensities[No];
            var storage = pushObj.GetComponent<IntensityObject>();
            //瞬時音響インテンシティをリストに保持させる
            insIntensity.ToArray().CopyTo(storage.tranIntensity, part*64);
            colors.ToArray().CopyTo(storage.colors, part * 64);
            scales.ToArray().CopyTo(storage.scales, part * 64);
        }
    }

}
