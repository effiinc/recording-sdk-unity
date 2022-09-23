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

    void _initializeRecorder(const char* token) {
        NSString *nsToken = MakeNSString(token);
        _screenRecordingManager = [[ScreenRecordingManager alloc] initWithToken:nsToken appInfo:@"UnityProject v0.4.2"];
    }

   void _logEvent(const char* eventType, const char* eventData){
       NSString *nsEventType = MakeNSString(eventType);
       NSString *nsEventData = MakeNSString(eventData);
       [ScreenRecordingEventLogger logEventWithType:nsEventType event:nsEventData];
   }
}
