using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpDownValueButton : MonoBehaviour
{

    TextMesh valueUiText;

    float interval = 5f;

    float num;

    void Start()
    {
        valueUiText = this.GetComponent<TextMesh>();
    }

    private void Update()
    {
        num = float.Parse(valueUiText.text);
        if (num > 10) interval = 5;
        else if (num > 1 && num <= 10) interval = 1;
        else if (num > 0.1 && num <= 1) interval = 0.1f;
        else if (num > 0.01 && num <= 0.1) interval = 0.01f;
        else if (num < 0.01) interval = 0.001f;
    }
    public void OnUpButtonClicked()
    {
        float setValue = float.Parse(valueUiText.text) + interval;
        valueUiText.text = setValue.ToString("f3");
    }

    public void OnDownButtonClicked()
    {
        float setValue = float.Parse(valueUiText.text) - interval;
        valueUiText.text = setValue.ToString("f3");
    }
}
