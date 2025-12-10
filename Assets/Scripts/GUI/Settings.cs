using System;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject volumePanel;
    public GameObject sliderPanel;
    public Slider slider;

    private void Awake()
    {
        volumePanel = gameObject.transform.Find("Volume").gameObject;
        sliderPanel = volumePanel.transform.Find("Slider").gameObject;
        slider = sliderPanel.GetComponent<Slider>();
    }

    void Start()
    {
        slider.onValueChanged.AddListener(SetVolume);
    }

    private void SetVolume(float value)
    {
        AudioListener.volume = value;
    }

}
