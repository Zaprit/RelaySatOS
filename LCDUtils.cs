using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript
{
    partial class Program
    {
        public class LCDUtils
        {
            Program _program;
            IMyTextSurfaceProvider text;
            public LCDUtils(Program program) {
                _program = program;
                List<IMyProgrammableBlock> blocks = new List<IMyProgrammableBlock>();
                program.GridTerminalSystem.GetBlocksOfType(blocks);
                text = blocks[0];
                text.GetSurface(0).BackgroundColor = Color.Orange;
            }

            public void WriteToScreen(String Display)
            {
                text.GetSurface(0).WriteText(Display);
            }
            public void AppendToScreen(String Display)
            {
                text.GetSurface(0).WriteText(Display,true);
            }
        }
    }
}
