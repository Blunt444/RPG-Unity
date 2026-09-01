using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public TMP_Text value;
    public Slider slider;
    public SliderType sliderType;

    private void OnEnable()
    {
        slider.onValueChanged.AddListener(GetAndSetSliderValue);
        slider.value = sliderType == SliderType.Music ? AudioManager.Instance.musicVolume : AudioManager.Instance.sfxVolume;
        GetAndSetSliderValue(sliderType == SliderType.Music ? AudioManager.Instance.musicVolume : AudioManager.Instance.sfxVolume);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(GetAndSetSliderValue);
    }

    public void GetAndSetSliderValue(float value)
    {
        this.value.text = $"{Mathf.RoundToInt(value * 100)}%";
        switch (sliderType)
        {
            case SliderType.Music:
                AudioManager.Instance.SetMusicVolume(value);
                return;
            case SliderType.SFX:
                AudioManager.Instance.SetSFXVolume(value);
                return;
            default:
                return;
        }
    }
}

public enum SliderType
{
    Music,
    SFX
}
