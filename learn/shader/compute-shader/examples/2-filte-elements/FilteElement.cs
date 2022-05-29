using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilteElement : MonoBehaviour
{
    [SerializeField] ComputeShader compute = null;

    [SerializeField] Vector2 rangeMin, rangeMax;

    [SerializeField] Vector2[] array = null;
    [SerializeField] Vector2[] result = null;

    void Start()
    {
        int kernel = compute.FindKernel("FilteKernel");

        compute.SetVector("_RangeMin", rangeMin);
        compute.SetVector("_RangeMax", rangeMax);

        ComputeBuffer sourceBuffer = new ComputeBuffer(array.Length, sizeof(float) * 2, ComputeBufferType.Structured);
        ComputeBuffer resultBuffer = new ComputeBuffer(array.Length, sizeof(float) * 2, ComputeBufferType.Append);
        sourceBuffer.SetData(array);
        //resultBuffer.SetCounterValue(0);

        compute.SetBuffer(kernel, "sourceBuffer", sourceBuffer);
        compute.SetBuffer(kernel, "resultBuffer", resultBuffer);

        compute.SetInt("_ElementCount", array.Length);

        compute.Dispatch(kernel, 1 + (array.Length / 10), 1, 1);

        result = new Vector2[array.Length];

        resultBuffer.GetData(result);
    }
}
