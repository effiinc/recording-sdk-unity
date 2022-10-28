using UnityEngine;

namespace ScreenRecordingUnitySDK
{
    public static class ScreenRecordingSDK
    {
        private static string TOKEN = "3HDb5Di9WowKc28mT5tJoOWvsFW0BoFtwIwE";

        public static void InitializeRecorder()
        {
            var appVersion = Application.version.ToString();
#if (UNITY_IOS || PLATFORM_IOS) && !UNITY_EDITOR
             ScreenRecordingIOSInterface.InitializeRecorder(TOKEN);
#endif
            
#if (UNITY_ANDROID || PLATFORM_ANDROID) && !UNITY_EDITOR 
            ScreenRecordingAndroidInterface.InitializeRecorder(TOKEN, appVersion);
#endif
        }
        
        public static void LogEvent(string eventType, string eventData)
        {
#if (UNITY_IOS || PLATFORM_IOS) && !UNITY_EDITOR
             ScreenRecordingIOSInterface.LogEvent(eventType, eventData);
#endif
            
#if (UNITY_ANDROID || PLATFORM_ANDROID) && !UNITY_EDITOR
            ScreenRecordingAndroidInterface.LogEvent(eventType, eventData);
#endif
        }
    }
}