using HarmonyLib;
using System.Collections.Generic;

[HarmonyPatch]
public class PlayerUtilsPatch
{
    [HarmonyPatch(typeof(PlayerUtils), "ConfigLanguage")]
    class ConfigLanguagePatch
    {
        private static Dictionary<string, string> contents = new Dictionary<string, string>()
        {
            { "DOC_COMPUTER_GET_TYPE", "<mark=#00D0124D><b>[Parameters]</b></mark>\n<color=#33cccc>None</color>\n\n<mark=#00D0124D><b>[Description]</b></mark>\nReturns a <color=#33cccc>string</color> with the type of the current device." }
        };

        static void Postfix()
        {
            foreach (var content in contents)
            {
                TranslationSystem.Singleton.rootTexts["English"].content.Add(content.Key, content.Value);
            }
        }
    }
}