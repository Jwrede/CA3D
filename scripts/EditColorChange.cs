using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditColorChange : MonoBehaviour
{
    public GameObject CellularAutomata;
    public GameObject thisButton;
    public GameObject canvas;
    [HideInInspector] public int currColor;

    public void init()
    {
        currColor = 1;
        changeButtonText();
    }
    public void editColorChange()
    {
        Basic script = CellularAutomata.GetComponent<Basic>();
        currColor = currColor == script.colors ? 1 : currColor + 1;
        changeButtonText();
    }

    private void changeButtonText()
    {
        foreach (Transform t in canvas.transform)
        {
            if (t.name == "Color" + (currColor + 1))
            {
                Dropdown d = t.GetComponent<Dropdown>();
                thisButton.GetComponentInChildren<Text>().text = d.options[d.value].text;
            }
        }
    }
}
