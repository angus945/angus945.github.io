using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddValues : MonoBehaviour
{
    [SerializeField] ComputeShader compute = null;
    [SerializeField] int addition;
    [SerializeField] int[] array = null;
    [SerializeField] int[] result = null;

    void Start()
    {
        compute.SetInt("_Addition", addition);

        int kernel = compute.FindKernel("AddValueKernel");

        ComputeBuffer buffer = new ComputeBuffer(array.Length, sizeof(int), ComputeBufferType.Structured);
        buffer.SetData(array);

        compute.SetBuffer(kernel, "valuesBuffer", buffer);
        compute.Dispatch(kernel, Mathf.CeilToInt(array.Length / 10f), 1, 1);

        result = new int[array.Length];
        buffer.GetData(result);
    }
}
