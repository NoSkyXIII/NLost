using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReductionSlider : MonoBehaviour
{
    [SerializeField] public Image fillImage;
    [SerializeField] private TMP_Text _text;

    public void Set(float timeLeft, float FullTime)
    {
        fillImage.fillAmount = timeLeft / FullTime;

        if (_text != null)
        {
            int time = Mathf.RoundToInt(FullTime - timeLeft);
            _text.text = time.ToString();
        }
    }
}
