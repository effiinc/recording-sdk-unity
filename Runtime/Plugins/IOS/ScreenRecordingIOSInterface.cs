using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenRecordingUnitySDK
{
    public  class ScreenRecordingIOSInterface
    {
        private static string TOKEN = "3HDb5Di9WowKc28mT5tJoOWvsFW0BoFtwIwE";

#if (UNITY_IOS || PLATFORM_IOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void _initializeRecorder(string token);
        [DllImport("__Internal")]
        private static extern void _logEvent(string eventType, string eventData);
        public static void InitializeRecorder()
        {
            _initializeRecorder(TOKEN);
        }
        
        public static void LogEvent(string eventType, string eventData)
        {
            _logEvent(eventType, eventData);
        }
#endif
    }
}