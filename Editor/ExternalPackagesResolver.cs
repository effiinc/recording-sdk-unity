using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
using UnityEditor;
#if UNITY_ANDROID && UNITY_EDITOR
using UnityEditor.PackageManager;
#endif
using UnityEngine;

namespace ScreenRecordingUnitySDK
{
#if UNITY_ANDROID && UNITY_EDITOR
    public class ExternalPackagesResolver : EditorWindow
    {

        private static string unityNativeActivityName = "com.unity3d.player.UnityPlayerActivity";
        private static string unityPluginActivityName = "com.effi.uactivity.PluginActivity";

        private static string PACKAGE_NAME = "recording-sdk-unity";
        private static string ANDROIDMANIFEST_NAME_FILE = "AndroidManifest.xml";
        private static string BASE_GRADLE_FILE = "baseProjectTemplate.gradle";
        private static string LAUNCHER_GRADLE_FILE = "launcherTemplate.gradle";
        private static string _internalMessage;
        
        [MenuItem("Effi/ScreenRecordingSDK/ResolveAndroidManifest")]
        public static void ResolveAndroidManifest()
        {
            CheckAndFixManifest();
        }

        private static void CheckAndFixManifest()
        {
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
                ShowMessage("The Android Manifest Resolver could not complete the process successfully. See the console for details!");
                return;
            }

            XmlNode manNode = FindChildNode(doc, "manifest");
            string ns = manNode.GetNamespaceOfPrefix("android");

            XmlNode application_node = FindChildNode(manNode, "application");

            if (application_node == null)
            {
                ShowMessage("The Android Manifest Resolver could not complete the process successfully. See the console for details!");
                Debug.LogError("Error parsing " + outputFile);
                return;
            }

            XmlElement main_activity =
                FindElementWithAndroidName("activity", "name", ns, unityNativeActivityName, application_node);
            if (main_activity == null)
            {
                ShowMessage("The Android Manifest is already fixed!");
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
            if (File.Exists(outputFile))
            {
                ShowMessage("Android manifest resolved successfully!");
            }
            else
            {
                ShowMessage("The Android Manifest Resolver could not complete the process successfully. See the console for details!");
            }
        }

        private static void CloneAndroidFile(string fileName, bool overwrite = false)
        {
            var packagePath = ExternalPackagesTools.GetPackagePath(PACKAGE_NAME);
            if (string.IsNullOrEmpty(packagePath))
            {
                packagePath = Path.Combine(Application.dataPath, "ScreenRecordingSDK");
            }
            var targetFile = Path.Combine(packagePath, "Editor/Plugins/Android", fileName);
            var destination = Path.Combine(Application.dataPath, "Plugins/Android", fileName);
            File.Copy(targetFile, destination, overwrite);
            if (File.Exists(destination))
            {
                ShowMessage("Android manifest resolved successfully!");
            }
            else
            {
                ShowMessage("The Android Manifest Resolver could not complete the process successfully. See the console for details!");
            }
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
        
        private static void ShowMessage(string message)
        {
            _internalMessage = message;
            ExternalPackagesResolver window = ScriptableObject.CreateInstance<ExternalPackagesResolver>();
            window.position = new Rect(100, 100, 250, 150);
            window.ShowPopup();
        }
        
        private void OnGUI()
        {
            GUILayout.Space(20);
            EditorGUILayout.LabelField(_internalMessage, EditorStyles.wordWrappedLabel);
            GUILayout.Space(50);
            if (GUILayout.Button("Ok")) this.Close();
        }

    }
#endif
}