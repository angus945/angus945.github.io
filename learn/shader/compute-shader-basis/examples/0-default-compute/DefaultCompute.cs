using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCompute : MonoBehaviour
{
    public ComputeShader compute;
    public  RenderTexture resultTex;

    void Start()
    {
        resultTex = new RenderTexture(1024, 1024, 0, RenderTextureFormat.Default);
        resultTex.enableRandomWrite = true;
        resultTex.Create();

        int kernel = compute.FindKernel("CSMain");


        Debug.Log(kernel);
        // compute.SetTexture(kernel, "image", image);
        compute.SetTexture(kernel, "Result", resultTex);

        compute.Dispatch(kernel, 1 + (resultTex.width / 8), 1 + (resultTex.height / 8), 1);
    }
}
