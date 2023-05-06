using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CustomRenderPass : ScriptableRenderPass
{
    Mesh mesh;
    Matrix4x4 transform;
    Material material;

    MaterialPropertyBlock properties;

    public CustomRenderPass()
    {
        renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
        //properties = new MaterialPropertyBlock();
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer command = CommandBufferPool.Get("Command");
        
        if(mesh != null && material != null)
        {
            command.DrawMesh(mesh, transform, material, 0, 0);
        }

        context.ExecuteCommandBuffer(command);

        CommandBufferPool.Release(command);
    }
    public void SetRenderObject(Mesh mesh, Vector3 position, Vector3 rotation, Vector3 scale, Material material)
    {
        this.mesh = mesh;
        this.transform = Matrix4x4.TRS(position, Quaternion.Euler(rotation), scale);
        this.material = material;
    }
}
