using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

namespace ScreenRecordingUnitySDK
{
    public static class ScreenRecordingAndroidInterface
    {
        private static AndroidJavaClass unityClass;
        private static AndroidJavaObject unityActivity;
        private static AndroidJavaObject _pluginInstance;

#if (UNITY_ANDROID || PLATFORM_ANDROID) && !UNITY_EDITOR
        public static void InitializeRecorder()
        {
            InitializePlugin("com.effi.uactivity.PluginActivity");
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