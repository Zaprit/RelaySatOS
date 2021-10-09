using Sandbox.ModAPI.Ingame;
using System;
using System.Collections;
using System.Text;
using VRage.Game.GUI.TextPanel;
using VRageMath;

namespace IngameScript
{
    partial class Program
    {
        public class LCDUtils
        {
            Program _program;
            IMyTextSurface text;
            IMyTextSurface keypad;
            public ArrayList scrollback = new ArrayList();
            public LCDUtils(Program program) {
                _program = program;
                text = program.Me.GetSurface(0);
                keypad = program.Me.GetSurface(1);
                text.BackgroundColor = Color.Orange;
                text.ContentType = ContentType.TEXT_AND_IMAGE;
                text.FontSize = 0.505F;
                text.Font = "Monospace";
                keypad.ContentType = ContentType.SCRIPT;
                keypad.Script = "TSS_FactionIcon";
                keypad.ScriptBackgroundColor = Color.Orange;
                keypad.ScriptForegroundColor = Color.White;
                
            }

            /*
             * BSOD Style Error 
             */
            public void Error(String error)
            {
                text.BackgroundColor = Color.Blue;
                text.FontSize = 1.5F;
                text.Alignment = TextAlignment.CENTER;

                keypad.ScriptBackgroundColor = Color.Blue;


                text.WriteText(String.Format("RelaySatOS\n\nERROR: {0}", error));

                _program.Echo("ERROR: " + error);
                _program.Runtime.UpdateFrequency = UpdateFrequency.None;
                
            }
            /*
             *  BSOD With Details In Log
             */
            public void Error(String error, String details)
            {
                text.BackgroundColor = Color.Blue;
                text.Font = "Monospace";
                text.FontSize = 0.996F;
                text.Alignment = TextAlignment.CENTER;

                keypad.ContentType = ContentType.TEXT_AND_IMAGE;
                keypad.Font = "Monospace";
                keypad.FontSize = 2.745F;
                keypad.WriteText(":( This kinda sucks");
                keypad.BackgroundColor = Color.Blue;
                keypad.TextPadding = 0F;

                text.WriteText(String.Format("RelaySatOS\n\n\n\nUnrecoverable Error:\n{0}\n\n\nSee Console For Details", error));

                _program.Echo("Error In Program:\n"+details);
                _program.Runtime.UpdateFrequency = UpdateFrequency.None;
            }

            public void Print(string print)
            {
                if(scrollback.Count >= Constants.maxVLines)
                {
                    scrollback.RemoveAt(0);
                }
                StringBuilder sb = new StringBuilder();
                int carriage = 0;
                for (int i = 0; i < print.Length; i++)
                {

                    if(carriage >= Constants.maxHLines)
                    {
                        sb.Append('\n');
                        carriage = 0;
                    }
                    carriage++;
                    sb.Append(print.ToCharArray()[i]);
                }
                scrollback.Add(sb.ToString());


                ClearScreen();
                foreach(string line in scrollback)
                {
                    text.WriteText(line, true);
                }
            }

            public void Println(string print) {
                if (scrollback.Count >= Constants.maxVLines)
                {
                    scrollback.RemoveAt(0);
                }
                StringBuilder sb = new StringBuilder();
                int carriage = 0;
                for (int i = 0; i < print.Length; i++)
                {

                    if (carriage >= Constants.maxHLines)
                    {
                        sb.Append('\n');
                        carriage = 0;
                    }
                    carriage++;
                    sb.Append(print.ToCharArray()[i]);
                }
                sb.Append("\n");
                scrollback.Add(sb.ToString());


                ClearScreen();
                foreach (string line in scrollback)
                {
                    text.WriteText(line, true);
                }
            }

            public void ClearScreen() => text.WriteText("");

            public void WriteToScreen(String Display) => text.WriteText(Display);
            public void AppendToScreen(String Display) => text.WriteText(Display,true);

        }
    }
}
