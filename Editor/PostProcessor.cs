using System;
using System.IO;
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
        
        [PostProcessBuildAttribute(1)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            BuildTarget iOSBuildTarget;
            iOSBuildTarget = BuildTarget.iOS;
            if (target == iOSBuildTarget)
            {
                RunPodUpdate(pathToBuiltProject);
                
                string projectPath =  pathToBuiltProject + "/Unity-iPhone.xcodeproj/project.pbxproj";


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

            //Change for SDK Unity project
            if (!File.Exists(podfile) || !File.Exists(podfile))
            {
                packagePath = Path.Combine(Application.dataPath, "ScreenRecordingSDK");
                podfile = Path.Combine(packagePath, "Pods/Podfile");
                podfileLock = Path.Combine(packagePath, "Pods/Podfile.lock");
                
            }

            if (!System.IO.File.Exists(destPodFile))
            {
                FileUtil.CopyFileOrDirectory(podfile, destPodFile);
                FileUtil.CopyFileOrDirectory(podfileLock, destPodLockfile);
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
    }
#endif
}