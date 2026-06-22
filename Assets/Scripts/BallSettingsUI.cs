using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class BallSettingsUI : MonoBehaviour
{
    [Header("Ссылки на Ball")]
    public BallController ballController;

    [Header("Слайдеры")]
    public Slider bounceMultiplierSlider;
    public Slider playerKickBoostSlider;
    public Slider velocityConservationSlider;
    public Slider gravityMultiplierSlider;
    public Slider ballScaleSlider;

    [Header("Текстовые поля (опционально)")]
    public TextMeshProUGUI bounceValueText;
    public TextMeshProUGUI kickValueText;
    public TextMeshProUGUI conservationValueText;
    public TextMeshProUGUI gravityValueText;
    public TextMeshProUGUI scaleValueText;

    private void Start()
    {
        if (ballController == null)
        {
            Debug.LogError("BallController не назначен в BallSettingsUI!");
            return;
        }

        // Инициализируем слайдеры текущими значениями из скрипта
        InitializeSliders();

        // Подписываемся на изменения слайдеров
        bounceMultiplierSlider.onValueChanged.AddListener(UpdateBounce);
        playerKickBoostSlider.onValueChanged.AddListener(UpdateKickBoost);
        velocityConservationSlider.onValueChanged.AddListener(UpdateConservation);
        gravityMultiplierSlider.onValueChanged.AddListener(UpdateGravity);
        ballScaleSlider.onValueChanged.AddListener(UpdateScale);
    }

    private void InitializeSliders()
    {
        bounceMultiplierSlider.value = ballController.bounceMultiplier;
        playerKickBoostSlider.value = ballController.playerKickBoost;
        velocityConservationSlider.value = ballController.velocityConservation;
        gravityMultiplierSlider.value = ballController.gravityMultiplier;
        ballScaleSlider.value = ballController.ballScale;

        UpdateAllTexts();
    }

    // Методы обновления параметров
    private void UpdateBounce(float value)
    {
        ballController.bounceMultiplier = value;
        if (bounceValueText) bounceValueText.text = value.ToString("F2");
    }

    private void UpdateKickBoost(float value)
    {
        ballController.playerKickBoost = value;
        if (kickValueText) kickValueText.text = value.ToString("F1");
    }

    private void UpdateConservation(float value)
    {
        ballController.velocityConservation = value;
        if (conservationValueText) conservationValueText.text = value.ToString("F2");
    }

    private void UpdateGravity(float value)
    {
        ballController.gravityMultiplier = value;
        if (gravityValueText) gravityValueText.text = value.ToString("F2");
    }

    private void UpdateScale(float value)
    {
        ballController.ballScale = value;
        ballController.transform.localScale = Vector3.one * value; // сразу применяем размер
        if (scaleValueText) scaleValueText.text = value.ToString("F2");
    }

    private void UpdateAllTexts()
    {
        if (bounceValueText) bounceValueText.text = ballController.bounceMultiplier.ToString("F2");
        if (kickValueText) kickValueText.text = ballController.playerKickBoost.ToString("F1");
        if (conservationValueText) conservationValueText.text = ballController.velocityConservation.ToString("F2");
        if (gravityValueText) gravityValueText.text = ballController.gravityMultiplier.ToString("F2");
        if (scaleValueText) scaleValueText.text = ballController.ballScale.ToString("F2");
    }

    // Опционально: кнопка "Сбросить к умолчанию"
    public void ResetToDefaults()
    {
        bounceMultiplierSlider.value = 1.1f;
        playerKickBoostSlider.value = 8f;
        velocityConservationSlider.value = 0.92f;
        gravityMultiplierSlider.value = 1f;
        ballScaleSlider.value = 1f;
    }
}