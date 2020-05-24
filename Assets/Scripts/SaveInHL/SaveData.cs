//取ったデータオブジェクトの情報を保存するクラス
[System.Serializable]
public class SaveData
{
    //測定設定
    public int colorMap;
    public float minRange, maxRange;
    public float size;
    public float interval;
    //測定データ
    public float[] micPosx, micPosy, micPosz;
    public float[] micRotx, micRoty, micRotz, micRotw;
    public float[] intensx, intensy, intensz;
    public float[] intensLev;
}