using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateValUpdateTime : MonoBehaviour
{
    public Slider thisSlider;
    public void updateValUpdateTime()
    {
        thisSlider.GetComponentInChildren<Text>().text = thisSlider.value.ToString("F1") + " Sec";
    }
}
