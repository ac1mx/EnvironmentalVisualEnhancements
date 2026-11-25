using System;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace _BuildManager
{
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    public class _BuildManager : MonoBehaviour
    {
        private void Awake()
        {
            logVersion();

            AddLoadingTips();
        }

        private void AddLoadingTips()
        {
            var loadingScreens = LoadingScreen.Instance.Screens;
            if (loadingScreens.Count > 1) {
                // The tips appear on the "second" loading screen, after the initial "Loading..." screen.
                int tipScreen = 1;
                var tips = loadingScreens[tipScreen].tips;
                string[] newTips = {
                    "Inserting Head Into Clouds...", // How KSP makes us all feel.
                    "Luminously Urbanizing...", // City lights
                    "Searching for the Aurora Borealis...", "Searching for the Aurora Australis...", // You'll find them in SVE!
                    "Reduxing EVE..."
                };
                Array.Resize(ref tips, tips.Length + newTips.Length);
                newTips.CopyTo(tips, tips.Length - newTips.Length);
                loadingScreens[tipScreen].tips = tips;
            }
        }

        private void logVersion()
        {
            string path = Assembly.GetExecutingAssembly().Location;
            if (String.IsNullOrEmpty(path)) return;
            string directory = Path.GetDirectoryName(path);
            var versionInfo = new StringBuilder("[BLACKRACK_EVE_REDUX] Version Info:\n");
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.IsDynamic) continue;
                string candidate = assembly.Location;
                if (String.IsNullOrEmpty(candidate) || Path.GetDirectoryName(candidate) != directory) continue;
                AssemblyName name = assembly.GetName();
                versionInfo.Append(name.Name).Append(", ").Append(name.Version).Append('\n');
            }

            KSPLog.print(versionInfo.ToString());
        }
    }
    
}
