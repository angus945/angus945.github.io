using System.Collections;
using System.Collections.Generic;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;

namespace DataDriven.TextProcess
{
    [CustomEditor(typeof(DataProcessList))]
    public class DataParsingListEditor : Editor
    {
        DataProcessList parsingList;

        private void OnEnable()
        {
            parsingList = (DataProcessList)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DownloadButton();

            //if(GUILayout.Button("Test"))
            //{
            //    Debug.Log(Application.streamingAssetsPath);
            //}
        }

        void DownloadButton()
        {
            //EditorGUI.BeginDisabledGroup(downloading);
            if (GUILayout.Button("ParstDatas"))
            {
                //downloading = true;
                //EditorCoroutineUtility.StopCoroutine

                // Packge Manager > Editor Coroutine 
                EditorCoroutineUtility.StartCoroutine(parsingList.ParsingRoutine(PrintOutDatas), this);
            }
            //EditorGUI.EndDisabledGroup();
        }
        void PrintOutDatas(ProcessingData[] datas)
        {
            foreach (ProcessingData item in datas)
            {
                Debug.Log(item);
            }
        }
        //void DownloadComplete(bool success, string data)
        //{
        //    downloading = false;

        //    if (success)
        //    {
        //        //SerializedProperty datas = serializedObject.FindProperty("_sourceData");
        //        //datas.stringValue = data;        
        //    }

        //    Repaint();
        //}
    }
}
