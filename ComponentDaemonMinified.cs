/*
 * Created by Connor Ford.
 */

// Amount of required components
Dictionary<string,int>N=new Dictionary<string,int>(){
    {"Computer",1000},
    {"Construction",1000},
    {"Display",1000},
    {"InteriorPlate",10000},
    {"LargeTube",1000},
    {"MetalGrid",1000},
    {"Motor",1000},
    {"SmallTube",1000},
    {"SteelPlate",10000}
};

// Assembler name
string M="Assembler Master";

// Do not alter anything below this line.
List<string>L=new List<string>{"Computer","Construction","Motor"};IMyAssembler K;List<IMyTerminalBlock>J;int I=0;
Program(){Runtime.UpdateFrequency=UpdateFrequency.Update100;}void Main(string H,UpdateType G){K=GridTerminalSystem.
GetBlockWithName(M)as IMyAssembler;J=new List<IMyTerminalBlock>();GridTerminalSystem.GetBlocksOfType(J,F=>F.HasInventory&&F.
IsSameConstructAs(Me));if(I!=5){I++;return;}I=0;foreach(string E in N.Keys){if(N[E]==0)continue;int D=0;int C=0;foreach(IMyTerminalBlock
B in J){if(B is IMyAssembler){List<MyProductionItem>O=new List<MyProductionItem>();K.GetQueue(O);foreach(MyProductionItem
A in O){if(A.BlueprintId==MyDefinitionId.Parse("MyObjectBuilder_BlueprintDefinition/"+E+(L.Contains(E)?"Component":""))){
C+=(int)A.Amount;}}}D+=(int)B.GetInventory(0).GetItemAmount(new MyItemType("MyObjectBuilder_Component",E));}Echo(E+": "+D
+" + "+C+" / "+N[E]);if(D+C<N[E]){K.AddQueueItem(MyDefinitionId.Parse("MyObjectBuilder_BlueprintDefinition/"+E),(double)N
[E]-(D+C));}}}