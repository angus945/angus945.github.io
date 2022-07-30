Shader "Unlit/VariantExampleShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        
        [KeywordEnum(None, DEFAULT, WOBBLE)] _Outline ("Outline Mode", Float) = 0 
        _OutlineWidth ("Outline Width", Range(0, 1)) = 0

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            Cull Front

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #pragma shader_feature __ _OUTLINE_DEFAULT _OUTLINE_WOBBLE
            
            #include "UnityCG.cginc"

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _OutlineWidth;

            v2f vert (appdata_base v)
            {
                v2f o;

                #ifdef _OUTLINE_DEFAULT
                float4 offset = v.vertex * _OutlineWidth;
                o.vertex = UnityObjectToClipPos(v.vertex + offset);

                #elif _OUTLINE_WOBBLE
                float4 offset = v.vertex * (_OutlineWidth + sin(_Time.y + v.vertex.x * v.vertex.y) * 0.1 );
                o.vertex = UnityObjectToClipPos(v.vertex + offset);

                #else
                o.vertex = UnityObjectToClipPos(v.vertex);
                #endif
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return 0;
            }
            ENDCG
        }

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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                return col;
            }
            ENDCG
        }
    }
}
