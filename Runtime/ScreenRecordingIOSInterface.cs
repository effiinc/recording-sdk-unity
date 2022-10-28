using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenRecordingUnitySDK
{
    public  class ScreenRecordingIOSInterface
    {
#if (UNITY_IOS || PLATFORM_IOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void _initializeRecorder(string token);
        [DllImport("__Internal")]
        private static extern void _logEvent(string eventType, string eventData);
        public static void InitializeRecorder(string token)
        {
            _initializeRecorder(token);
        }
        
        public static void LogEvent(string eventType, string eventData)
        {
            _logEvent(eventType, eventData);
        }
#endif
    }
}