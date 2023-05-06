using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

class GPUInstancePass : ScriptableRenderPass
{
    [Range(0, 1000)] int instanceCount = 10;
    Mesh instanceMesh;
    Material material;

    ComputeShader compute;

    //Compute
    int kernel;
    ComputeBuffer argsBuffer;
    ComputeBuffer positionBuffer;
    ComputeBuffer resultBuffer;

    //Datas
    uint[] args = new uint[5] { 0, 0, 0, 0, 0 };

    public GPUInstancePass(int instanceCount, Mesh instanceMesh, Material material, ComputeShader compute)
    {
        this.instanceCount = instanceCount;
        this.instanceMesh = instanceMesh;
        this.material = material;
        this.compute = compute;

        CreateArgs();
        Generate();
        SetCompute();
    }
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer command = CommandBufferPool.Get("Command");

        command.SetBufferCounterValue(resultBuffer, 0);
        //resultBuffer.SetCounterValue(0);

        command.DispatchCompute(compute, kernel, (instanceCount / 640 + 1), 1, 1);
        //compute.Dispatch(kernel, (instanceCount / 640 + 1), 1, 1);

        command.CopyCounterValue(resultBuffer, argsBuffer, sizeof(uint));
        //ComputeBuffer.CopyCount(resultBuffer, argsBuffer, sizeof(uint));

        command.DrawMeshInstancedIndirect(instanceMesh, 0, material, 0, argsBuffer);
        //Graphics.DrawMeshInstancedIndirect(instanceMesh, 0, material, new Bounds(Vector3.zero, new Vector3(100.0f, 100.0f, 100.0f)), argsBuffer);

        context.ExecuteCommandBuffer(command);

        CommandBufferPool.Release(command);
    }

    void CreateArgs()
    {
        args[0] = (uint)instanceMesh.GetIndexCount(0);
        args[1] = (uint)instanceCount;
        args[2] = (uint)instanceMesh.GetIndexStart(0);
        args[3] = (uint)instanceMesh.GetBaseVertex(0);
        argsBuffer = new ComputeBuffer(1, args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
        argsBuffer.SetData(args);
    }
    void Generate()
    {
        positionBuffer = new ComputeBuffer(instanceCount, sizeof(float) * 16, ComputeBufferType.Structured);
        resultBuffer = new ComputeBuffer(instanceCount, sizeof(float) * 16, ComputeBufferType.Append);

        Matrix4x4[] matrices = new Matrix4x4[instanceCount];
        for (int i = 0; i < matrices.Length; i++)
        {
            matrices[i] = Matrix4x4.Translate(Random.insideUnitSphere * 20);
        }

        positionBuffer.SetData(matrices);
        material.SetBuffer("transformBuffer", positionBuffer);
    }
    void SetCompute()
    {
        kernel = compute.FindKernel("CSMin");
        compute.SetInt("instanceCount", instanceCount);

        compute.SetBuffer(kernel, "positionBuffer", positionBuffer);
        compute.SetBuffer(kernel, "resultBuffer", resultBuffer);
    }

    public void Release()
    {
        argsBuffer.Dispose();
        positionBuffer.Dispose();
        resultBuffer.Dispose();
    }
}