
# Unity Screen recording SDK




## Installation

#### Install SDK with Unity Package Manager

- Click the add `+` button in the status bar.
- Select `Add package from git URL` from the add menu. A text box and an `Add` button appear.
- Enter `https://github.com/effiinc/recording-sdk-unity.git` and click `Add` 

#### IOS Platform settings
- Go to `Editor -> ProjectSettings -> OtherSettings`  and change `Target minimum iOS version to 13.0`

#### Android Platform settings
- Go to `Editor -> ProjectSettings -> OtherSettings`  Set Android minimum `API Level 22+`
- Go to `Editor -> ProjectSettings -> Publishing Settings` and set the true value to the following properties if they are not enabled:
  - Custom Main Manifest
  - Custom Main Gradle Template
  - Custom Gradle Properties Template 

- #### Resolve Android dependencies:

  **EMD(External Dependency Manager)** is used to add android dependencies, see more information about the plugin [Here](https://github.com/googlesamples/unity-jar-resolver).
  After importing the package, go to the `Assets->External Dependency Manager->AndroidResolver->Settings` and set the next values to the properties:

        Use Gradle Daemon = true
        Enable Resolution On Build = true
        Install Android Packages = false
        Explode AARs = false
        Patch AndroidManifest.xml = false
        Patch mainTemplate.gradle = true
        Use Jetifier = true
        Patch gradleTemplate.properties = true 
- #### Resolve AndroidManifest
  - Go to the menu `Effi->ScreenRecordingSDK` and select `ResolveAndroidManifest`
  - Go to Assets->External Dependency Manager->AndroidResolver and select Resolve



    

## Usage

Add `ScreenRecorder.prefab` from `Packages/ScreenRecordingSDK/Runtime/Prefabs` to the current scene

### Initialization
Set valid TOKEN to ScreenRecordingSDK.cs
```c#
private static string TOKEN = "token";
```
The `ScreenRecorder.cs` class initializes the plugin in the `Awake` method, if you need to initialize it in another place, put this `ScreenRecordingSDK.InitializeRecorder();`  in the place you need.

### Event logging
To log an event, insert the following line into your code:
```c#
ScreenRecordingSDK.LogEvent(eventType, eventData);
```
The `ScreenRecorder.cs` class initializes the plugin in the `Awake` method, if you need to initialize it in another place, put this `ScreenRecordingSDK.InitializeRecorder();`  in the place you need.


## SDK Update

- Open `Unity Package Manager`
- Select `ScreenRecordingSDK` and click the `Update` button
- Go to `Assets->External Dependency Manager->AndroidResolver` and select `Resolve`
- Go to the menu `Effi->ScreenRecordingSDK` and select `ResolveAndroidManifest`
## Authors

- [@Effi]()

