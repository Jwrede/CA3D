using UnityEngine;
using UnityEngine.UI;

public class RestrictSliderUpper : MonoBehaviour
{
    public Slider LowerSlider;
    public Slider UpperSlider;

    public void restrictSliderUpper()
    {
        if (UpperSlider.value < LowerSlider.value)
        {
            LowerSlider.value = UpperSlider.value;
        }
    }
}
