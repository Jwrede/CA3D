using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideElements : MonoBehaviour
{
    public Slider thisSlider;
    public Button Neighbourhood;
    public Slider ColorSlider;
    public Button RandomButton;
    public Slider LowerBound0;
    public Slider UpperBound0;
    public Slider LowerBound1;
    public Slider UpperBound1;
    public Button Dimension;
    public Slider InitialState;
    public Toggle UpdateInPlace;
    private GameObject[] Disable;
    private GameObject[] Enable;
    private bool UpdateInPlaceBefore;


    public void Start()
    {
        Enable = GameObject.FindGameObjectsWithTag("Enable");
        foreach(GameObject o in Enable)
        {
            o.SetActive(false);
        }
    }
    public void hideElements()
    {
        if (thisSlider.value == 1)
        {
            RandomButton.GetComponent<RandomString>().randomString();
            ColorSlider.value = 1;
            Disable = GameObject.FindGameObjectsWithTag("Disable");
            InitialState.value = 0.3f;
            UpdateInPlaceBefore = UpdateInPlace.isOn;
            UpdateInPlace.isOn = true;
        }
        else
        {
            InitialState.value = 0;
            UpdateInPlace.isOn = UpdateInPlaceBefore;
        }
        foreach (GameObject o in Disable)
        {
            o.SetActive(thisSlider.value == 0);
        }
        foreach (GameObject o in Enable)
        {
            o.SetActive(thisSlider.value != 0);
        }
        if (Dimension.GetComponentInChildren<Text>().text == "1D") Dimension.onClick.Invoke();
        if (Neighbourhood.GetComponentInChildren<Text>().text == "4 Neighbours") Neighbourhood.onClick.Invoke();
        LowerBound0.value = 3;
        UpperBound0.value = 3;
        LowerBound1.value = 2;
        UpperBound1.value = 3;
    }
}
