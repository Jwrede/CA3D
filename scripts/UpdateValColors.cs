using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateValColors : MonoBehaviour
{
    public Slider thisSlider;
    public void updateValColors()
    {
        thisSlider.GetComponentInChildren<Text>().text = (thisSlider.value + 1).ToString();
    }
}
