using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public GameObject CA;
    public Button Pause;
    public GameObject Edit;
    public GameObject StartStop;
    private bool setEdit = true;
    public void test()
    {
        if (!StartStop.activeSelf) StartStop.SetActive(true);
        if (setEdit)
        {
            Edit.SetActive(true);
            setEdit = false;
        }
        float amount = GameObject.Find("Amount").GetComponent<Slider>().value;
        CA.GetComponent<Basic>().init(amount);
    }
}
