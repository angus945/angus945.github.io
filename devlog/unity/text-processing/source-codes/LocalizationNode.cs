using DataDriven.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DataDriven.Localization
{
    [System.Serializable]
    class LocalizationItem
    {
        public string key;
        public string value;

        public LocalizationItem(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
    }

}
namespace DataDriven.TextProcess
{
    [CreateAssetMenu(fileName = "New LocalizationNode", menuName = "DataDriven/DataProcess/Localization")]
    public class LocalizationNode : TextProcessNode
    {

        public override IEnumerator ProcessingRoutine(ProcessingData[] input, Action<ProcessingData[]> onFinishedCallback)
        {
            List<ProcessingData> outputDatas = new List<ProcessingData>();

            for (int i = 0; i < input.Length; i++)
            {
                string[] contents = TextAnalize.ParseToArray(input[i].contents);

                Dictionary<string, List<string>> table = StageB(contents);
                foreach (KeyValuePair<string, List<string>> item in table)
                {
                    outputDatas.Add(new ProcessingData(input[i].dataName, item.Key, item.Value.ToArray(), null));
                }
            }

            onFinishedCallback.Invoke(outputDatas.ToArray());

            yield return null;
        }

        static Dictionary<string, List<string>> StageB(string[] contents)
        {
            Dictionary<string, List<string>> languageTable = new Dictionary<string, List<string>>();

            for (int i = 0; i < contents.Length; i++)
            {
                string[] item = TextAnalize.ParseToItems(contents[i]);
                string key = item[0];

                for (int languageIndex = 1; languageIndex < item.Length; languageIndex++)
                {
                    TextAnalize.AnalizeItem(item[languageIndex], out string language, out string value);
                    
                    if(!languageTable.ContainsKey(language))
                    {
                        languageTable.Add(language, new List<string>());
                    }

                    LocalizationItem localize = new LocalizationItem(key, value);
                    languageTable[language].Add(JsonUtility.ToJson(localize));
                }
            }

            return languageTable;

        }

    }
}
