using UnityEngine;
using UnityEngine.UI;

public class ProgressBarBehavior : MonoBehaviour
{
    Image barImage;

    private void Start()
    {
        barImage = GetComponent<Image>();
    }

    public void UpdateBar(float current, float end = 1.0f)
    {
        barImage.fillAmount = current / end;
    }

    public void UpdateBar(float current, float start, float end)
    {
        barImage.fillAmount = (current - start) / (end - start);
    }
}
