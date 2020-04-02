/*
 * Created by Connor Ford.
 * 
 * Argument usage: {Assembler name}
 * EX: Assembler Master
 */

// Set each number to the amount you want as a minimum, leave as 0 if you don't want it checked
Dictionary<string,int>N=new Dictionary<string,int>(){
    {"BulletproofGlass",0},
    {"Canvas",0},
    {"Computer",0},
    {"Construction",0},
    {"Detector",0},
    {"Display",0},
    {"Explosives",0},
    {"Girder",0},
    {"GravityGenerator",0},
    {"InteriorPlate",0},
    {"LargeTube",0},
    {"Medical",0},
    {"MetalGrid",0},
    {"Motor",0},
    {"PowerCell",0},
    {"RadioCommunication",0},
    {"Reactor",0},
    {"SmallTube",0},
    {"SolarCell",0},
    {"SteelPlate",0},
    {"Superconductor",0},
    {"Thrust",0},
    {"ZoneChip",0}
};
// Do not alter anything below this line
List<string>M=new List<string>{"Computer","Construction","Detector","Explosives","Girder","GravityGenerator","Medical","Motor","RadioCommunication","Reactor","Thrust"};
IMyAssembler L;List<IMyTerminalBlock>K;int J=0;String[]I;Program(){Runtime.UpdateFrequency=UpdateFrequency.Update100;}void Main(
string H,UpdateType G){I=H.Split(';');L=GridTerminalSystem.GetBlockWithName(I[0])as IMyAssembler;if(L==null){Echo(
"An argument was incorrect or missing. Remember, block names are case sensitive.");return;}K=new List<IMyTerminalBlock>();GridTerminalSystem.GetBlocksOfType(K,F=>F.HasInventory&&F.IsSameConstructAs(Me)
);if(J!=5){J++;return;}J=0;foreach(string E in N.Keys){if(N[E]==0)continue;int D=0;int C=0;foreach(IMyTerminalBlock B in
K){if(B is IMyAssembler){List<MyProductionItem>O=new List<MyProductionItem>();L.GetQueue(O);foreach(MyProductionItem A in
O){if(A.BlueprintId==MyDefinitionId.Parse("MyObjectBuilder_BlueprintDefinition/"+E+(M.Contains(E)?"Component":""))){C+=(
int)A.Amount;}}}D+=(int)B.GetInventory(0).GetItemAmount(new MyItemType("MyObjectBuilder_Component",E));}Echo(E+": "+D+" + "
+C+" / "+N[E]);if(D+C<N[E]){L.AddQueueItem(MyDefinitionId.Parse("MyObjectBuilder_BlueprintDefinition/"+E),(double)N[E]-(D
+C));}}}