using System;
using UnityEngine;
using UnityEngine.UI;

public class ChangeNeighbourhood : MonoBehaviour
{
    public Button Dimension;
    public Button Neighbourhood;
    public GameObject MinMaxSliders;
    
    public void changeNeighbourhood()
    {
        if(Dimension.GetComponentInChildren<Text>().text == "1D")
        {
            int neighbourhoodBefore = Int32.Parse(Neighbourhood.GetComponentInChildren<Text>().text.Split(' ')[0]);
            Neighbourhood.GetComponentInChildren<Text>().text = neighbourhoodBefore < 6 ? "" + (neighbourhoodBefore + 2) + " Neighbours" : "" + 2 + " Neighbours";
        }
        else if (Dimension.GetComponentInChildren<Text>().text == "2D")
        {
            Neighbourhood.GetComponentInChildren<Text>().text = Neighbourhood.GetComponentInChildren<Text>().text == "4 Neighbours" ? "8 Neighbours" : "4 Neighbours";
        }

        foreach(Transform child in MinMaxSliders.transform)
        {
            foreach(Slider s in child.gameObject.GetComponentsInChildren<Slider>())
            {
                s.maxValue = Int32.Parse(Neighbourhood.GetComponentInChildren<Text>().text[0].ToString());
            }
        }
    }
}
