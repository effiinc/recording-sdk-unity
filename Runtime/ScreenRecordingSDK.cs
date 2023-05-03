using UnityEngine;

namespace ScreenRecordingUnitySDK
{
    public static class ScreenRecordingSDK
    {
        public static void InitializeRecorder(string TOKEN, string appVersion, string userID, bool isEnable)
        {
            
#if (UNITY_IOS || PLATFORM_IOS) && !UNITY_EDITOR
             ScreenRecordingIOSInterface.InitializeRecorder(TOKEN, appVersion, userID, isEnable);
#endif
            
#if (UNITY_ANDROID || PLATFORM_ANDROID) && !UNITY_EDITOR 
            ScreenRecordingAndroidInterface.InitializeRecorder(TOKEN, appVersion, userID);
#endif
        }
        
        public static void SetUserId(string userId)
        {
#if (UNITY_IOS || PLATFORM_IOS) && !UNITY_EDITOR
            // ScreenRecordingIOSInterface.SetUserId(userId);
#endif
            
#if (UNITY_ANDROID || PLATFORM_ANDROID) && !UNITY_EDITOR
            ScreenRecordingAndroidInterface.SetUserId(userId);
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