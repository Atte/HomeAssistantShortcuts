using System.Configuration;

namespace HomeAssistantShortcuts.Properties {
    internal sealed partial class Settings : ApplicationSettingsBase
    {
        public static Settings Default { get; } = (Settings)Synchronized(new Settings());

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("http://localhost:8123/api/")]
        public string ApiBaseUrl
        {
            get => (string)this["ApiBaseUrl"];
            set => this["ApiBaseUrl"] = value;
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("")]
        public string ApiAccessToken
        {
            get => (string)this["ApiAccessToken"];
            set => this["ApiAccessToken"] = value;
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("")]
        public Shortcuts Shortcuts
        {
            get => (Shortcuts)this["Shortcuts"];
            set => this["Shortcuts"] = value;
        }
    }
}
