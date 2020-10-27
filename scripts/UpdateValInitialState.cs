using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateValInitialState : MonoBehaviour
{
    public Slider thisSlider;
    public void updateValInitialState()
    {
        if(thisSlider.value != 0) thisSlider.GetComponentInChildren<Text>().text = (thisSlider.value * 100).ToString("F0") + "%";
        else thisSlider.GetComponentInChildren<Text>().text = "1 Block";
    }
}
