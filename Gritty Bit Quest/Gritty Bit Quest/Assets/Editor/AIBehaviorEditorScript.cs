﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(AIBehavior))]

public class AIBehaviorEditorScript : Editor
{
    SerializedProperty m_Name;
    AIBehavior AIBehavior;
    List<string> SkipPropertyList = new List<string>();
    float lineHeight;
    float lineHeightSpace;

    void OnEnable()
    {
        if (target == null)
        {
            return;
        }

        SkipPropertyList.Add("x");
        SkipPropertyList.Add("y");
        SkipPropertyList.Add("z");
        SkipPropertyList.Add("StartTime");
        SkipPropertyList.Add("EndTime");
        SkipPropertyList.Add("data");

        m_Name = serializedObject.FindProperty("name");
        AIBehavior = (AIBehavior)target;


        for (int i = 0; i < AIBehavior.StatesList.Count; i++)
        {
            for (int j = 0; j < AIBehavior.StatesList[i].Actions.Count; j++)
            {
                AIBehavior.StatesList[i].Actions[j].ActionSettings.StartTime = 0;
                AIBehavior.StatesList[i].Actions[j].ActionSettings.EndTime = Mathf.Infinity;
            }
        }
    }

    void DrawList(SerializedProperty element)
    {

        GUILayout.BeginVertical("Box");

        IEnumerator childEnum = element.GetEnumerator();
        while (childEnum.MoveNext())
        {
            SerializedProperty current = childEnum.Current as SerializedProperty;
            if (current.name.ToLower() == "action")
                HandleSkipPropertiesListActions(current.enumValueIndex);
            if (current.name.ToLower() == "switchcondition")
                HandleSkipPropertiesListSwitches(current.enumValueIndex);
            if (current.name == "PossibleNextStates")
                SkipPropertyList.Remove("data");

            bool skip = false;
            for (int i = 0; i < SkipPropertyList.Count; i++)
            {
                if (SkipPropertyList[i] == current.name)
                {
                    skip = true;
                    break;
                }
            }
            if (!skip)
                HandleProperties(current);

        }
        GUILayout.EndVertical();
    }

    void HandleProperties(SerializedProperty property)
    {
        if (property.isArray && property.propertyType != SerializedPropertyType.String)
        {
            EditorGUILayout.TextField(property.name, EditorStyles.boldLabel);
        }
        //use reflection to get the variable
        if (property.propertyType == SerializedPropertyType.ArraySize)
        {
            property.intValue = EditorGUILayout.IntField("Size: ", property.intValue);
            return;
        }
        else if (property.propertyType == SerializedPropertyType.String)
        {
            property.stringValue = EditorGUILayout.TextField(property.name, property.stringValue);
            return;
        }
        else if (property.propertyType == SerializedPropertyType.Integer)
        {
            property.intValue = EditorGUILayout.IntField(property.name, property.intValue);
            return;
        }
        else if (property.propertyType == SerializedPropertyType.Boolean)
        {
            property.boolValue = EditorGUILayout.Toggle(property.name, property.boolValue);
            return;
        }
        else if (property.propertyType == SerializedPropertyType.Vector3)
        {
            property.vector3Value = EditorGUILayout.Vector3Field(property.name, property.vector3Value);
            return;
        }
        else if (property.propertyType == SerializedPropertyType.Vector2)
        {
            property.vector2Value = EditorGUILayout.Vector2Field(property.name, property.vector2Value);
            return;
        }
        else if (property.propertyType == SerializedPropertyType.Float)
        {
            property.floatValue = EditorGUILayout.FloatField(property.name, property.floatValue);
            return;
        }
<<<<<<< HEAD
        else if (property.name == "ActionSettings")
=======

        else if (property.propertyType == SerializedPropertyType.Enum)
>>>>>>> parent of d0d11103... Gritty Bit Vehicle
        {
            EditorGUILayout.PropertyField(property, true);
        }
        else if (property.propertyType != SerializedPropertyType.Generic)
        {
            EditorGUILayout.PropertyField(property, true);
            return;
        }
    }

    void HandleSkipPropertiesListActions(int enumValue) //Do Once
    {
        if (enumValue == 0) // Spawn
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationClip");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");

        }
<<<<<<< HEAD
        else if (enumValue == 1) //Idle
