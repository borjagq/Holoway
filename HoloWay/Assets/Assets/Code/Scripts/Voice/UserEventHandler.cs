using Agora.Rtc;
using Agora_RTC_Plugin.API_Example.Examples.Basic.JoinChannelAudio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class UserEventHandler : IRtcEngineEventHandler
{
    private readonly VoiceHandler _audioSample;

    internal UserEventHandler(VoiceHandler audioSample)
    {
        _audioSample = audioSample;
    }

    public override void OnError(int err, string msg)
    {
        //_audioSample.Log.UpdateLog(string.Format("OnError err: {0}, msg: {1}", err, msg));
    }

    public override void OnJoinChannelSuccess(RtcConnection connection, int elapsed)
    {
        //int build = 0;
        //_audioSample.Log.UpdateLog(string.Format("sdk version: ${0}", _audioSample.RtcEngine.GetVersion(ref build)));
        //_audioSample.Log.UpdateLog(string.Format("OnJoinChannelSuccess channelName: {0}, uid: {1}, elapsed: {2}", connection.channelId, connection.localUid, elapsed));
    }

    public override void OnRejoinChannelSuccess(RtcConnection connection, int elapsed)
    {
        //_audioSample.Log.UpdateLog("OnRejoinChannelSuccess");
    }

    public override void OnLeaveChannel(RtcConnection connection, RtcStats stats)
    {
        //_audioSample.Log.UpdateLog("OnLeaveChannel");
    }

    public override void OnClientRoleChanged(RtcConnection connection, CLIENT_ROLE_TYPE oldRole, CLIENT_ROLE_TYPE newRole, ClientRoleOptions newRoleOptions)
    {
        //_audioSample.Log.UpdateLog("OnClientRoleChanged");
    }

    public override void OnUserJoined(RtcConnection connection, uint uid, int elapsed)
    {
        //_audioSample.Log.UpdateLog(string.Format("OnUserJoined uid: ${0} elapsed: ${1}", uid, elapsed));
    }

    public override void OnUserOffline(RtcConnection connection, uint uid, USER_OFFLINE_REASON_TYPE reason)
    {
        //_audioSample.Log.UpdateLog(string.Format("OnUserOffLine uid: ${0}, reason: ${1}", uid,  (int)reason));
    }
}
