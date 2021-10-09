using System;

namespace IngameScript
{
    partial class Program
    {
        public class Constants
        {
            public static string version = "v0.2-alpha";
            public static string titletext = String.Format("RelaySatOS {0}", version);
            public static int maxVLines = 20;
            public static int maxHLines = 51;
            public static string ADTag = "autoDiscoverRS";
            public static int maxADRetries = 100;
        }
    }
}
