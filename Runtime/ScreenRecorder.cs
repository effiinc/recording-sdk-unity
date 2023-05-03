using UnityEngine;

namespace ScreenRecordingUnitySDK
{
    public class ScreenRecorder : MonoBehaviour
    {
        [SerializeField] private bool enableRecorder = true;
        [SerializeReference] private string TOKEN;
        [SerializeReference] private string USER_ID;
        
        public void Start()
        {
            InitAndStartRecording();
        }

        private void InitAndStartRecording()
        {
            var appVersion = Application.version.ToString();
            ScreenRecordingSDK.InitializeRecorder(TOKEN, appVersion, USER_ID, enableRecorder);
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