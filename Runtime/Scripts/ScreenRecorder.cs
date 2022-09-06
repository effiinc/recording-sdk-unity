using ScreenRecordingUnitySDK;
using UnityEngine;

namespace ScreenRecordingUnitySDK
{
    public class ScreenRecorder : MonoBehaviour
    {
        public void Start()
        {
            InitAndStartRecording();
        }

        public void InitAndStartRecording()
        {
            ScreenRecordingSDK.InitializeRecorder();
        }
    }
}