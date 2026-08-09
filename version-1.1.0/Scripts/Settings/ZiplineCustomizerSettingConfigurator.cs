using Bindito.Core;

namespace SouvyZiplineCustomizer.Settings
{
    [Context("MainMenu")]
    [Context("Game")]
    [Context("MapEditor")]
    public class ZiplineCustomizerSettingConfigurator : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition)
        {
            containerDefinition.Bind<ZiplineCustomizerSettingsOwner>().AsSingleton();
        }
    }
}
