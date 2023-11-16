using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public Image ForegroundImage;
    public Image BackgroundImage;

    public void SetHealthBarPercent(float percentage)
    {
        float parentWidth = GetComponent<RectTransform>().rect.width;
        float Width = parentWidth * percentage;
        ForegroundImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Width);
    }
}
