using UnityEngine;
using UnityEngine.UI;
using System;

public class RandomString : MonoBehaviour
{
    public InputField Target;
    public Slider Colors;
    public Button Dimension;
    public Button Neighbourhood;
    public int size;

    public void randomString()
    {
        int dimension;
        if (Dimension.GetComponentInChildren<Text>().text == "1D")
        {
            dimension = Int32.Parse(Neighbourhood.GetComponentInChildren<Text>().text.Split(' ')[0]) + 1;
        }
        else if (Dimension.GetComponentInChildren<Text>().text == "2D")
        {
            dimension = Neighbourhood.GetComponentInChildren<Text>().text == "4 Neighbours" ? 5 : 9;
        }
        else
        {
            dimension = 1;
            Debug.Log("fix RandomString script");
        }
        int Base = (int) Colors.value+1;
        size = (int) Mathf.Pow(Base, dimension);
        string result = "";
        for (int i = 0; i < size; i++)
        {
            result += UnityEngine.Random.Range(0, Base);
        }
        Target.GetComponent<InputField>().text = result;
    }
}
