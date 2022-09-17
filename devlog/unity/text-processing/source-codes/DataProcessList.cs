using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataDriven.TextProcess
{
    [CreateAssetMenu(fileName = "New ProcessList", menuName = "DataDriven/DataProcess/ProcessList", order = 0)]
    public class DataProcessList : ScriptableObject
    {
        [SerializeField] TextProcessNode[] processNodes;

        public IEnumerator ParsingRoutine(Action<ProcessingData[]> onFinishedCallback)
        {
            ProcessingData[] parsingDatas = new ProcessingData[0];

            for (int i = 0; i < processNodes.Length; i++)
            {
                yield return processNodes[i].ProcessingRoutine(parsingDatas, (datas) =>
                {
                    parsingDatas = datas;
                });
            }

            onFinishedCallback?.Invoke(parsingDatas);
        }
    }
}
