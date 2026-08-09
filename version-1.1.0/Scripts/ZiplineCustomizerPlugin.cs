using System.Security.Permissions;
using HarmonyLib;
using Timberborn.ModManagerScene;

#pragma warning disable CS0618
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618

namespace SouvyZiplineCustomizer
{
    public class ZiplineCustomizerPlugin : IModStarter
    {
        public const string Id = "Souvy.ZiplineCustomizer";

        public void StartMod(IModEnvironment modEnvironment)
        {
            new Harmony(Id).PatchAll();
        }
    }
}
