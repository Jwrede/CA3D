using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideEdit : MonoBehaviour
{
    public GameObject Edit;
    public Button thisButton;
    public void hideEdit()
    {
        Edit.SetActive(thisButton.GetComponentInChildren<Text>().text != "Pause");
    }
}
