using Sandbox.ModAPI.Ingame;
using System.Text;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {
        public Config cfg;
        public LCDUtils lcd;
        IMyBroadcastListener listener;
        public Program()
        {
            init();
        }
        private void init() {
            Runtime.UpdateFrequency = UpdateFrequency.Update100;
            IMyRadioAntenna radioAntenna = (IMyRadioAntenna)GridTerminalSystem.GetBlockWithName("DiscoveryAntenna");
            lcd = new LCDUtils(this);
            if (Storage == "")
            {

                lcd.Println(Constants.titletext);
                lcd.Println("Initialising RelaySAT");
                lcd.Println("Loading Config");
                lcd.Println("Initialised Radio with address: " + IGC.Me);
            }
            listener = IGC.RegisterBroadcastListener(Constants.ADTag);
            listener.SetMessageCallback(Constants.ADTag);
            cfg = new Config(this);
        }
        public void Save()
        {

            var sb = new StringBuilder();
            foreach(string str in lcd.scrollback.ToArray())
            {
                sb.Append(str);
            }
            Storage = sb.ToString();
            
        }
        int count = 0;
        bool registered = false;
        public void Main(string argument, UpdateType updateSource)
        {
            if(updateSource == UpdateType.Terminal)
            {
                Storage = "";
                init();
            }
            if (lcd.scrollback.Count == 0) 
            {
                foreach(string line in Storage.Split('\n'))
                {
                    lcd.scrollback.Add(line + "\n");
                }
                lcd.ClearScreen();
                foreach (string line in lcd.scrollback)
                {
                    lcd.AppendToScreen(line);
                }
            }
            IGC.SendBroadcastMessage(Constants.ADTag, "AutoDiscoverRequest");
            count++;
            if (!registered && count < Constants.maxADRetries)
            {
                lcd.Error("No BaseStation Response", "No Response From BaseStation after "+Constants.maxADRetries+" tries,\n please move within range of basestation and try again.");
            }
            while (listener.HasPendingMessage)
            {
                var message = listener.AcceptMessage();
                if ((string)message.Data == "ADBSAccept")
                {
                    registered = true;
                }
            }

        }
    }
}
