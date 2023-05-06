Shader "Custom/LambertShader"
{
    Properties
    {
        _Color ("Tint", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
            };

            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                o.normal = v.normal;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

                float lightIntensity = dot(_WorldSpaceLightPos0.xyz, i.normal);
                float3 albedo = _Color * (lightIntensity + 1) / 2;

                return fixed4(albedo, _Color.a);
            }
            ENDCG
        }
    }
}
