using DevKit.Entities.API;
using DevKit.Entities.Demo.Characters.Players.API;
using DevKit.Entities.Extensions;
using DevKit.Serialization.Json.API;

namespace DevKit.Entities.Demo.Characters.Players
{
    [JsonDataContract]
    public class PlayerDeviceInfo : Entity<IPlayerDeviceInfo>, IPlayerDeviceInfo
    {
        [JsonDataMember(Name = "duid")]
        public string DeviceUniqueId { get; set; }

        [JsonDataMember(Name = "deviceName")]
        public string DeviceName { get; set; }

        [JsonDataMember(Name = "deviceModel")]
        public string DeviceModel { get; set; }

        [JsonDataMember(Name = "gfxDeviceName")]
        public string GraphicsDeviceName { get; set; }

        [JsonDataMember(Name = "locale")]
        public string Locale { get; set; }

        [JsonDataMember(Name = "os")]
        public string OperatingSystem { get; set; }

        [JsonDataMember(Name = "gfxMultiThreaded")]
        public bool IsGraphicsMultiThreaded { get; set; }

        [JsonDataMember(Name = "supportsAudio")]
        public bool IsAudioSupported { get; set; }

        [JsonDataMember(Name = "supports3DTextures")]
        public bool Supports3DTextures { get; set; }

        [JsonDataMember(Name = "supports2DArrayTextures")]
        public bool Supports2DArrayTextures { get; set; }

        [JsonDataMember(Name = "supports2DArrayTxt")]
        public bool IsAccelerometerSupported { get; set; }

        [JsonDataMember(Name = "gfxDeviceVID")]
        public int GraphicsDeviceVendorID { get; set; }

        [JsonDataMember(Name = "gfxDeviceId")]
        public int GraphicsDeviceID { get; set; }

        [JsonDataMember(Name = "systemMemorySize")]
        public int SystemMemorySize { get; set; }

        [JsonDataMember(Name = "numberMultiTxtSupported")]
        public int SupportsMultisampledTextures { get; set; }

        [JsonDataMember(Name = "maxCubemapSize")]
        public int MaxCubemapSize { get; set; }

        [JsonDataMember(Name = "gfxShaderLvl")]
        public int GraphicsShaderLevel { get; set; }

        [JsonDataMember(Name = "deviceType")]
        public string DeviceType { get; set; }

        [JsonDataMember(Name = "maxAnisotropyLvl")]
        public int MaxAnisotropyLevel { get; set; }

        [JsonDataMember(Name = "cpuFreq")]
        public int ProcessorFrequency { get; set; }

        [JsonDataMember(Name = "cpuCount")]
        public int ProcessorCount { get; set; }

        [JsonDataMember(Name = "gfxMemorySize")]
        public int GraphicsMemorySize { get; set; }

        [JsonDataMember(Name = "batteryLvl")]
        public float BatteryLevel { get; set; }

        [JsonDataMember(Name = "cpuType")]
        public string ProcessorType { get; set; }

        [JsonDataMember(Name = "screenResolution")]
        public string CurrentResolution { get; set; }

        [JsonDataMember(Name = "screenOrientation")]
        public string ScreenOrientation { get; set; }

        [JsonDataMember(Name = "screenHeight")]
        public int ScreenHeight { get; set; }

        [JsonDataMember(Name = "screenWidth")]
        public int ScreenWidth { get; set; }

        [JsonDataMember(Name = "screenDpi")]
        public float ScreenDpi { get; set; }

        [JsonDataMember(Name = "fullScreen")]
        public bool IsFullScreen { get; set; }

        [JsonDataMember(Name = "screenBrightness")]
        public float ScreenBrightness { get; set; }

        [JsonDataMember(Name = "fps")]
        public int FrameRate { get; set; }

        [JsonDataMember(Name = "runningPlatform")]
        public string RunningPlatform { get; set; }

        [JsonDataMember(Name = "sysLng")]
        public string SystemLanguage { get; set; }

        [JsonDataMember(Name = "internetReachability")]
        public string InternetReachability { get; set; }

        [JsonDataMember(Name = "internetReachability")]
        public string AppInstallerName { get; set; }

        [JsonDataMember(Name = "appInstallMode")]
        public string AppInstallMode { get; set; }

