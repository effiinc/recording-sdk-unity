using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

namespace ScreenRecordingUnitySDK
{
    public class ExternalPackagesResolver : Editor
    {
        private static string unityNativeActivityName = "com.unity3d.player.UnityPlayerActivity";
        private static string unityPluginActivityName = "com.effi.uactivity.PluginActivity";

        private static string PACKAGE_NAME = "recording-sdk-unity";
        private static string ANDROIDMANIFEST_NAME_FILE = "AndroidManifest.xml";
        private static string BASE_GRADLE_FILE = "baseProjectTemplate.gradle";
        private static string LAUNCHER_GRADLE_FILE = "launcherTemplate.gradle";

        [MenuItem("ScreenRecordingSDK/Android/ResolveAndroidManifest")]
        public static void ResolveAndroidManifest()
        {
            CheckAndFixManifest();
        }

        [MenuItem("ScreenRecordingSDK/Android/Rewrite BaseGradle")]
        public static void RewriteBaseGradle()
        {
            CloneAndroidFile(BASE_GRADLE_FILE);
        }

        [MenuItem("ScreenRecordingSDK/Android/Rewrite LauncherGradle")]
        public static void RewriteLauncherGradle()
        {
            CloneAndroidFile(LAUNCHER_GRADLE_FILE);
        }


        private static void CheckAndFixManifest()
        {
#if UNITY_ANDROID
            var outputFile = Path.Combine(Application.dataPath, "Plugins/Android", ANDROIDMANIFEST_NAME_FILE);
            if (!File.Exists(outputFile))
            {
                CloneAndroidFile(ANDROIDMANIFEST_NAME_FILE);
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(outputFile);

            if (doc == null)
            {
                Debug.LogError("Couldn't load " + outputFile);
                return;
            }

            XmlNode manNode = FindChildNode(doc, "manifest");
            string ns = manNode.GetNamespaceOfPrefix("android");

            XmlNode application_node = FindChildNode(manNode, "application");

            if (application_node == null)
            {
                Debug.LogError("Error parsing " + outputFile);
                return;
            }

            XmlElement main_activity =
                FindElementWithAndroidName("activity", "name", ns, unityNativeActivityName, application_node);
            if (main_activity == null)
            {
                return;
            }

            main_activity.Attributes.RemoveNamedItem("name", ns);
            XmlAttribute[] attributes = new XmlAttribute[main_activity.Attributes.Count];
            main_activity.Attributes.CopyTo(attributes, 0);
            main_activity.Attributes.RemoveAll();
            main_activity.SetAttribute("name", ns, unityPluginActivityName);
            for (int i = 0; i < attributes.Length; i++)
            {
                main_activity.SetAttribute(attributes[i].LocalName, ns, attributes[i].Value);
            }

            doc.Save(outputFile);
#endif
        }

        private static void CloneAndroidFile(string fileName, bool overwrite = false)
        {
            var packagePath = GetPackagePath();
            var targetFile = Path.Combine(packagePath, "Editor/Plugins/Android", fileName);
            var destination = Path.Combine(Application.dataPath, "Plugins/Android", fileName);
            File.Copy(targetFile, destination, overwrite);
        }


        private static string GetPackagePath()
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
                    if (package.name == PACKAGE_NAME)
                    {
                        Debug.Log("Path " + package.resolvedPath);
                        return package.resolvedPath;
                    }
                }
            }

            return "";
        }


        private static XmlNode FindChildNode(XmlNode parent, string name)
        {
            XmlNode curr = parent.FirstChild;
            while (curr != null)
            {
                if (curr.Name.Equals(name))
                {
                    return curr;
                }

                curr = curr.NextSibling;
            }

            return null;
        }

        private static XmlElement FindElementWithAndroidName(string name, string androidName, string ns, string value,
            XmlNode parent)
        {
            var curr = parent.FirstChild;
            while (curr != null)
            {
                if (curr.Name.Equals(name) && curr is XmlElement &&
                    ((XmlElement) curr).GetAttribute(androidName, ns) == value)
                {
                    return curr as XmlElement;
                }

                curr = curr.NextSibling;
            }

            return null;
        }
    }
}