using HarmonyLib;
using Miniscript;
using System.Reflection;

[HarmonyPatch]
public class GreyInterpreterPatch
{
    [HarmonyPatch(typeof(GreyInterpreter), "ComputerType")]
    class ComputerTypePatch
    {
        static void Postfix(GreyInterpreter __instance, ref GreyMap __result)
        {
            FieldInfo fieldInfo = AccessTools.Field(typeof(GreyInterpreter), "_computerType");
            GreyMap _computerType = fieldInfo.GetValue(__instance) as GreyMap;
            if (!_computerType.ContainsKey("get_type"))
            {
                _computerType["get_type"] = Intrinsic.GetByName("get_type").GetFunc();
            }
            fieldInfo.SetValue(__instance, _computerType);
            __result = _computerType;
        }
    }
}