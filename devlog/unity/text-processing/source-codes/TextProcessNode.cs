using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataDriven.TextProcess
{
    //[CreateAssetMenu(fileName = "New ProcessNode", menuName = "DataDriven/DataProcess/ProcessNode")]
    public abstract class TextProcessNode : ScriptableObject
    {
        public abstract IEnumerator ProcessingRoutine(ProcessingData[] input, Action<ProcessingData[]> onFinishedCallback);
    }
}
