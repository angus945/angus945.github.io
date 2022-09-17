using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace DataDriven.TextProcess
{
    [CreateAssetMenu(fileName = "New GASAccessNode", menuName = "DataDriven/DataProcess/GASAccess")]
    public class GASDataAccessNode : TextProcessNode
    {
        public const string postURL = "https://script.google.com/macros/s/AKfycbygvwXyQTff_GmkDAURYHHwfJnks7Nlt717-9GPBIIEq8AxWywHbVYv0ZhaaE9Yt0ZG/exec";

        [SerializeField] string excelID = "";
        [SerializeField] string[] sheetNames = null;

        public override IEnumerator ProcessingRoutine(ProcessingData[] input, Action<ProcessingData[]> onFinishedCallback)
        {
            ProcessingData[] datas = new ProcessingData[sheetNames.Length];

            for (int i = 0; i < datas.Length; i++)
            {
                WWWForm form = new WWWForm();
                form.AddField("id", excelID);
                form.AddField("name", sheetNames[i]);

                using (UnityWebRequest www = UnityWebRequest.Post(postURL, form))
                {
                    yield return www.SendWebRequest();

                    if (www.result != UnityWebRequest.Result.Success)
                    {
                        Debug.Log(www.error);
                        //onFinishedCallback?.Invoke(false, "");
                    }
                    else
                    {
                        datas[i] = new ProcessingData(sheetNames[i], "", www.downloadHandler.text, "");

                        Debug.Log("download complete!");
                    }
                }
            }

            onFinishedCallback?.Invoke(datas);
        }
    }
}
