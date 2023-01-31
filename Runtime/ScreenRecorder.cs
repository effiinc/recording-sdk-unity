using UnityEngine;

namespace ScreenRecordingUnitySDK
{
    public class ScreenRecorder : MonoBehaviour
    {
        [SerializeField] private bool isEnable = true;    
        private static string TOKEN = "3HDb5Di9WowKc28mT5tJoOWvsFW0BoFtwIwE";
        private static string USER_ID = "Unity_UserID: ad4a6f928d6b";
        
        public void Start()
        {
            InitAndStartRecording();
        }

        private void InitAndStartRecording()
        {
            var appVersion = Application.version.ToString();
            ScreenRecordingSDK.InitializeRecorder(TOKEN, appVersion, USER_ID, isEnable);
        }

        public void SetUserId(string id)
        {
            ScreenRecordingSDK.SetUserId(id);
        }

        public void LogEvent(string eventType, string eventData)
        {
            ScreenRecordingSDK.LogEvent(eventType, eventData);
        }

    }
}