=======
    }

    public override void OnInspectorGUI()
    {
        //base.DrawDefaultInspector();
        AIBehavior.firstAction = (AIAction.ActionState)EditorGUILayout.EnumPopup("First Action", AIBehavior.firstAction);
        for (int i = 0; i < AIBehavior.ActionsList.Count; i++)
>>>>>>> parent of d0d11103... Gritty Bit Vehicle
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationClip");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
        }
        else if (enumValue == 2) //RunForward
        {
<<<<<<< HEAD
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationClip");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
=======
            AIAction tempAction = new AIAction();
            tempAction.Switches.Add(new Switch());
            AIBehavior.ActionsList.Add(tempAction);

>>>>>>> parent of d0d11103... Gritty Bit Vehicle
        }
        else if (enumValue == 3)//Walk Forward
        {
<<<<<<< HEAD
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationClip");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
=======
            AIBehavior.ActionsList.Remove(AIBehavior.ActionsList[AIBehavior.ActionsList.Count - 1]);
>>>>>>> parent of d0d11103... Gritty Bit Vehicle
        }
        else if (enumValue == 4)//WalkLeft
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationClip");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
        }
        else if (enumValue == 5)//WalkRight
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationClip");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
        }
        else if (enumValue == 6)// Roar
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationClip");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, %YAML 1.1
%TAG !u! tag:unity3d.com,2011:
--- !u!129 &1
PlayerSettings:
  m_ObjectHideFlags: 0
  serializedVersion: 18
  productGUID: 1c77ebe6c55874d4097a1d7c297be25d
  AndroidProfiler: 0
  AndroidFilterTouchesWhenObscured: 0
  AndroidEnableSustainedPerformanceMode: 0
  defaultScreenOrientation: 4
  targetDevice: 2
  useOnDemandResources: 0
  accelerometerFrequency: 60
  companyName: DefaultCompany
  productName: libcache
  defaultCursor: {fileID: 0}
  cursorHotspot: {x: 0, y: 0}
  m_SplashScreenBackgroundColor: {r: 0.13725491, g: 0.12156863, b: 0.1254902, a: 1}
  m_ShowUnitySplashScreen: 1
  m_ShowUnitySplashLogo: 1
  m_SplashScreenOverlayOpacity: 1
  m_SplashScreenAnimation: 1
  m_SplashScreenLogoStyle: 1
  m_SplashScreenDrawMode: 0
  m_SplashScreenBackgroundAnimationZoom: 1
  m_SplashScreenLogoAnimationZoom: 1
  m_SplashScreenBackgroundLandscapeAspect: 1
  m_SplashScreenBackgroundPortraitAspect: 1
  m_SplashScreenBackgroundLandscapeUvs:
    serializedVersion: 2
    x: 0
    y: 0
    width: 1
    height: 1
  m_SplashScreenBackgroundPortraitUvs:
    serializedVersion: 2
    x: 0
    y: 0
    width: 1
    height: 1
  m_SplashScreenLogos: []
  m_VirtualRealitySplashScreen: {fileID: 0}
  m_HolographicTrackingLossScreen: {fileID: 0}
  defaultScreenWidth: 1024
  defaultScreenHeight: 768
  defaultScreenWidthWeb: 960
  defaultScreenHeightWeb: 600
  m_StereoRenderingPath: 0
  m_ActiveColorSpace: 0
  m_MTRendering: 1
  m_StackTraceTypes: 010000000100000001000000010000000100000001000000
  iosShowActivityIndicatorOnLoading: -1
  androidShowActivityIndicatorOnLoading: -1
  displayResolutionDialog: 0
  iosUseCustomAppBackgroundBehavior: 0
  iosAllowHTTPDownload: 1
  allowedAutorotateToPortrait: 1
  allowedAutorotateToPortraitUpsideDown: 1
  allowedAutorotateToLandscapeRight: 1
  allowedAutorotateToLandscapeLeft: 1
  useOSAutorotation: 1
  use32BitDisplayBuffer: 1
  preserveFramebufferAlpha: 0
  disableDepthAndStencilBuffers: 0
  androidStartInFullscreen: 1
  androidRenderOutsideSafeArea: 1
  androidUseSwappy: 0
  androidBlitType: 0
  defaultIsNativeResolution: 1
  macRetinaSupport: 1
  runInBackground: 1
  captureSingleScreen: 0
  muteOtherAudioSources: 0
  Prepare IOS For Recording: 0
  Force IOS Speakers When Recording: 0
  deferSystemGesturesMode: 0
  hideHomeButton: 0
  submitAnalytics: 1
  usePlayerLog: 1
  bakeCollisionMeshes: 0
  forceSingleInstance: 0
  useFlipModelSwapchain: 1
  resizableWindow: 0
  useMacAppStoreValidation: 0
  macAppStoreCategory: public.app-category.games
  gpuSkinning: 1
  graphicsJobs: 0
  xboxPIXTextureCapture: 0
  xboxEnableAvatar: 0
  xboxEnableKinect: 0
  xboxEnableKinectAutoTracking: 0
  xboxEnableFitness: 0
  visibleInBackground: 1
  allowFullscreenSwitch: 1
  graphicsJobMode: 0
  fullscreenMode: 1
  xboxSpeechDB: 0
  xboxEnableHeadOrientation: 0
  xboxEnableGuest: 0
  xboxEnablePIXSampling: 0
  metalFramebufferOnly: 0
  xboxOneResolution: 0
  xboxOneSResolution: 0
  xboxOneXResolution: 3
  xboxOneMonoLoggingLevel: 0
  xboxOneLoggingLevel: 1
  xboxOneDisableEsram: 0
  xboxOnePresentImmediateThreshold: 0
  switchQueueCommandMemory: 0
  switchQueueControlMemory: 16384
  switchQueueComputeMemory: 262144
  switchNVNShaderPoolsGranularity: 33554432
  switchNVNDefaultPoolsGranularity: 16777216
  switchNVNOtherPoolsGranularity: 16777216
  vulkanEnableSetSRGBWrite: 0
  m_SupportedAspectRatios:
    4:3: 1
    5:4: 1
    16:10: 1
    16:9: 1
    Others: 1
  bundleVersion: 0.1
  preloadedAssets: []
  metroInputSource: 0
  wsaTransparentSwapchain: 0
  m_HolographicPauseOnTrackingLoss: 1
  xboxOneDisableKinectGpuReservation: 1
  xboxOneEnable7thCore: 1
  vrSettings:
    cardboard:
      depthFormat: 0
      enableTransitionView: 0
    daydream:
      depthFormat: 0
      useSustainedPerformanceMode: 0
      enableVideoLayer: 0
      useProtectedVideoMemory: 0
      minimumSupportedHeadTracking: 0
      maximumSupportedHeadTracking: 1
    hololens:
      depthFormat: 1
      depthBufferSharingEnabled: 1
    lumin:
      depthFormat: 0
      frameTiming: 2
      enableGLCache: 0
      glCacheMaxBlobSize: 524288
      glCacheMaxFileSize: 8388608
    oculus:
      sharedDepthBuffer: 1
      dashSupport: 1
      lowOverheadMode: 0
    enable360StereoCapture: 0
  isWsaHolographicRemotingEnabled: 0
  protectGraphicsMemory: 0
  enableFrameTimingStats: 0
  useHDRDisplay: 0
  m_ColorGamuts: 00000000
  targetPixelDensity: 30
  resolutionScalingMode: 0
  androidSupportedAspectRatio: 1
  androidMaxAspectRatio: 2.1
  applicationIdentifier: {}
  buildNumber: {}
  AndroidBundleVersionCode: 1
  AndroidMinSdkVersion: 16
  AndroidTargetSdkVersion: 0
  AndroidPreferredInstallLocation: 1
  aotOptions: 
  stripEngineCode: 1
  iPhoneStrippingLevel: 0
  iPhoneScriptCallOptimization: 0
  ForceInternetPermission: 0
  ForceSDCardPermission: 0
  CreateWallpaper: 0
  APKExpansionFiles: 0
  keepLoadedShadersAlive: 0
  StripUnusedMeshComponents: 1
  VertexChannelCompressionMask: 4054
  iPhoneSdkVersion: 988
  iOSTargetOSVersionString: 9.0
  tvOSSdkVersion: 0
  tvOSRequireExtendedGameController: 0
  tvOSTargetOSVersionString: 9.0
  uIPrerenderedIcon: 0
  uIRequiresPersistentWiFi: 0
  uIRequiresFullScreen: 1
  uIStatusBarHidden: 1
  uIExitOnSuspend: 0
  uIStatusBarStyle: 0
  iPhoneSplashScreen: {fileID: 0}
  iPhoneHighResSplashScreen: {fileID: 0}
  iPhoneTallHighResSplashScreen: {fileID: 0}
  iPhone47inSplashScreen: {fileID: 0}
  iPhone55inPortraitSplashScreen: {fileID: 0}
  iPhone55inLandscapeSplashScreen: {fileID: 0}
  iPhone58inPortraitSplashScreen: {fileID: 0}
  iPhone58inLandscapeSplashScreen: {fileID: 0}
  iPadPortraitSplashScreen: {fileID: 0}
  iPadHighResPortraitSplashScreen: {fileID: 0}
  iPadLandscapeSplashScreen: {fileID: 0}
  iPadHighResLandscapeSplashScreen: {fileID: 0}
  iPhone65inPortraitSplashScreen: {fileID: 0}
  iPhone65inLandscapeSplashScreen: {fileID: 0}
  iPhone61inPortraitSplashScreen: {fileID: 0}
  iPhone61inLandscapeSplashScreen: {fileID: 0}
  appleTVSplashScreen: {fileID: 0}
  appleTVSplashScreen2x: {fileID: 0}
  tvOSSmallIconLayers: []
  tvOSSmallIconLayers2x: []
  tvOSLargeIconLayers: []
  tvOSLargeIconLayers2x: []
  tvOSTopShelfImageLayers: []
  tvOSTopShelfImageLayers2x: []
  tvOSTopShelfImageWideLayers: []
  tvOSTopShelfImageWideLayers2x: []
  iOSLaunchScreenType: 0
  iOSLaunchScreenPortrait: {fileID: 0}
  iOSLaunchScreenLandscape: {fileID: 0}
  iOSLaunchScreenBackgroundColor:
    serializedVersion: 2
    rgba: 0
  iOSLaunchScreenFillPct: 100
  iOSLaunchScreenSize: 100
  iOSLaunchScreenCustomXibPath: 
  iOSLaunchScreeniPadType: 0
  iOSLaunchScreeniPadImage: {fileID: 0}
  iOSLaunchScreeniPadBackgroundColor:
    serializedVersion: 2
    rgba: 0
  iOSLaunchScreeniPadFillPct: 100
  iOSLaunchScreeniPadSize: 100
  iOSLaunchScreeniPadCustomXibPath: 
  iOSUseLaunchScreenStoryboard: 0
  iOSLaunchScreenCustomStoryboardPath: 
  iOSDeviceRequirements: []
  iOSURLSchemes: []
  iOSBackgroundModes: 0
  iOSMetalForceHardShadows: 0
  metalEditorSupport: 1
  metalAPIValidation: 1
  iOSRenderExtraFrameOnPause: 0
  appleDeveloperTeamID: 
  iOSManualSigningProvisioningProfileID: 
  tvOSManualSigningProvisioningProfileID: 
  iOSManualSigningProvisioningProfileType: 0
  tvOSManualSigningProvisioningProfileType: 0
  appleEnableAutomaticSigning: 0
  iOSRequireARKit: 0
  iOSAutomaticallyDetectAndAddCapabilities: 1
  appleEnableProMotion: 0
  clonedFromGUID: c0afd0d1d80e3634a9dac47e8a0426ea
  templatePackageId: com.unity.template.3d@3.1.2
  templateDefaultScene: Assets/Scenes/SampleScene.unity
  AndroidTargetArchitectures: 1
  AndroidSplashScreenScale: 0
  androidSplashScreen: {fileID: 0}
  AndroidKeystoreName: '{inproject}: '
  AndroidKeyaliasName: 
  AndroidBuildApkPerCpuArchitecture: 0
  AndroidTVCompatibility: 0
  AndroidIsGame: 1
  AndroidEnableTango: 0
  androidEnableBanner: 1
  androidUseLowAccuracyLocation: 0
  androidUseCustomKeystore: 0
  m_AndroidBanners:
  - width: 320
    height: 180
    banner: {fileID: 0}
  androidGamepadSupportLevel: 0
  AndroidValidateAppBundleSize: 1
  AndroidAppBundleSizeToValidate: 150
  resolutionDialogBanner: {fileID: 0}
  m_BuildTargetIcons: []
  m_BuildTargetPlatformIcons: []
  m_BuildTargetBatching:
  - m_BuildTarget: Standalone
    m_StaticBatching: 1
    m_DynamicBatching: 0
  - m_BuildTarget: tvOS
    m_StaticBatching: 1
    m_DynamicBatching: 0
  - m_BuildTarget: Android
    m_StaticBatching: 1
    m_DynamicBatching: 0
  - m_BuildTarget: iPhone
    m_StaticBatching: 1
    m_DynamicBatching: 0
  - m_BuildTarget: WebGL
    m_StaticBatching: 0
    m_DynamicBatching: 0
  m_BuildTargetGraphicsAPIs:
  - m_BuildTarget: AndroidPlayer
    m_APIs: 150000000b000000
    m_Automatic: 0
  - m_BuildTarget: iOSSupport
    m_APIs: 10000000
    m_Automatic: 1
  - m_BuildTarget: AppleTVSupport
    m_APIs: 10000000
    m_Automatic: 0
  - m_BuildTarget: WebGLSupport
    m_APIs: 0b000000
    m_Automatic: 1
  m_BuildTargetVRSettings:
  - m_BuildTarget: Standalone
    m_Enabled: 0
    m_Devices:
    - Oculus
    - OpenVR
  openGLRequireES31: 0
  openGLRequireES31AEP: 0
  openGLRequireES32: 0
  vuforiaEnabled: 0
  m_TemplateCustomTags: {}
  mobileMTRendering:
    Android: 1
    iPhone: 1
    tvOS: 1
  m_BuildTargetGroupLightmapEncodingQuality: []
  m_BuildTargetGroupLightmapSettings: []
  playModeTestRunnerEnabled: 0
  runPlayModeTestAsEditModeTest: 0
  actionOnDotNetUnhandledException: 1
  enableInternalProfiler: 0
  logObjCUncaughtExceptions: 1
  enableCrashReportAPI: 0
  cameraUsageDescription: 
  locationUsageDescription: 
  microphoneUsageDescription: 
  switchNetLibKey: 
  switchSocketMemoryPoolSize: 6144
  switchSocketAllocatorPoolSize: 128
  switchSocketConcurrencyLimit: 14
  switchScreenResolutionBehavior: 2
  switchUseCPUProfiler: 0
  switchApplicationID: 0x01004b9000490000
  switchNSODependencies: 
  switchTitleNames_0: 
  switchTitleNames_1: 
  switchTitleNames_2: 
  switchTitleNames_3: 
  switchTitleNames_4: 
  switchTitleNames_5: 
  switchTitleNames_6: 
  switchTitleNames_7: 
  switchTitleNames_8: 
  switchTitleNames_9: 
  switchTitleNames_10: 
  switchTitleNames_11: 
  switchTitleNames_12: 
  switchTitleNames_13: 
  switchTitleNames_14: 
  switchPublisherNames_0: 
  switchPublisherNames_1: 
  switchPublisherNames_2: 
  switchPublisherNames_3: 
  switchPublisherNames_4: 
  switchPublisherNames_5: 
  switchPublisherNames_6: 
  switchPublisherNames_7: 
  switchPublisherNames_8: 
  switchPublisherNames_9: 
  switchPublisherNames_10: 
  switchPublisherNames_11: 
  switchPublisherNames_12: 
  switchPublisherNames_13: 
  switchPublisherNames_14: 
  switchIcons_0: {fileID: 0}
  switchIcons_1: {fileID: 0}
  switchIcons_2: {fileID: 0}
  switchIcons_3: {fileID: 0}
  switchIcons_4: {fileID: 0}
  switchIcons_5: {fileID: 0}
  switchIcons_6: {fileID: 0}
  switchIcons_7: {fileID: 0}
  switchIcons_8: {fileID: 0}
  switchIcons_9: {fileID: 0}
  switchIcons_10: {fileID: 0}
  switchIcons_11: {fileID: 0}
  switchIcons_12: {fileID: 0}
  switchIcons_13: {fileID: 0}
  switchIcons_14: {fileID: 0}
  switchSmallIcons_0: {fileID: 0}
  switchSmallIcons_1: {fileID: 0}
  switchSmallIcons_2: {fileID: 0}
  switchSmallIcons_3: {fileID: 0}
  switchSmallIcons_4: {fileID: 0}
  switchSmallIcons_5: {fileID: 0}
  switchSmallIcons_6: {fileID: 0}
  switchSmallIcons_7: {fileID: 0}
  switchSmallIcons_8: {fileID: 0}
  switchSmallIcons_9: {fileID: 0}
  switchSmallIcons_10: {fileID: 0}
  switchSmallIcons_11: {fileID: 0}
  switchSmallIcons_12: {fileID: 0}
  switchSmallIcons_13: {fileID: 0}
  switchSmallIcons_14: {fileID: 0}
  switchManualHTML: 
  switchAccessibleURLs: 
  switchLegalInformation: 
  switchMainThreadStackSize: 1048576
  switchPresenceGroupId: 
  switchLogoHandling: 0
  switchReleaseVersion: 0
  switchDisplayVersion: 1.0.0
  switchStartupUserAccount: 0
  switchTouchScreenUsage: 0
  switchSupportedLanguagesMask: 0
  switchLogoType: 0
  switchApplicationErrorCodeCategory: 
  switchUserAccountSaveDataSize: 0
  switchUserAccountSaveDataJournalSize: 0
  switchApplicationAttribute: 0
  switchCardSpecSize: -1
  switchCardSpecClock: -1
  switchRatingsMask: 0
  switchRatingsInt_0: 0
  switchRatingsInt_1: 0
  switchRatingsInt_2: 0
  switchRatingsInt_3: 0
  switchRatingsInt_4: 0
  switchRatingsInt_5: 0
  switchRatingsInt_6: 0
  switchRatingsInt_7: 0
  switchRatingsInt_8: 0
  switchRatingsInt_9: 0
  switchRatingsInt_10: 0
  switchRatingsInt_11: 0
  switchLocalCommunicationIds_0: 
  switchLocalCommunicationIds_1: 
  switchLocalCommunicationIds_2: 
  switchLocalCommunicationIds_3: 
  switchLocalCommunicationIds_4: 
  switchLocalCommunicationIds_5: 
  switchLocalCommunicationIds_6: 
  switchLocalCommunicationIds_7: 
  switchParentalControl: 0
  switchAllowsScreenshot: 1
  switchAllowsVideoCapturing: 1
  switchAllowsRuntimeAddOnContentInstall: 0
  switchDataLossConfirmation: 0
  switchUserAccountLockEnabled: 0
  switchSystemResourceMemory: 16777216
  switchSupportedNpadStyles: 22
  switchNativeFsCacheSize: 32
  switchIsHoldTypeHorizontal: 0
  switchSupportedNpadCount: 8
  switchSocketConfigEnabled: 0
  switchTcpInitialSendBufferSize: 32
  switchTcpInitialReceiveBufferSize: 64
  switchTcpAutoSendBufferSizeMax: 256
  switchTcpAutoReceiveBufferSizeMax: 256
  switchUdpSendBufferSize: 9
  switchUdpReceiveBufferSize: 42
  switchSocketBufferEfficiency: 4
  switchSocketInitializeEnabled: 1
  switchNetworkInterfaceManagerInitializeEnabled: 1
  switchPlayerConnectionEnabled: 1
  ps4NPAgeRating: 12
  ps4NPTitleSecret: 
  ps4NPTrophyPackPath: 
  ps4ParentalLevel: 11
  ps4ContentID: ED1633-NPXX51362_00-0000000000000000
  ps4Category: 0
  ps4MasterVersion: 01.00
  ps4AppVersion: 01.00
  ps4AppType: 0
  ps4ParamSfxPath: 
  ps4VideoOutPixelFormat: 0
  ps4VideoOutInitialWidth: 1920
  ps4VideoOutBaseModeInitialWidth: 1920
  ps4VideoOutReprojectionRate: 60
  ps4PronunciationXMLPath: 
  ps4PronunciationSIGPath: 
  ps4BackgroundImagePath: 
  ps4StartupImagePath: 
  ps4StartupImagesFolder: 
  ps4IconImagesFolder: 
  ps4SaveDataImagePath: 
  ps4SdkOverride: 
  ps4BGMPath: 
  ps4ShareFilePath: 
  ps4ShareOverlayImagePath: 
  ps4PrivacyGuardImagePath: 
  ps4NPtitleDatPath: 
  ps4RemotePlayKeyAssignment: -1
  ps4RemotePlayKeyMappingDir: 
  ps4PlayTogetherPlayerCount: 0
  ps4EnterButtonAssignment: 1
  ps4ApplicationParam1: 0
  ps4ApplicationParam2: 0
  ps4ApplicationParam3: 0
  ps4ApplicationParam4: 0
  ps4DownloadDataSize: 0
  ps4GarlicHeapSize: 2048
  ps4ProGarlicHeapSize: 2560
  playerPrefsMaxSize: 32768
  ps4Passcode: frAQBc8Wsa1xVPfvJcrgRYwTiizs2trQ
  ps4pnSessions: 1
  ps4pnPresence: 1
  ps4pnFriends: 1
  ps4pnGameCustomData: 1
  playerPrefsSupport: 0
  enableApplicationExit: 0
  resetTempFolder: 1
  restrictedAudioUsageRights: 0
  ps4UseResolutionFallback: 0
  ps4ReprojectionSupport: 0
  ps4UseAudio3dBackend: 0
  ps4SocialScreenEnabled: 0
  ps4ScriptOptimizationLevel: 0
  ps4Audio3dVirtualSpeakerCount: 14
  ps4attribCpuUsage: 0
  ps4PatchPkgPath: 
  ps4PatchLatestPkgPath: 
  ps4PatchChangeinfoPath: 
  ps4PatchDayOne: 0
  ps4attribUserManagement: 0
  ps4attribMoveSupport: 0
  ps4attrib3DSupport: 0
  ps4attribShareSupport: 0
  ps4attribExclusiveVR: 0
  ps4disableAutoHideSplash: 0
  ps4videoRecordingFeaturesUsed: 0
  ps4contentSearchFeaturesUsed: 0
  ps4attribEyeToEyeDistanceSettingVR: 0
  ps4IncludedModules: []
  monoEnv: 
  splashScreenBackgroundSourceLandscape: {fileID: 0}
  splashScreenBackgroundSourcePortrait: {fileID: 0}
  blurSplashScreenBackground: 1
  spritePackerPolicy: 
  webGLMemorySize: 16
  webGLExceptionSupport: 1
  webGLNameFilesAsHashes: 0
  webGLDataCaching: 1
  webGLDebugSymbols: 0
  webGLEmscriptenArgs: 
  webGLModulesDirectory: 
  webGLTemplate: APPLICATION:Default
  webGLAnalyzeBuildSize: 0
  webGLUseEmbeddedResources: 0
  webGLCompressionFormat: 1
  webGLLinkerTarget: 1
  webGLThreadsSupport: 0
  webGLWasmStreaming: 0
  scriptingDefineSymbols: {}
  platformArchitecture: {}
  scriptingBackend: {}
  il2cppCompilerConfiguration: {}
  managedStrippingLevel: {}
  incrementalIl2cppBuild: {}
  allowUnsafeCode: 0
  additionalIl2CppArgs: 
  scriptingRuntimeVersion: 1
  gcIncremental: 0
  gcWBarrierValidation: 0
  apiCompatibilityLevelPerPlatform: {}
  m_RenderingPath: 1
  m_MobileRenderingPath: 1
  metroPackageName: Template_3D
  metroPackageVersion: 
  metroCertificatePath: 
  metroCertificatePassword: 
  metroCertificateSubject: 
  metroCertificateIssuer: 
  metroCertificateNotAfter: 0000000000000000
  metroApplicationDescription: Template_3D
  wsaImages: {}
  metroTileShortName: 
  metroTileShowName: 0
  metroMediumTileShowName: 0
  metroLargeTileShowName: 0
  metroWideTileShowName: 0
  metroSupportStreamingInstall: 0
  metroLastRequiredScene: 0
  metroDefaultTileSize: 1
  metroTileForegroundText: 2
  metroTileBackgroundColor: {r: 0.1333