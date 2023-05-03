using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

namespace ScreenRecordingUnitySDK
{
    public static class ScreenRecordingAndroidInterface
    {
        private static string ACTIVITY_CLASS_NAME = "com.effi.uactivity.PluginActivity";
        private static AndroidJavaObject _pluginInstance;

#if (UNITY_ANDROID || PLATFORM_ANDROID) && !UNITY_EDITOR
        public static void InitializeRecorder(string token, string appVersion, string userId)
        {
            _pluginInstance = new AndroidJavaObject(ACTIVITY_CLASS_NAME);
            if (_pluginInstance != null)
            {
                _pluginInstance.Call("InitAndStartRecording", token, appVersion, userId);
            }
        }

        public static void SetUserId(string userID)
        {
            if (_pluginInstance != null)
            {
                _pluginInstance.Call("SetUserId", userID);
            } 
        }
        
        public static void LogEvent(string type, string data)
        {
            if (_pluginInstance != null)
            {
                _pluginInstance.Call("SendEvent", type, data);
            } 
        }

        private static void InitializePlugin(string pluginName)
        {
            _pluginInstance = new AndroidJavaObject(pluginName);
            if (_pluginInstance == null)
            {
                Debug.LogError("Plugin Instance ERROR");
            }
        }


#endif
    }
}