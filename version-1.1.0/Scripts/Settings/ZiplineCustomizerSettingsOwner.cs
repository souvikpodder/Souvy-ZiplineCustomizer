using System.Reflection;
using ModSettings.Common;
using ModSettings.Core;
using Timberborn.Modding;
using Timberborn.SettingsSystem;

namespace SouvyZiplineCustomizer.Settings
{
    public class ZiplineCustomizerSettingsOwner : ModSettingsOwner
    {
        public RangeIntModSetting MaxDistanceSetting { get; } = new(
            50, 10, 500,
            ModSettingDescriptor.CreateLocalized("Souvy.ZiplineCustomizer.Setting.MaxDistance.Label")
                               .SetLocalizedTooltip("Souvy.ZiplineCustomizer.Setting.MaxDistance.Tooltip")
        );

        public RangeIntModSetting MaxAngleSetting { get; } = new(
            50, 10, 89,
            ModSettingDescriptor.CreateLocalized("Souvy.ZiplineCustomizer.Setting.MaxAngle.Label")
                               .SetLocalizedTooltip("Souvy.ZiplineCustomizer.Setting.MaxAngle.Tooltip")
        );

        public RangeIntModSetting MaxConnectionsSetting { get; } = new(
            8, 1, 20,
            ModSettingDescriptor.CreateLocalized("Souvy.ZiplineCustomizer.Setting.MaxConnections.Label")
                               .SetLocalizedTooltip("Souvy.ZiplineCustomizer.Setting.MaxConnections.Tooltip")
        );

        public RangeIntModSetting SpeedMultiplierSetting { get; } = new(
            2, 1, 5,
            ModSettingDescriptor.CreateLocalized("Souvy.ZiplineCustomizer.Setting.SpeedMultiplier.Label")
                               .SetLocalizedTooltip("Souvy.ZiplineCustomizer.Setting.SpeedMultiplier.Tooltip")
        );

        public static ZiplineCustomizerSettingsOwner Instance { get; private set; }

        public ZiplineCustomizerSettingsOwner(
            ISettings settings,
            ModSettingsOwnerRegistry modSettingsOwnerRegistry,
            ModRepository modRepository
        ) : base(settings, modSettingsOwnerRegistry, modRepository)
        {
            Instance = this;
            CleanupDuplicateRegistrations(modSettingsOwnerRegistry);
        }

        private void CleanupDuplicateRegistrations(ModSettingsOwnerRegistry modSettingsOwnerRegistry)
        {
            var field = typeof(ModSettingsOwnerRegistry).GetField("_modSettingOwners", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                var dict = field.GetValue(modSettingsOwnerRegistry) as System.Collections.IDictionary;
                if (dict != null)
                {
                    foreach (System.Collections.IList list in dict.Values)
                    {
                        if (list != null && list.Contains(this) && list.Count > 1)
                        {
                            list.Remove(this);
                            break;
                        }
                    }
                }
            }
        }

        public override string HeaderLocKey => "Souvy.ZiplineCustomizer.Setting.Header";

        public override ModSettingsContext ChangeableOn => ModSettingsContext.MainMenu | ModSettingsContext.Game;

        protected override string ModId => ZiplineCustomizerPlugin.Id;
    }
}
