using HarmonyLib;
using System.Collections.Generic;

[HarmonyPatch]
public class DocsGreyApiPatch
{
    [HarmonyPatch(typeof(DocsGreyApi), "Awake")]
    class AwakePatch
    {
        private static string _type = "Computer";
        private static List<DocsGreyApi.Method> _methods = new List<DocsGreyApi.Method>()
        {
            new DocsGreyApi.Method(){ name = "get_type", args = "" }
        };

        static void Postfix(DocsGreyApi __instance)
        {
            foreach (var method in _methods)
            {
                DocsGreyApi.Singleton.clases[_type].methods.Add(method);
            }
        }
    }
}