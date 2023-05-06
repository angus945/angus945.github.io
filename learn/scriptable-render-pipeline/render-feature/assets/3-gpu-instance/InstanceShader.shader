Shader "Unlit/InstanceShader"
{
    Properties
    {

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

      Pass
        {           
            CGPROGRAM           
            
            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile_instancing
            #pragma target 4.5

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;

            fixed3 _ColorBlend;
            int _TexSlice;

            #if SHADER_TARGET >= 45
                StructuredBuffer<float4x4> transformBuffer;
            #endif

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD3;
                
                float3 objectPos : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
            };

            v2f vert (appdata v, uint instanceID : SV_INSTANCEID)
            {
                #if SHADER_TARGET >= 45
                    float4x4 transformMat = transformBuffer[instanceID];
                #else
                    float4x4 transformMat = 0;
                #endif
                
                float3 originPosition = mul(transformMat, float4(0, 0, 0, 1));
                float3 localPosition = mul(transformMat, v.vertex.xyz);
                float3 worldPosition = originPosition + localPosition;

                float4 originVert = mul(UNITY_MATRIX_VP, float4(originPosition, 1.0f));
                
                v2f o;
                o.vertex = mul(UNITY_MATRIX_VP, float4(worldPosition, 1.0f));
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                
                o.objectPos = originPosition;
                o.worldPos = worldPosition;
                o.normal = v.normal;
                return o;
            }

            // uniform float4 u_MouseFade;

            fixed4 frag (v2f i, uint id : SV_INSTANCEID) : SV_Target
            {
                float3 lightDir = _WorldSpaceLightPos0.xyz;
                float light = dot(lightDir, i.normal);
                
                return fixed4(light.xxx, 1);
            }
            ENDCG
        }
    }
}
