using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ImageEffectPass : ScriptableRenderPass
{
    Material material;
    RenderTargetIdentifier sourceTexture;
    RenderTargetHandle tempTexture;

    public ImageEffectPass(Material material)
    {
        this.material = material;
        tempTexture.Init("_TempTexture");

        renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer command = CommandBufferPool.Get("Command");

        RenderTextureDescriptor cameraTargetDescriptor = renderingData.cameraData.cameraTargetDescriptor;
        cameraTargetDescriptor.depthBufferBits = 0;
        command.GetTemporaryRT(tempTexture.id, cameraTargetDescriptor, FilterMode.Bilinear);

        command.Blit(sourceTexture, tempTexture.Identifier(), material, 0);
        command.Blit(tempTexture.Identifier(), sourceTexture);

        context.ExecuteCommandBuffer(command);

        CommandBufferPool.Release(command);
    }

    public override void FrameCleanup(CommandBuffer cmd)
    {
        cmd.ReleaseTemporaryRT(tempTexture.id);
    }

    public void SetSource(RenderTargetIdentifier source)
    {
        this.sourceTexture = source;
    }

}
