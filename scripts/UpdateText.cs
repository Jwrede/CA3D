using UnityEngine;
using UnityEngine.UI;

public class UpdateText : MonoBehaviour
{
    public Slider thisSlider;
    public Text text;

    public void updateText()
    {
        text.text = thisSlider.value.ToString();
    }
}
