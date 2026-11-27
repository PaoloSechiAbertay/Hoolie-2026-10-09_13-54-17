using UnityEngine;
using UnityEngine.InputSystem;

public class MicSettingsManager : MonoBehaviour
{
    public float loudnessSensibility = 100f;
    public float sensitivitySetting = 1;
    public float noiseDampeningSetting = 0;
    InputActionMap numbers;

    public static MicSettingsManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        numbers = InputSystem.actions.FindActionMap("Numbers");
    }

    private void Update()
    {
        updateSettings();
    }

    void updateSettings()
    {
        if (numbers.FindAction("LShift").IsPressed())
        {
            if (numbers.FindAction("One").WasPressedThisFrame()) { MicSettingsManager.instance.sensitivitySetting = 1; }
            else if (numbers.FindAction("Two").WasPressedThisFrame()) { MicSettingsManager.instance.sensitivitySetting = 2; }
            else if (numbers.FindAction("Three").WasPressedThisFrame()) { MicSettingsManager.instance.sensitivitySetting = 3; }
            else if (numbers.FindAction("Four").WasPressedThisFrame()) { MicSettingsManager.instance.sensitivitySetting = 4; }
            else if (numbers.FindAction("Five").WasPressedThisFrame()) { MicSettingsManager.instance.sensitivitySetting = 5; }
            else if (numbers.FindAction("Six").WasPressedThisFrame()) { MicSettingsManager.instance.sensitivitySetting = 6; }
            else if (numbers.FindAction("Seven").WasPressedThisFrame()) { MicSettingsManager.instance.sensitivitySetting = 7; }
            else if (numbers.FindAction("Eight").WasPressedThisFrame()) { MicSettingsManager.instance.sensitivitySetting = 8; }
            else if (numbers.FindAction("Nine").WasPressedThisFrame()) { MicSettingsManager.instance.sensitivitySetting = 9; }
            else if (numbers.FindAction("Zero").WasPressedThisFrame()) { MicSettingsManager.instance.sensitivitySetting = 10; }

            else if (numbers.FindAction("1").WasPressedThisFrame()) { MicSettingsManager.instance.sensitivitySetting = 0.1f; }
            else if (numbers.FindAction("2").WasPressedThisFrame()) { MicSettingsManager.instance.sensitivitySetting = 0.2f; }
            else if (numbers.FindAction("3").WasPressedThisFrame()) { MicSettingsManager.instance.sensitivitySetting = 0.3f; }
            else if (numbers.FindAction("4").WasPressedThisFrame()) { MicSettingsManager.instance.sensitivitySetting = 0.4f; }
            else if (numbers.FindAction("5").WasPressedThisFrame()) { MicSettingsManager.instance.sensitivitySetting = 0.5f; }
            else if (numbers.FindAction("6").WasPressedThisFrame()) { MicSettingsManager.instance.sensitivitySetting = 0.6f; }
            else if (numbers.FindAction("7").WasPressedThisFrame()) { MicSettingsManager.instance.sensitivitySetting = 0.7f; }
            else if (numbers.FindAction("8").WasPressedThisFrame()) { MicSettingsManager.instance.sensitivitySetting = 0.8f; }
            else if (numbers.FindAction("9").WasPressedThisFrame()) { MicSettingsManager.instance.sensitivitySetting = 0.9f; }
        }

        else if (numbers.FindAction("RShift").IsPressed())
        {
            if (numbers.FindAction("0").WasPressedThisFrame()) { MicSettingsManager.instance.noiseDampeningSetting = 0; }
            else if (numbers.FindAction("1").WasPressedThisFrame()) { MicSettingsManager.instance.noiseDampeningSetting = 1; }
            else if (numbers.FindAction("2").WasPressedThisFrame()) { MicSettingsManager.instance.noiseDampeningSetting = 2; }
            else if (numbers.FindAction("3").WasPressedThisFrame()) { MicSettingsManager.instance.noiseDampeningSetting = 3; }
            else if (numbers.FindAction("4").WasPressedThisFrame()) { MicSettingsManager.instance.noiseDampeningSetting = 4; }
            else if (numbers.FindAction("5").WasPressedThisFrame()) { MicSettingsManager.instance.noiseDampeningSetting = 5; }
            else if (numbers.FindAction("6").WasPressedThisFrame()) { MicSettingsManager.instance.noiseDampeningSetting = 6; }
            else if (numbers.FindAction("7").WasPressedThisFrame()) { MicSettingsManager.instance.noiseDampeningSetting = 7; }
            else if (numbers.FindAction("8").WasPressedThisFrame()) { MicSettingsManager.instance.noiseDampeningSetting = 8; }
            else if (numbers.FindAction("9").WasPressedThisFrame()) { MicSettingsManager.instance.noiseDampeningSetting = 9; }
            else if (numbers.FindAction("Zero").WasPressedThisFrame()) { MicSettingsManager.instance.noiseDampeningSetting = 0; }

            else if (numbers.FindAction("One").WasPressedThisFrame()) { MicSettingsManager.instance.noiseDampeningSetting = 1; }
            else if (numbers.FindAction("Two").WasPressedThisFrame()) { MicSettingsManager.instance.noiseDampeningSetting = 2; }
            else if (numbers.FindAction("Three").WasPressedThisFrame()) { MicSettingsManager.instance.noiseDampeningSetting = 3; }
            else if (numbers.FindAction("Four").WasPressedThisFrame()) { MicSettingsManager.instance.noiseDampeningSetting = 4; }
            else if (numbers.FindAction("Five").WasPressedThisFrame()) { MicSettingsManager.instance.noiseDampeningSetting = 5; }
            else if (numbers.FindAction("Six").WasPressedThisFrame()) { MicSettingsManager.instance.noiseDampeningSetting = 6; }
            else if (numbers.FindAction("Seven").WasPressedThisFrame()) { MicSettingsManager.instance.noiseDampeningSetting = 7; }
            else if (numbers.FindAction("Eight").WasPressedThisFrame()) { MicSettingsManager.instance.noiseDampeningSetting = 8; }
            else if (numbers.FindAction("Nine").WasPressedThisFrame()) { MicSettingsManager.instance.noiseDampeningSetting = 9; }
        }
    }
}
