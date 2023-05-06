using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CustomRenderFeature : ScriptableRendererFeature
{

    [SerializeField] Mesh mesh;
    [SerializeField] Vector3 position;
    [SerializeField] Vector3 rotation;
    [SerializeField] Vector3 scale;
    [SerializeField] Material material;

    CustomRenderPass renderPass;

    public override void Create()
    {
        renderPass = new CustomRenderPass();

        renderPass.SetRenderObject(mesh, position, rotation, scale, material);
    }
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(renderPass);
    }

    public void SetScale(Vector3 scale)
    {
        renderPass.SetRenderObject(mesh, position, rotation, scale, material);
    }
}
