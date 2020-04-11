using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System;
using VRage.Collections;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Game;
using VRage;
using VRageMath;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {
        // Needed amounts of components
        Dictionary<string, int> minComponents = new Dictionary<string, int>()
        {
            { "Computer", 1000 },
            { "Construction", 1000 },
            { "Display", 1000 },
            { "InteriorPlate", 10000 },
            { "LargeTube", 1000 },
            { "MetalGrid", 1000 },
            { "Motor", 1000 },
            { "SmallTube", 1000 },
            { "SteelPlate", 10000 }
        };

        // Name of assembler to queue
        string assemblerName = "Assembler Master";

        // List to clarify what needs the "Component" tag during the assembler conditional
        List<string> assemblerComponents = new List<string>
        {
            "Computer",
            "Construction",
            "Motor"
        };

        // Space Engineers interfaces
        IMyAssembler assembler;
        List<IMyTerminalBlock> blocksWithInventory;

        int counter = 0;

        // Run on initialization
        public Program()
        {
            // Runs Main every 100 ticks
            Runtime.UpdateFrequency = UpdateFrequency.Update100;
        }

        public void Main(string argument, UpdateType updateSource)
        {
            // Gets assembler
            assembler = GridTerminalSystem.GetBlockWithName(assemblerName) as IMyAssembler;

            // Gets every block in same grid with an inventory and stores it in blocksWithInventory
            blocksWithInventory = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType(blocksWithInventory, x => x.HasInventory && x.IsSameConstructAs(Me));

            // Only runs every 10 seconds (60 ticks per second, 600 per ten, called every 100, loops for six then runs)
            if (counter != 5)
            {
                counter++;
                return;
            }
            counter = 0;

            // For each required component
            foreach (string component in minComponents.Keys)
            {
                // If none specified
                if (minComponents[component] == 0) continue;

                int amountInv = 0;
                int amountQueued = 0;
                // For each block in list of blocks
                foreach (IMyTerminalBlock blockWithInventory in blocksWithInventory)
                {
                    // If blockWithInventory is an assembler
                    if (blockWithInventory is IMyAssembler)
                    {
                        // Get queue of assembler
                        List<MyProductionItem> assemblerQueue = new List<MyProductionItem>();
                        assembler.GetQueue(assemblerQueue);
                        // For each item in queue
                        foreach (MyProductionItem productionItem in assemblerQueue)
                        {
                            // If item is component (tacking on "Component" keyword to some components because Space Engineers is nothing if not inconsistent)
                            if (productionItem.BlueprintId == MyDefinitionId.Parse("MyObjectBuilder_BlueprintDefinition/" + component + 
                                (assemblerComponents.Contains(component) ? "Component" : ""))) {
                                // Add amount of component
                                amountQueued += (int) productionItem.Amount;
                            }
                        }
                    }
                    // Add amount of component in inventory
                    amountInv += (int)blockWithInventory.GetInventory(0).GetItemAmount(new MyItemType("MyObjectBuilder_Component", component));
                }
                Echo(component + ": " + amountInv + " + " + amountQueued + " / " + minComponents[component]);
                // If less of component than minimum
                if (amountInv + amountQueued < minComponents[component])
                {
                    // Queue enough of item to meet minimum
                    assembler.AddQueueItem(
                        MyDefinitionId.Parse("MyObjectBuilder_BlueprintDefinition/" + component),
                        (double) minComponents[component] - (amountInv + amountQueued)
                    );
                }
            }
        }
    }
}
