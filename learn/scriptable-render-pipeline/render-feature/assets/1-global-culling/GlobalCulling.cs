using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GlobalCulling : MonoBehaviour
{
    [SerializeField] Transform cullingSphere;
    [SerializeField] bool cullIn;

    void Update()
    {
        Vector4 cullingSDF = cullingSphere.position;
        cullingSDF.w = cullingSphere.localScale.x / 2;

        Shader.SetGlobalVector("_SDFCulling", cullingSDF);
        Shader.SetGlobalFloat("_SDFCullingDir", cullIn ? 1 : -1);
    }
}
