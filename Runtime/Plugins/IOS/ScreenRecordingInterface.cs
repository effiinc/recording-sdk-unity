using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenRecordingUnitySDK.IOS
{
    public  class ScreenRecordingInterface
    {
        private static string TOKEN = "3HDb5Di9WowKc28mT5tJoOWvsFW0BoFtwIwE";

#if (UNITY_IOS || PLATFORM_IOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void _initializeRecorder(string token);
        public static void InitializeRecorder()
        {
            _initializeRecorder(TOKEN);
        }
#endif
        
    }
}