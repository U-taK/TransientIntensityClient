using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LoadData : MonoBehaviour
{

    //原点座標を示すマーカ
    [SerializeField]
    GameObject standardTarget;

    public Text saveDataPath;

    private string dataPath = null;

    private SaveData LoadElem;

    //コーン
    public GameObject prefab;

    //loadするオブジェクトの仮置き
    GameObject measurePoint;
    GameObject intensityObj;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //デバック用
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DataLoad();
        }
    }

    public void DataLoad()
    {
        //jsonファイルを探してSaveData型で読み込む
        string json = null;
        dataPath = Application.persistentDataPath + "/" + saveDataPath.text;
        Debug.Log("DataPath is" + dataPath);
        if (File.Exists(dataPath))
        {
            json = File.ReadAllText(dataPath);

            LoadElem = (SaveData)JsonUtility.FromJson(json, typeof(SaveData));
        }
        else
        {
            Debug.Log("DataPath: " + dataPath + " is null");
        }

        //オブジェクトを生成する
        for (int i = 0; i < LoadElem.intensLev.Length; i++)
        {
            measurePoint = new GameObject();
            measurePoint.transform.parent = standardTarget.transform;
            measurePoint.transform.localPosition = new Vector3(LoadElem.micPosx[i], LoadElem.micPosy[i], LoadElem.micPosz[i]);
            measurePoint.transform.localRotation = new Quaternion(LoadElem.micRotx[i], LoadElem.micRoty[i], LoadElem.micRotz[i], LoadElem.micRotw[i]);
            measurePoint.transform.name = "measurepoint" + i;

            //コーンの色を指定
            Color vecObjColor = ColorBar.DefineColor(LoadElem.colorMap, LoadElem.intensLev[i], LoadElem.minRange, LoadElem.maxRange);

            // intensityObj = new GameObject();
            Vector3 intensity = new Vector3(LoadElem.intensx[i], LoadElem.intensy[i], LoadElem.intensz[i]);
            intensityObj = CreateVecObj(prefab, measurePoint, intensity, LoadElem.size, vecObjColor);
        }
    }

    //オブジェクト生成
    GameObject CreateVecObj(GameObject prefab, GameObject objParent, Vector3 intensity, float ObjSize, Color vecColor)
    {
        GameObject OutputObj = GameObject.Instantiate(prefab) as GameObject;
        OutputObj.transform.parent = objParent.transform;
        OutputObj.transform.localPosition = Vector3.zero;
        OutputObj.transform.localRotation = Quaternion.LookRotation(10000000000 * intensity);
        OutputObj.transform.localScale = new Vector3(ObjSize, ObjSize, ObjSize * 4f);
        OutputObj.transform.GetComponent<Renderer>().material.color = vecColor;
        OutputObj.name = "VectorObject";
        return OutputObj;
    }

}
