using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetHealth(float health)//intial health bar values
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetPlayerHealth(float health)//setting the health when damged
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);// change colour based off value on a set graident


    }

}
