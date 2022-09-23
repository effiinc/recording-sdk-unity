using ScreenRecordingUnitySDK;
using UnityEngine;

namespace ScreenRecordingUnitySDK
{
    public class ScreenRecorder : MonoBehaviour
    {
        public void Awake()
        {
            InitAndStartRecording();
        }

        private void InitAndStartRecording()
        {
            ScreenRecordingSDK.InitializeRecorder();
        }

        public void LogEvent(string eventType, string eventData)
        {
            ScreenRecordingSDK.LogEvent(eventType, eventData);
        }

    }
}