using UnityEngine;

namespace ScreenRecordingUnitySDK
{
    public static class ScreenRecordingSDK
    {
        public static void InitializeRecorder()
        {
#if (UNITY_IOS || PLATFORM_IOS) && !UNITY_EDITOR
             ScreenRecordingIOSInterface.InitializeRecorder();
#endif
            
#if (UNITY_ANDROID || PLATFORM_ANDROID) && !UNITY_EDITOR
            ScreenRecordingUnitySDK.ScreenRecordingAndroidInterface.InitializeRecorder();
#else
            Debug.LogError("Editor does not supported yet!");
#endif
        }
        
        public static void LogEvent(string eventType, string eventData)
        {
#if (UNITY_IOS || PLATFORM_IOS) && !UNITY_EDITOR
             ScreenRecordingIOSInterface.LogEvent(eventType, eventData);
#endif
            
#if (UNITY_ANDROID || PLATFORM_ANDROID) && !UNITY_EDITOR
            //ScreenRecordingUnitySDK.ScreenRecordingAndroidInterface.LogEvent(eventType, eventData);
#else
            Debug.LogError("Editor does not supported yet!");
#endif
        }
    }
}