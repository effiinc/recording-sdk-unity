#import <Foundation/Foundation.h>
#import <ScreenRecordingSDK/ScreenRecordingSDK-Swift.h>

extern "C" {

ScreenRecordingManager* _screenRecordingManager;

#pragma mark - Functions
    
// Helper method to create NSString copy from C String
NSString* MakeNSString (const char* string) {
    if (string) {
        return [NSString stringWithUTF8String: string];
    } else {
        return [NSString stringWithUTF8String: ""];
    }
}

void _initializeRecorder(const char* token, const char* appInfo, const char* userId) {
    NSString *nsToken = MakeNSString(token);
    NSString *nsAppInfo = MakeNSString(appInfo);
    NSString *nsUserId = MakeNSString(userId);
    _screenRecordingManager = [[ScreenRecordingManager alloc] initWithToken:nsToken appInfo:nsAppInfo userId:nsUserId enableRecording: YES];
}

   void _logEvent(const char* eventType, const char* eventData){
       NSString *nsEventType = MakeNSString(eventType);
       NSString *nsEventData = MakeNSString(eventData);
       [ScreenRecordingEventLogger logEventWithType:nsEventType event:nsEventData];
   }
}
