using UnityEngine;

namespace ScreenRecordingUnitySDK
{
    public static class ScreenRecordingSDK
    {
        public static void InitializeRecorder()
        {
#if (UNITY_IOS || PLATFORM_IOS) && !UNITY_EDITOR
            IOS.ScreenRecordingInterface.InitializeRecorder();
#else
        Debug.LogError("Only the IOS platform is supported!");
#endif
        }
    }
}