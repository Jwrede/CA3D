using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateValAmount : MonoBehaviour
{
    public Slider thisSlider;
    public void updateValAmount()
    {
        thisSlider.GetComponentInChildren<Text>().text = thisSlider.value.ToString();
    }
}