        [JsonDataMember(Name = "platform")]
        public string Platform { get; set; }

        [JsonDataMember(Name = "appVer")]
        public string AppVersion { get; set; }

        [JsonDataMember(Name = "unityVer")]
        public string UnityVersion { get; set; }

        [JsonDataMember(Name = "appId")]
        public string AppId { get; set; }

        public override void Init()
        {
            base.Init();

            Id = this.GetId();

            DeviceModel = UnityEngine.Device.SystemInfo.deviceModel;
            DeviceName = UnityEngine.Device.SystemInfo.deviceName;
            OperatingSystem = UnityEngine.Device.SystemInfo.operatingSystem;
            DeviceUniqueId = UnityEngine.Device.SystemInfo.deviceUniqueIdentifier;
            GraphicsDeviceName = UnityEngine.Device.SystemInfo.graphicsDeviceName;
            ProcessorType = UnityEngine.Device.SystemInfo.processorType;
            BatteryLevel = UnityEngine.Device.SystemInfo.batteryLevel;
            GraphicsMemorySize = UnityEngine.Device.SystemInfo.graphicsMemorySize;
            ProcessorCount = UnityEngine.Device.SystemInfo.processorCount;
            ProcessorFrequency = UnityEngine.Device.SystemInfo.processorFrequency;
            MaxAnisotropyLevel = UnityEngine.Device.SystemInfo.maxAnisotropyLevel;
            DeviceType = UnityEngine.Device.SystemInfo.deviceType.ToString();
            GraphicsShaderLevel = UnityEngine.Device.SystemInfo.graphicsShaderLevel;
            MaxCubemapSize = UnityEngine.Device.SystemInfo.maxCubemapSize;
            SupportsMultisampledTextures = UnityEngine.Device.SystemInfo.supportsMultisampledTextures;
            SystemMemorySize = UnityEngine.Device.SystemInfo.systemMemorySize;
            GraphicsDeviceID = UnityEngine.Device.SystemInfo.graphicsDeviceID;
            GraphicsDeviceVendorID = UnityEngine.Device.SystemInfo.graphicsDeviceVendorID;
            IsAccelerometerSupported = UnityEngine.Device.SystemInfo.supportsAccelerometer;
            Supports2DArrayTextures = UnityEngine.Device.SystemInfo.supports2DArrayTextures;
            Supports3DTextures = UnityEngine.Device.SystemInfo.supports3DTextures;
            IsAudioSupported = UnityEngine.Device.SystemInfo.supportsAudio;
            IsGraphicsMultiThreaded = UnityEngine.Device.SystemInfo.graphicsMultiThreaded;

            AppId = UnityEngine.Device.Application.identifier;
            UnityVersion = UnityEngine.Device.Application.unityVersion;
            AppVersion = UnityEngine.Device.Application.version;
            Platform = UnityEngine.Device.Application.platform.ToString();
            AppInstallMode = UnityEngine.Device.Application.installMode.ToString();
            AppInstallerName = UnityEngine.Device.Application.installerName;
            InternetReachability = UnityEngine.Device.Application.internetReachability.ToString();
            SystemLanguage = UnityEngine.Device.Application.systemLanguage.ToString();
            FrameRate = UnityEngine.Device.Application.targetFrameRate;

            if (UnityEngine.Device.Application.isEditor)
            {
                RunningPlatform = "Editor";
            }
            else if (UnityEngine.Device.Application.isConsolePlatform)
            {
                RunningPlatform = "Console";
            }
            else if (UnityEngine.Device.Application.isMobilePlatform)
            {
                RunningPlatform = "Mobile";
            }
            else
            {
                RunningPlatform = "Undefined";
            }

            ScreenBrightness = UnityEngine.Device.Screen.brightness;
            IsFullScreen = UnityEngine.Device.Screen.fullScreen;
            ScreenDpi = UnityEngine.Device.Screen.dpi;
            ScreenWidth = UnityEngine.Device.Screen.width;
            ScreenHeight = UnityEngine.Device.Screen.height;
            ScreenOrientation = UnityEngine.Device.Screen.orientation.ToString();
            CurrentResolution = UnityEngine.Device.Screen.currentResolution.ToString();
        }

        public override void Init(IEntityConfig config)
        {
            Config = config;
        }
    }
}
