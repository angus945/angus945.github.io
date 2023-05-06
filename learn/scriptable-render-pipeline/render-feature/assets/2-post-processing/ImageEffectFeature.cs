using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ImageEffectFeature : ScriptableRendererFeature
{
    [SerializeField] string shaderName;

    ImageEffectPass pass;

    public override void Create()
    {
        Material material = new Material(Shader.Find(shaderName));
        pass = new ImageEffectPass(material);
    }
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        pass.SetSource(renderer.cameraColorTarget);

        renderer.EnqueuePass(pass);
    }
}
