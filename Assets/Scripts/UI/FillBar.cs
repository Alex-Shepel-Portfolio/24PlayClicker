using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FillBar : ImprovedMonoBehaviour
{
    [SerializeField] private Image imageFill;
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private Transform content;
    // private void Start()
    // {
    //     Inactive();
    // }
    public void Active()
    {
        content.SetActive();
    }
    public void Inactive()
    {
        content.SetInactive();
    }
    
    public void FillStatus(float currentValue, float targetvalue)
    {
        var fillAmount = currentValue / targetvalue;
        FillStatus(fillAmount);
    }
    public void FillStatus(float proggres)
    {
        imageFill.fillAmount = Mathf.Clamp01(proggres);
    }

}
