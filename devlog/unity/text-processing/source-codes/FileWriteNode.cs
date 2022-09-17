using System;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace DataDriven.TextProcess
{
    [CreateAssetMenu(fileName = "New FileWriteNode", menuName = "DataDriven/DataProcess/IO")]
    public class FileWriteNode : TextProcessNode
    {
        [SerializeField] string writeLocation = "";
        [SerializeField] string fileFormat = "json";

        [SerializeField] bool relativelyFlag;

        public override IEnumerator ProcessingRoutine(ProcessingData[] input, Action<ProcessingData[]> onFinishedCallback)
        {
            for (int i = 0; i < input.Length; i++)
            {
                ProcessingData datas = input[i];

                if(relativelyFlag)
                {
                    string folderPath = $"{Application.dataPath}/{writeLocation}/{datas.dataFlag}";
                    string path = $"{folderPath}/{datas.dataName}.{fileFormat}";

                    Directory.CreateDirectory(folderPath);
                    File.WriteAllText(path, datas.contents.PrintOut());
                }
                else
                {
                    string path = $"{Application.dataPath}/{writeLocation}/{datas.dataName}.{fileFormat}";
                    File.WriteAllText(path, datas.contents.PrintOut());
                }
            }

            AssetDatabase.Refresh();
            onFinishedCallback?.Invoke(input);

            yield return null;
        }
    }
}
