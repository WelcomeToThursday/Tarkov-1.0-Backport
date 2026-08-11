using BepInEx;

namespace PunisherBossModPreloader
{

    [BepInPlugin("com.wtt.contentbackport", "Content Backport Preloader Patch", "1.1.4")]
    public class Patcher : BaseUnityPlugin
    {

        public static Patcher Instance { get; private set; }

        public void Awake()
        {
            Patcher.Instance = this;
        }
    }
}
