using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

namespace uOSC
{
    public class IntensityManager : MonoBehaviour, IInputClickHandler
    {
        PositionSender positionSender;
        //送信されたデータを格納するためのList
        List<string> transformData = new List<string>();
        int i = 0;

        // HoloLens上に保存するためのList
        List<DataStorage> dataList = new List<DataStorage>();
        
        // HoloLens上の保存先パス
        private string saveDataPath;

        InstanceManager instanceManager;
        Vector3 micPos;
        Quaternion micRot;

        bool animReady = false;

        [SerializeField]
        UIPanelManager uIPanelManager;

        [SerializeField]
        TextMesh putNum;
        // Start is called before the first frame update
        void Start()
        {
            
            InputManager.Instance.PushFallbackInputHandler(gameObject);

            //座標送信用スクリプトを呼び出し
            positionSender = gameObject.GetComponent<PositionSender>();
            instanceManager = GetComponent<InstanceManager>();
            var server = GetComponent<uOscServer>();
            server.onDataReceived.AddListener(OnDataReceived);

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnInputClicked(InputClickedEventData eventData)
        {
            //Airtapで座標送信
            positionSender.SendtoPC();
            if (UIManager._playMode && !UIManager._playingNow)
                uIPanelManager.PlayStart();
        }

        void OnDataReceived(Message message)
        {
            StartCoroutine(DataProcess(message));
        }

        private IEnumerator DataProcess(Message message)
        {
            //address
            var msg = message.address;
            Debug.Log("Catch the address: " + msg);
            if (msg == "ResultSend" && UIManager._instance)
            {
                //value
                foreach (var value in message.values)
                {
                    transformData.Add(value.ToString());
                }
                Vector3 Intensity = new Vector3(float.Parse(transformData[i + 7]), float.Parse(transformData[i + 8]), float.Parse(transformData[i + 9]));

                float intensityLv = AIMath.CalcuIntensityLevel(Intensity);
                DataStorage instanceData;
                //インテンシティレベルが指定した範囲内なら
                if (intensityLv >= UIManager.LevelMin || intensityLv <= UIManager.LevelMax)
                {
                    //コーンの色を指定
                    Color vecObjColor = ColorBar.DefineColor(UIManager.ColorMapID, intensityLv, UIManager.LevelMin, UIManager.LevelMax);
                    micPos = new Vector3(float.Parse(transformData[i]), float.Parse(transformData[i + 1]), float.Parse(transformData[i + 2]));
                    micRot = new Quaternion(float.Parse(transformData[i + 3]), float.Parse(transformData[i + 4]), float.Parse(transformData[i + 5]), float.Parse(transformData[i + 6]));
                    GameObject instant= instanceManager.CreateInstantObj(int.Parse(transformData[i + 10]), micPos, micRot, Intensity, vecObjColor, UIManager.ObjSIze);
                    instanceData = new DataStorage(int.Parse(transformData[i + 10]), micPos, micRot, Intensity,intensityLv);
                    dataList.Add(instanceData);
                }
                i += 11;
               // UIManager._measure = true;
                yield return null;
            }
            //サンプリングレート分の瞬時音響インテンシティデータの受信(No+インテンシティ)
            else if (msg == "InstanceSend")
            {
                List<Vector3> transInt = new List<Vector3>();
                List<Color> colors = new List<Color>();
                List<Vector3> scales = new List<Vector3>();
                
                foreach (var value in message.values)
                {
                    transformData.Add(value.ToString());
                }
                int Num = int.Parse(transformData[i]);
                i++;
                int part = int.Parse(transformData[i]);
                i++;
                for (; i < transformData.Count; i += 3)
                {
                    animReady = false;
                    Vector3 Intensity = new Vector3(float.Parse(transformData[i]), float.Parse(transformData[i + 1]), float.Parse(transformData[i + 2]));
                    float intensityLv = AIMath.CalcuIntensityLevel(Intensity);
                    //コーンの色を指定
                    Color ObjColor = ColorBar.DefineColor(UIManager.ColorMapID, intensityLv, UIManager.LevelMin, UIManager.LevelMax);
                    transInt.Add(Intensity);
                    colors.Add(ObjColor);
                    scales.Add(DefineSize(intensityLv, UIManager.LevelMin, UIManager.LevelMax));
                }
                //オブジェクトをインテンシティに計算し終えたらデータ送信
                positionSender.CalcInstantEnd(Num);
                yield return null;
                instanceManager.PushIntensityObj(Num, part, transInt, colors,scales);
                putNum.text = Num.ToString();
                animReady = true;
                yield return null;

            }
            else if (msg == "SendEnd")
            {
                uIPanelManager.DisplayReady();
                yield return null;
            }
            else if (msg == "SettingSender")
            {
                UIManager.ColorMapID = int.Parse(message.values[0].ToString());
                UIManager.LevelMin = float.Parse(message.values[1].ToString());
                UIManager.LevelMax = float.Parse(message.values[2].ToString());
                UIManager.ObjSIze = float.Parse(message.values[3].ToString());
                uIPanelManager.ReadyMeasure();
            }
        }

        private Vector3 DefineSize(float level, float min, float max)
        {
            float colorScale = (level - min) / (max - min);
            if(colorScale > 1)
            {
                return new Vector3(1f, 1f, 4f);
            }
            else if(colorScale >= 0 && colorScale <= 1)
            {
                return new Vector3(colorScale, colorScale, colorScale * 4f);
            }
            else
            {
                return Vector3.zero;
            }
        }

        public void Save()
        {
            int thisMonth = DateTime.Today.Month;
            int thisDay = DateTime.Today.Day;
            int thisHour = DateTime.Today.Hour;
            int thisMin = DateTime.Today.Minute;
            // セーブデータの格納先
            saveDataPath = Application.persistentDataPath + "/" + thisMonth.ToString() + thisDay.ToString() + thisHour.ToString() + thisMin.ToString() + "Intensitydata.json";
            //測定データの数
            int Num = dataList.Count - 1;
            SaveData saveData = new SaveData();
            //要素の初期化
            saveData.micPosx = new float[Num];
            saveData.micPosy = new float[Num];
            saveData.micPosz = new float[Num];
            saveData.micRotx = new float[Num];
            saveData.micRoty = new float[Num];
            saveData.micRotz = new float[Num];
            saveData.micRotw = new float[Num];
            saveData.intensx = new float[Num];
            saveData.intensy = new float[Num];
            saveData.intensz = new float[Num];
            saveData.intensLev = new float[Num];

            //設定の保存
            saveData.colorMap = UIManager.ColorMapID;
            saveData.minRange = UIManager.LevelMin;
            saveData.maxRange = UIManager.LevelMax;
            saveData.size = UIManager.ObjSIze;

            //測定データの保存
            for (int i = 0; i < Num; i++)
            {
                saveData.micPosx[i] = dataList[i].micLocalPos.x;
                saveData.micPosy[i] = dataList[i].micLocalPos.y;
                saveData.micPosz[i] = dataList[i].micLocalPos.z;
                saveData.micRotx[i] = dataList[i].micLocalRot.x;
                saveData.micRoty[i] = dataList[i].micLocalRot.y;
                saveData.micRotz[i] = dataList[i].micLocalRot.z;
                saveData.micRotw[i] = dataList[i].micLocalRot.w;
                saveData.intensx[i] = dataList[i].intensityDir.x;
                saveData.intensy[i] = dataList[i].intensityDir.y;
                saveData.intensz[i] = dataList[i].intensityDir.z;
                saveData.intensLev[i] = dataList[i].intensityLv;
            }
            //データの保存
            string json = JsonUtility.ToJson(saveData);
            File.WriteAllText(saveDataPath, json);
            Debug.Log("Save in " + saveDataPath);
        }
    }
}