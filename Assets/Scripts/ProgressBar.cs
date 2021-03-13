using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private Image progressBarImage;

    void Awake()
    {
        progressBarImage.fillAmount = 0;
    }

    public void progressChanged(float pct)
    {
        var clampedPct = Mathf.Clamp01(pct);
        progressBarImage.fillAmount = pct;
    }
}
