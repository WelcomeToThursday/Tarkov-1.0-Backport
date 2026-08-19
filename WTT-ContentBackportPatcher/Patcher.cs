using BepInEx;

namespace PunisherBossModPreloader
{

    [BepInPlugin("com.wtt.contentbackport", "Content Backport Preloader Patch", "1.1.5")]
    public class Patcher : BaseUnityPlugin
    {

        public static Patcher Instance { get; private set; }

        public void Awake()
        {
            Patcher.Instance = this;
        }
    }
}
