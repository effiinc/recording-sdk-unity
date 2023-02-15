using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if UNITY_IOS && UNITY_EDITOR
using UnityEditor.Callbacks;
using UnityEditor;
using UnityEditor.iOS.Xcode;
using UnityEngine;

#endif

namespace ScreenRecordingUnitySDK
{
#if UNITY_IOS && UNITY_EDITOR
    public class Postprocessor
    {
        private static readonly string PACKAGE_NAME = "recording-sdk-unity";

        [PostProcessBuildAttribute(1000)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            BuildTarget iOSBuildTarget;
            iOSBuildTarget = BuildTarget.iOS;
            if (target == iOSBuildTarget)
            {
                RunPodUpdate(pathToBuiltProject);

                string projectPath = pathToBuiltProject + "/Unity-iPhone.xcodeproj/project.pbxproj";


                PBXProject pbxProject = new PBXProject();
                pbxProject.ReadFromFile(projectPath);


                //Disabling Bitcode on all targets


                //Main
                string targetGuid = pbxProject.GetUnityMainTargetGuid();
                pbxProject.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "NO");


                //Unity Tests
                targetGuid = pbxProject.TargetGuidByName(PBXProject.GetUnityTestTargetName());
                pbxProject.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "NO");


                //Unity Framework
                targetGuid = pbxProject.GetUnityFrameworkTargetGuid();
                pbxProject.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "NO");


                pbxProject.WriteToFile(projectPath);
            }
        }

        static void RunPodUpdate(string path)
        {
            //  #if !UNITY_CLOUD_BUILD
            // Copy the podfile into the project.
            var packagePath = ExternalPackagesTools.GetPackagePath(PACKAGE_NAME);
            string podfile = Path.Combine(packagePath, "Pods/Podfile"); //Package path
            string podfileLock = Path.Combine(packagePath, "Pods/Podfile.lock"); //Package path
            string destPodFile = path + "/Podfile";
            string destPodLockfile = path + "/Podfile.lock";

            if (!File.Exists(podfile) || !File.Exists(podfile))
            {
                packagePath = Path.Combine(Application.dataPath, "ScreenRecordingSDK");
                podfile = Path.Combine(packagePath, "Pods/Podfile");
                //  podfileLock = Path.Combine(packagePath, "Pods/Podfile.lock");
            }

            if (File.Exists(destPodFile))
            {
                string[] existingPodFileContent = File.ReadAllLines(destPodFile);
                string[] pluginPodFileContent = File.ReadAllLines(podfile);
                string[] newPodFile = MergePodFiles(pluginPodFileContent, existingPodFileContent);
                if (newPodFile != null && newPodFile.Length > 0)
                {
                    File.WriteAllLines(destPodFile, newPodFile);
                }
            }
            else
            {
                if (!File.Exists(destPodFile))
                {
                    FileUtil.CopyFileOrDirectory(podfile, destPodFile);
                    //  FileUtil.CopyFileOrDirectory(podfileLock, destPodLockfile);
                }
            }

            try
            {
                CocoaPodHelper.Install(path);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError("Could not create a new Xcode project with CocoaPods: " +
                                           e.Message);
            }
        }

        private static string[] MergePodFiles(string[] pod1Lines, string[] pod2Lines)
        {
            List<string> mergedLines = new List<string>();

            int unityFrameWorkTargetLineNumber1 = 0;
            int unityFrameWorkTargetLineNumber2 = 0;

            unityFrameWorkTargetLineNumber1 = GetTargetLine(pod1Lines, "UnityFramework");
            unityFrameWorkTargetLineNumber2 = GetTargetLine(pod2Lines, "UnityFramework");

            for (int i = 0; i <= unityFrameWorkTargetLineNumber1; i++)
            {
                mergedLines.Add(pod1Lines[i]);
            }

            int index2 = unityFrameWorkTargetLineNumber2 + 1;
            while (!pod2Lines[index2].Contains("end"))
            {
                mergedLines.Add(pod2Lines[index2]);
                index2++;
            }

            int index1 = unityFrameWorkTargetLineNumber1 + 1;
            int localIdx = index1;
            while (pod1Lines[localIdx] != "end")
            {
                if (!IsTargetContainEntry(mergedLines.ToArray(), unityFrameWorkTargetLineNumber1,pod1Lines[localIdx])) 
                {
                    mergedLines.Add(pod1Lines[localIdx]);
                    index1++;
                }

                localIdx++;
            }

            mergedLines.Add("end");
            index1++;

            for (int i = index2 + 1; i < pod2Lines.Length; i++)
            {
                mergedLines.Add(pod2Lines[i]);
            }

            for (int i = index1 + 1; i < pod1Lines.Length; i++)
            {
                mergedLines.Add(pod1Lines[i]);
            }

            return mergedLines.ToArray();
        }

        private static int GetTargetLine(string[] allLines, string targetName)
        {
            for (int i = 0; i < allLines.Length; i++)
            {
                if (allLines[i].Contains("target '" + targetName + "' do"))
                {
                    return i;
                }
            }

            return allLines.Length - 1;
        }

        private static bool IsTargetContainEntry(string[] allLines, int index, string entryName)
        {
            for (int i = index; i < allLines.Length; i++)
            {
                if (allLines[i] == entryName)
                {
                    return true;
                }
            }
            
            return false;
        }
    }
#endif
}