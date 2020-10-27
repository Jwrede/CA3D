using UnityEngine;
using UnityEngine.UI;

public class RestrictSliderLower : MonoBehaviour
{
    public Slider LowerSlider;
    public Slider UpperSlider;

    public void restrictSliderLower()
    {
        if(LowerSlider.value > UpperSlider.value)
        {
            UpperSlider.value = LowerSlider.value;
        }
    }
}
