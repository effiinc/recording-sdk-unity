using System.Text;
using System.Threading;
using UnityEditor;

#if UNITY_EDITOR
using UnityEditor.PackageManager;
#endif

using UnityEngine;

namespace ScreenRecordingUnitySDK
{
#if UNITY_EDITOR
    public static class ExternalPackagesTools
    {
        public static string GetPackagePath(string packageNAme)
        {
            var listRequest = Client.List(true);
            while (!listRequest.IsCompleted)
                Thread.Sleep(100);

            if (listRequest.Error != null)
            {
                Debug.Log("Error: " + listRequest.Error.message);
                return "";
            }
            var text = new StringBuilder("Packages:\n");
            var packages = listRequest.Result;
            foreach (var package in packages)
            {
                if (package.source == PackageSource.Git)
                {
                    text.AppendLine($"{package.name}: {package.version} [{package.resolvedPath}]");
                    if (package.name == packageNAme)
                    {
                        Debug.Log("Path " + package.resolvedPath);
                        return package.resolvedPath;
                    }
                }
            }

            return "";
        }
    }
#endif
}