using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenRecordingUnitySDK
{
    public  class ScreenRecordingIOSInterface
    {
#if (UNITY_IOS || PLATFORM_IOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void _initializeRecorder(string token, string version, string userId, bool isEnable);
        [DllImport("__Internal")]
        private static extern void _logEvent(string eventType, string eventData);
        public static void InitializeRecorder(string token, string version, string userId, bool isEnable)
        {
            _initializeRecorder(token, version, userId, isEnable);
        }
        
        public static void LogEvent(string eventType, string eventData)
        {
            _logEvent(eventType, eventData);
        }
#endif
    }
}