using HarmonyLib;
using Miniscript;
using NetworkMessages;
using System.Collections.Generic;
using System.Reflection;

[HarmonyPatch]
class ComputerIntrinsicsPatch
{
    private static bool intrinsicsAdded;

    [HarmonyPatch(typeof(ComputerIntrinsics), "AddInstrinsics")]
    static void Postfix()
    {
        if (intrinsicsAdded == true)
            return;
        intrinsicsAdded = true;
        Intrinsic intrinsic1 = Intrinsic.Create("get_type");
        intrinsic1.AddParam("self");
        intrinsic1.code = delegate(TAC.Context context, Intrinsic.Result partialResult)
        {
            GreyMap greyMap = context.GetVar("self") as GreyMap;
            if (greyMap != null)
            {
                GreyInterpreter greyInterpreter = (GreyInterpreter)context.interpreter;
                var computer = greyInterpreter.hostData.GetComputer(greyMap).GetComputer();
                if (computer != null)
                {
                    FieldInfo devInfo = AccessTools.Field(typeof(Computer), "typeDevice");
                    var devOut = devInfo.GetValue(computer);
                    if (devOut != null)
                    {
                        NetworkLan.TypeDevice typeDevice = (NetworkLan.TypeDevice)devOut;
                        return new Intrinsic.Result(typeDevice.ToString().ToLower());
                    }
                }
            }
            return Intrinsic.Result.Null;
        };
    }
}