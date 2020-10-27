using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeactivateColorPickers : MonoBehaviour
{
    public Canvas Parent;
    public void deactivateColorPickers()
    {
        int activeColor = (int) GetComponent<Slider>().value + 1;
        int maxValue = (int)GetComponent<Slider>().maxValue + 1;

        for (int i = 1; i <= maxValue; i++) {
            if (Parent.transform.Find("Color" + i).gameObject.activeSelf == true)
            {
                Parent.transform.Find("Color" + i).gameObject.SetActive(false);
            }
        }

        for(int i = 1; i <= activeColor; i++)
        {
            Parent.transform.Find("Color" + i).gameObject.SetActive(true);
        }
    }
}
