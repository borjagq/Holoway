using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SettingsMenuTests
{
    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene(3);
    }

    [UnityTest]
    public IEnumerator Test_UICanvas()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_SettingsMenu()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/SettingsMenu");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_MenuBackground()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/SettingsMenu/SettingsMenuBackground");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_MenuItemSettingsMain()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/SettingsMenu/MenuItem_SettingsMain");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_ButtonControls()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/SettingsMenu/MenuItem_SettingsMain/Button_Controls");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_ButtonGraphics()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/SettingsMenu/MenuItem_SettingsMain/Button_Graphics");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_ButtonDisplay()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/SettingsMenu/MenuItem_SettingsMain/Button_Display");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_ButtonAudio()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/SettingsMenu/MenuItem_SettingsMain/Button_Audio");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_ButtonNetwork()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/SettingsMenu/MenuItem_SettingsMain/Button_Network");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_ButtonBack()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/SettingsMenu/MenuItem_SettingsMain/Button_Back");
        Assert.NotNull(Object);
    }


    [UnityTest]
    public IEnumerator Test_MainCamera()
    {
        yield return null;
        GameObject Object = GameObject.Find("Main Camera");
        Assert.NotNull(Object);
    }


    [UnityTest]
    public IEnumerator Test_EventSystem()
    {
        yield return null;
        GameObject Object = GameObject.Find("EventSystem");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_ChangeSceneMainMenu()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/SettingsMenu/MenuItem_SettingsMain/Button_Back");
        Button button = Object.GetComponent<Button>();
        button.onClick.Invoke();
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(2, SceneManager.GetActiveScene().buildIndex);
    }

    [UnityTest]
    public IEnumerator Test_ShowNetworkMenu()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/SettingsMenu/MenuItem_SettingsMain/Button_Network");
        Button button = Object.GetComponent<Button>();
        button.onClick.Invoke();
        
        GameObject NetworkMenu = GameObject.Find("UICanvas/SettingsMenu/MenuItem_Network");

        Assert.IsTrue(NetworkMenu.activeInHierarchy);
    }

    [UnityTest]
    public IEnumerator Test_SaveNetworkValues()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/SettingsMenu/MenuItem_SettingsMain/Button_Network");
        Button button = Object.GetComponent<Button>();
        button.onClick.Invoke();

        GameObject NetworkMenu = GameObject.Find("UICanvas/SettingsMenu/MenuItem_Network");

        yield return new WaitForSeconds(1f);

        
        GameObject IPAddress = GameObject.Find("UICanvas/SettingsMenu/MenuItem_Network/MenuItem_Network_IPAddress/InputField_IPAddress");
        GameObject Port = GameObject.Find("UICanvas/SettingsMenu/MenuItem_Network/MenuItem_Network_Port/InputField_Port");

        TMP_InputField IPInputField = IPAddress.GetComponent<TMP_InputField>();
        IPInputField.text = "127.0.0";


        TMP_InputField PortInputField = Port.GetComponent<TMP_InputField>();
        PortInputField.text = "7777";

        GameObject NetworkSave = GameObject.Find("UICanvas/SettingsMenu/MenuItem_Network/Button_NetworkSettings_Save");
        Button SaveButton = NetworkSave.GetComponent<Button>();
        SaveButton.onClick.Invoke();

        Assert.AreEqual(PortInputField.text, GlobalGameSettings.Instance.NetworkSettings.GetPort().ToString());
        Assert.AreEqual(IPInputField.text, GlobalGameSettings.Instance.NetworkSettings.GetIPAddress());
    }

    [UnityTest]
    public IEnumerator Test_SaveVolumeValues()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/SettingsMenu/MenuItem_SettingsMain/Button_Audio");
        Button button = Object.GetComponent<Button>();
        button.onClick.Invoke();

        GameObject NetworkMenu = GameObject.Find("UICanvas/SettingsMenu/MenuItem_Volume");

        yield return new WaitForSeconds(1f);

        GameObject VolumeSlider = GameObject.Find("UICanvas/SettingsMenu/MenuItem_Volume/MenuItem_Volume_VolumeSlider/Slider_Volume");
        GameObject MicVolumeSlider = GameObject.Find("UICanvas/SettingsMenu/MenuItem_Volume/MenuItem_Volume_MicVolumeSlider/Slider_MicrophoneVolume");
        GameObject UIVolumeSlider = GameObject.Find("UICanvas/SettingsMenu/MenuItem_Volume/MenuItem_Volume_UIVolumeSlider/Slider_UIVolume");
        GameObject Checkbox = GameObject.Find("UICanvas/SettingsMenu/MenuItem_Volume/MenuItem_Volume_Checkbox/Toggle_Sound");


        Slider VolumeSliderValue = VolumeSlider.GetComponent<Slider>();
        VolumeSliderValue.value = 0.5f;

        Slider MicVolumeSliderValue = MicVolumeSlider.GetComponent<Slider>();
        MicVolumeSliderValue.value = 0.5f;

        Slider UIVolumeSliderValue = UIVolumeSlider.GetComponent<Slider>();
        UIVolumeSliderValue.value = 0.5f;

        Toggle CheckboxToggle = Checkbox.GetComponent<Toggle>();
        CheckboxToggle.isOn = false;

        GameObject NetworkSave = GameObject.Find("UICanvas/SettingsMenu/MenuItem_Volume/Button_NetworkSettings_Save");
        Button SaveButton = NetworkSave.GetComponent<Button>();
        SaveButton.onClick.Invoke();


        Assert.AreEqual(VolumeSliderValue.value, GlobalGameSettings.Instance.AudioSettings.GetAudioVolume());
        Assert.AreEqual(MicVolumeSliderValue.value, GlobalGameSettings.Instance.AudioSettings.GetMicrophoneVolume());
        Assert.AreEqual(UIVolumeSliderValue.value, GlobalGameSettings.Instance.AudioSettings.GetUIVolume());
    }

    [UnityTest]
    public IEnumerator Test_ShowVolumeMenu()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/SettingsMenu/MenuItem_SettingsMain/Button_Audio");
        Button button = Object.GetComponent<Button>();
        button.onClick.Invoke();

        GameObject AudioMenu = GameObject.Find("UICanvas/SettingsMenu/MenuItem_Volume");

        Assert.IsTrue(AudioMenu.activeInHierarchy);
    }

    [UnityTest]
    public IEnumerator Test_HideNetworkMenu()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/SettingsMenu/MenuItem_SettingsMain/Button_Network");
        Button button = Object.GetComponent<Button>();
        button.onClick.Invoke();

        yield return new WaitForSeconds(1f);

        GameObject NetworkBackButton = GameObject.Find("UICanvas/SettingsMenu/MenuItem_Network/Button_GoBack");
        Button NetworkButton = NetworkBackButton.GetComponent<Button>();
        NetworkButton.onClick.Invoke();

        yield return new WaitForSeconds(1f);

        GameObject MainSettingsMenu = GameObject.Find("UICanvas/SettingsMenu/MenuItem_SettingsMain");

        Assert.IsTrue(MainSettingsMenu.activeInHierarchy);
    }

    [UnityTest]
    public IEnumerator Test_HideAudioMenu()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/SettingsMenu/MenuItem_SettingsMain/Button_Audio");
        Button button = Object.GetComponent<Button>();
        button.onClick.Invoke();

        yield return new WaitForSeconds(1f);

        GameObject AudioMenuBackButton = GameObject.Find("UICanvas/SettingsMenu/MenuItem_Volume/Button_GoBack");
        Button AudioButton = AudioMenuBackButton.GetComponent<Button>();
        AudioButton.onClick.Invoke();

        yield return new WaitForSeconds(1f);

        GameObject MainSettingsMenu = GameObject.Find("UICanvas/SettingsMenu/MenuItem_SettingsMain");

        Debug.Log(MainSettingsMenu.activeInHierarchy);

        Assert.IsTrue(MainSettingsMenu.activeInHierarchy);
    }
}
