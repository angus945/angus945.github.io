using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FeatureAccess : MonoBehaviour
{
    //[SerializeField] ScriptableRendererData rendererData;
    [SerializeField] bool enableFeature;
    [SerializeField] Vector3 scale;

    CustomRenderFeature feature;

    ScriptableRendererData renderData;
    ScriptableRendererFeature addFeature;

    void Start()
    {
        RenderPipelineAsset pipelineAsset = GraphicsSettings.renderPipelineAsset;
        //RenderPipelineAsset pipelineAsset = (RenderPipelineAsset)QualitySettings.renderPipeline;

        FieldInfo propertyInfo = pipelineAsset.GetType().GetField("m_RendererDataList", BindingFlags.Instance | BindingFlags.NonPublic);
        renderData = ((ScriptableRendererData[])propertyInfo?.GetValue(pipelineAsset))?[0];

        List<ScriptableRendererFeature> features = renderData.rendererFeatures;
        feature = features.Find(n => n.name == "CustomRenderFeature") as CustomRenderFeature;

        addFeature = ScriptableObject.CreateInstance<CustomRenderFeature>();
        renderData.rendererFeatures.Add(addFeature);
    }

    // Update is called once per frame
    void Update()
    {
        feature.SetActive(enableFeature);
        feature.SetScale(scale);
    }

    void OnDisable()
    {
        renderData.rendererFeatures.Remove(addFeature);
    }
}
