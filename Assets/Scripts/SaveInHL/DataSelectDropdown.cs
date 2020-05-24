using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DataSelectDropdown : MonoBehaviour
{

    FileInfo[] files;
    string saveDataPath;
    string selectedData;
    [SerializeField]
    Dropdown dropdown;

    // Use this for initialization
    void Start()
    {
        saveDataPath = Application.persistentDataPath;
        DirectoryInfo di = new DirectoryInfo(saveDataPath);
        files = di.GetFiles("*.json", SearchOption.AllDirectories);
        foreach (FileInfo f in files)
        {
            dropdown.options.Add(new Dropdown.OptionData { text = f.Name });
        }
        dropdown.RefreshShownValue();
        selectedData = files[dropdown.value].FullName;
    }

    public void OnDropdownValueChanged()
    {
        selectedData = files[dropdown.value].FullName;
        Debug.Log(selectedData);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
