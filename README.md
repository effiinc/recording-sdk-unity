
# Unity Screen recording SDK v0.6.2




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

  **Gradle** 
    - Open the `gradleTemplate.properties`, and paste the following code at the end of the file:
    ```
    android.useAndroidX=true
    android.enableJetifier=true
    ```
    - Open the `mainTemplate.gradle` file, and insert the following code at the beginning:
    
    ```
    ([rootProject] + (rootProject.subprojects as List)).each { project ->
    project.repositories {
            def unityProjectPath = $/file:///**DIR_UNITYPROJECT**/$.replace("\\", "/")
            maven {
             url "https://maven.google.com"
            }
            maven {
            url "https://effi.jfrog.io/artifactory/effi-screen-recorder"
            }
            mavenLocal()
           mavenCentral()
        }
    }
    ```
    - Insert the following code into the dependencies section:
    ```
    implementation 'com.effi.io:screen-recorder:0.2.11'
    ```
    - Insert the following code under the dependencies section:
    ```
    android {
        packagingOptions {
            exclude ('/lib/arm64-v8a/*' + '*')
            exclude ('/lib/armeabi/*' + '*')
            exclude ('/lib/mips/*' + '*')
            exclude ('/lib/mips64/*' + '*')
            exclude ('/lib/x86/*' + '*')
            exclude ('/lib/x86_64/*' + '*')
        }
    }
    ```


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
- Go to the menu `Effi->ScreenRecordingSDK` and select `ResolveAndroidManifest`
## Authors

- [@Effi]()

