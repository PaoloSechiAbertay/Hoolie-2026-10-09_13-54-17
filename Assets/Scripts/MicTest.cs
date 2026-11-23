using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MicTest : MonoBehaviour
{
    MicrophoneDetection microphoneDetection;
    public Slider MicTestBar;
    public TextMeshProUGUI NoiseText;
    public TextMeshProUGUI SensitivityText;
    public TextMeshProUGUI DampeningText;
    bool testing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        microphoneDetection = GetComponent<MicrophoneDetection>();

        MicTestBar.gameObject.SetActive(false);
        testing = false;
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
        if (testing)
        {
            Test();
        }
    }

    void Inputs()
    {
        if (InputSystem.actions.FindAction("LShift").IsPressed() && InputSystem.actions.FindAction("RShift").WasPressedThisFrame())
        {
            MicTestBar.gameObject.SetActive(!MicTestBar.IsActive());
            testing = !testing; 
        }
    }

    void Test()
    {
        float noise = (microphoneDetection.GetLoudnessFromMicrophone() * MicSettingsManager.instance.loudnessSensibility * MicSettingsManager.instance.sensitivitySetting) - MicSettingsManager.instance.noiseDampeningSetting;
        MicTestBar.value = noise;
        NoiseText.text = noise.ToString();
        SensitivityText.text = MicSettingsManager.instance.sensitivitySetting.ToString();
        DampeningText.text = MicSettingsManager.instance.noiseDampeningSetting.ToString();
    }
}
