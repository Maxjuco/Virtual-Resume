using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Slider slider;

    public Gradient gradient;
    public Image fill;

    /*function which initiate the max health of the player and set the fill to this max value*/
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        //initialize the color to the gradient value 1 or 100%(the most right one)
        fill.color = gradient.Evaluate(1f);
    }


    /*function which set the actual health*/
    public void SetHealth(int health)
    {
        slider.value = health;

        //get the value of the slider and adapt to put them to 0 and 1 and take the correspondant color of this value on the gradient [0;1] OR 0% to 100%
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }



}
