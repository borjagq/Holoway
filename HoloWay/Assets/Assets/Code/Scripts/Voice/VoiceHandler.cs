using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agora.Rtc;
using Agora.Util;
using Logger = Agora.Util.Logger;
using System.Linq;
using System;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class VoiceHandler : MonoBehaviour
{

    [FormerlySerializedAs("appIdInput")]
    [SerializeField]
    private AppIdInput _appIdInput;

    [Header("_____________Basic Configuration_____________")]
    [FormerlySerializedAs("APP_ID")]
    [SerializeField]
    private string _appID = GlobalGameStrings.VOICE_APP_ID_STRING;

    [FormerlySerializedAs("TOKEN")]
    [SerializeField]
    private string _token = "";

    [FormerlySerializedAs("CHANNEL_NAME")]
    [SerializeField]
    private string _channelName = "";

    public Text LogText;
    internal Logger Log;
    internal IRtcEngine RtcEngine = null;

    private IAudioDeviceManager _audioDeviceManager;
    private DeviceInfo[] _audioPlaybackDeviceInfos;
    public Dropdown _audioDeviceSelect;


    public bool PublishingMode = false;
    // Start is called before the first frame update
    private void Start()
    {
        _appID = GlobalGameStrings.VOICE_APP_ID_STRING;
        Debug.Log($"VOICE: AppID => {_appID}");
        LoadAssetData();
        if (CheckAppId())
        {
            InitRtcEngine();
            SetBasicConfiguration();
        }
        //Auto join the channel once you spawn the player....
        JoinChannel();
        //However, stop the player from publishing the audio
        StopPublishAudio();
    }

    private void Update()
    {
        PermissionHelper.RequestMicrophontPermission();
        if(Input.GetKeyDown(KeyCode.M))
        {
            PublishingMode = !PublishingMode;
            if (PublishingMode)
                StartPublishAudio();
            else
                StopPublishAudio();
        }
    }

    private bool CheckAppId()
    {
        Log = new Logger(LogText);
        return Log.DebugAssert(_appID.Length > 10, "Please fill in your appId in API-Example/profile/appIdInput.asset!!!!!");
    }

    //Show data in AgoraBasicProfile
    [ContextMenu("ShowAgoraBasicProfileData")]
    private void LoadAssetData()
    {
        if (_appIdInput == null) return;
        _appID = _appIdInput.appID;
        _token = _appIdInput.token;
        _channelName = _appIdInput.channelName;
    }

    private void InitRtcEngine()
    {
        RtcEngine = Agora.Rtc.RtcEngine.CreateAgoraRtcEngine();
        UserEventHandler handler = new UserEventHandler(this);
        RtcEngineContext context = new RtcEngineContext(_appID, 0,
                                    CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_LIVE_BROADCASTING,
                                    AUDIO_SCENARIO_TYPE.AUDIO_SCENARIO_DEFAULT);
        RtcEngine.Initialize(context);
        RtcEngine.InitEventHandler(handler);
    }

    private void SetBasicConfiguration()
    {
        RtcEngine.EnableAudio();
        RtcEngine.SetChannelProfile(CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_COMMUNICATION);
        RtcEngine.SetClientRole(CLIENT_ROLE_TYPE.CLIENT_ROLE_BROADCASTER);
    }

    #region -- Button Events ---

    public void StartEchoTest()
    {
        RtcEngine.StartEchoTest(2);
        Debug.Log("StartEchoTest, speak now. You cannot conduct another echo test or join a channel before StopEchoTest");
    }

    public void StopEchoTest()
    {
        RtcEngine.StopEchoTest();
    }

    public void JoinChannel()
    {
        RtcEngine.JoinChannel(_token, _channelName);
    }

    public void LeaveChannel()
    {
        RtcEngine.LeaveChannel();
    }

    public void StopPublishAudio()
    {
        var options = new ChannelMediaOptions();
        options.publishMicrophoneTrack.SetValue(false);
        var nRet = RtcEngine.UpdateChannelMediaOptions(options);
        Debug.Log("UpdateChannelMediaOptions: " + nRet);
    }

    public void StartPublishAudio()
    {
        var options = new ChannelMediaOptions();
        options.publishMicrophoneTrack.SetValue(true);
        var nRet = RtcEngine.UpdateChannelMediaOptions(options);
        Debug.Log("UpdateChannelMediaOptions: " + nRet);
    }

    public void GetAudioPlaybackDevice()
    {
        _audioDeviceSelect.ClearOptions();
        _audioDeviceManager = RtcEngine.GetAudioDeviceManager();
        _audioPlaybackDeviceInfos = _audioDeviceManager.EnumeratePlaybackDevices();
        Debug.Log(string.Format("AudioPlaybackDevice count: {0}", _audioPlaybackDeviceInfos.Length));
        for (var i = 0; i < _audioPlaybackDeviceInfos.Length; i++)
        {
            Debug.Log(string.Format("AudioPlaybackDevice device index: {0}, name: {1}, id: {2}", i,
                _audioPlaybackDeviceInfos[i].deviceName, _audioPlaybackDeviceInfos[i].deviceId));
        }

        _audioDeviceSelect.AddOptions(_audioPlaybackDeviceInfos.Select(w =>
                new Dropdown.OptionData(
                    string.Format("{0} :{1}", w.deviceName, w.deviceId)))
            .ToList());
    }

    public void SelectAudioPlaybackDevice()
    {
        if (_audioDeviceSelect == null) return;
        var option = _audioDeviceSelect.options[_audioDeviceSelect.value].text;
        if (string.IsNullOrEmpty(option)) return;

        var deviceId = option.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1];
        var ret = _audioDeviceManager.SetPlaybackDevice(deviceId);
        Debug.Log("SelectAudioPlaybackDevice ret:" + ret + " , DeviceId: " + deviceId);
    }

    #endregion

    private void OnDestroy()
    {
        Debug.Log("OnDestroy");
        if (RtcEngine == null) return;
        RtcEngine.InitEventHandler(null);
        RtcEngine.LeaveChannel();
        RtcEngine.Dispose();
    }
}
