using System.Collections.Generic;

namespace IngameScript
{
    partial class Program
    {
        public class Config
        {
            Dictionary<string,string> config = new Dictionary<string, string>();
            public Config(Program program) 
            {
                if (program.Me.CustomData != "")
                {
                    foreach (string configValue in program.Me.CustomData.Split('\n'))
                    {
                        string[] parts = configValue.Split('=');
                        config.Add(parts[0], parts[1]);
                    }
                }
            }
            public string Get(string key) {
                return(config[key]);
            }
        }
    }
}
