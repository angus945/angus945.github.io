using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GPUInstanceFeature : ScriptableRendererFeature
{
    [SerializeField] ComputeShader compute;

    [Header("Render")]
    [SerializeField] [Range(1, 5000)] int instanceCount = 10;
    [SerializeField] Mesh instanceMesh;
    [SerializeField] Material material;

    GPUInstancePass pass;

    public override void Create()
    {
        pass = new GPUInstancePass(instanceCount, instanceMesh, material, compute);
    }
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(pass);
    }
    protected override void Dispose(bool disposing)
    {
        pass.Release();

        base.Dispose(disposing);
        // TODO Dispose ���եήɾ��O?
    }
}
