using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDimension : MonoBehaviour
{
    private int currDimension = 1;
    public Slider ColorSlider;
    public Toggle Time;
    public Slider AmountSlider;
    public void changeDimension()
    {
        currDimension++;
        if (currDimension > 2) currDimension = 1;
        if (currDimension == 2)
        {
            AmountSlider.maxValue = 100;
            ColorSlider.maxValue = 2;
        }
        if (currDimension == 1)
        {
            AmountSlider.maxValue = 1000;
            ColorSlider.maxValue = 4;
        }
        GetComponentInChildren<Text>().text = "" + currDimension + "D";
    }
}
