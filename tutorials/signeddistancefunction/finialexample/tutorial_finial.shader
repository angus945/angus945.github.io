Shader "Hidden/SDF_FinialExample"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _ShapeColor("Shape", Color) = (0,0,0,1)
        _BackgroundColor("Shape", Color) = (0,0,0,1)
        
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.uv = v.uv;
                return o;
            }

            #define PI 3.14159265
            #define PI2 6.28318530718
            #define Deg2Rad 0.0174532925
            #define Rad2Deg 57.2957795

            sampler2D _MainTex;

            fixed4 _ShapeColor;
            fixed4 _BackgroundColor;

            float2 spaceNormalize(float2 uv, float scale)
            {
                return (uv - 0.5) * 2 * scale;
            }

            float noise_random(float2 uv) 
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453123);
            }
            float noise_valueNoise(float2 uv)
            {   
                float2 i = floor(uv);
                float2 f = frac(uv);

                float a = noise_random(i);
                float b = noise_random(i + float2(1.0, 0.0));
                float c = noise_random(i + float2(0.0, 1.0));
                float d = noise_random(i + float2(1.0, 1.0));

                float2 u = f*f*(3.0-2.0*f);

                return lerp(a, b, u.x) + (c - a)* u.y * (1.0 - u.x) + (d - b) * u.x * u.y;
            }

            fixed4 colorDistance(float distance)
            {
                return lerp(_ShapeColor, _BackgroundColor, saturate(distance));
            }

            float combina_add(float a, float b)
            {
                return min(a, b);
            }
            float combina_mask(float a, float b)
            {
                return max(a, b);
            }
            float combina_cull(float a, float cull)
            {
                return max(a, -cull);
            }
            float combina_fusion(float a, float b, float k)
            {
                k = saturate(k);
                float h = max(k - abs(a- b), 0) / k;
                return min(a, b) - pow(h, 3) * 1 / 6;
            }

            float2 translate(float2 uv, float2 direction)
            {
                return uv - direction;   
            }
            float2 rotate(float2 uv, float angle)
            {
                float radian = -angle * Deg2Rad;

                float2x2 rotMatrix;
                rotMatrix[0] = float2(cos(radian), -sin(radian));
                rotMatrix[1] = float2(sin(radian),  cos(radian));
                
                return mul(rotMatrix, uv);
            }
            float2 scale(float2 uv, float2 scale)
            {
                return uv / max(scale, 0.000001);
            }
            float2 mirror(float2 uv)
            {
                return abs(uv);
            }
            float2 mirror(float2 uv, float2 mirrorPoint, float2 mirrorNormal)
            {
                mirrorNormal = normalize(mirrorNormal);
                
                float2 mirrorPlane = cross(float3(mirrorNormal.xy, 0), float3(0, 0, 1));
                float2 mirrorUV = uv - mirrorPoint;
                
                float2 normalPorj = abs(dot(mirrorUV, mirrorNormal)) * mirrorNormal;
                float2 plaenPorj = dot(mirrorUV, mirrorPlane) * mirrorPlane;
                
                return normalPorj + plaenPorj;                
            }

            float2 radial (float2 uv, float amount, out float2 radialDirection)
            {
                float radian = atan2(uv.y, uv.x);
    
                radian = lerp(radian, PI2 + radian, step(radian, 0));

                float radials = PI2 / amount;
                float radial = fmod(radian, radials);

                float2 radialUV = 0;
                sincos(radial, radialUV.y, radialUV.x);    
                sincos(radials * 0.5, radialDirection.y, radialDirection.x);

                return radialUV * length(uv);
            }
            float2 radial (float2 uv, float amount)
            {
                float2 _;
                return radial(uv, amount, _);
            }
            
            float SDF_circle(float2 uv, float radius)
            {
                return length(uv) - radius;
            }
            float SDF_circle(float2 uv, float radius, float2 offset)
            {
                uv = translate(uv, offset);
                return SDF_circle(uv, radius);
            }
            float SDF_Ring(float2 uv, float radius, float thickness)
            {
                float thick = thickness * 0.5;
                float outside = SDF_circle(uv, radius + thick);
                float inSide = SDF_circle(uv, radius - thick);
                return combina_cull(outside, inSide);
            }

            float SDF_Sector(float2 uv, float radius, float degree)
            {
                float rectorRadian = degree * Deg2Rad;

                float2 uvdir = normalize(uv);
                float3 edgeDir = 0;
                sincos(rectorRadian, edgeDir.y, edgeDir.x);  
                
                uv = mirror(uv,0 , float2(0, 1));
                float projToEdge = dot(uv, edgeDir);

                float sectorDistance = length(uv) - radius;
                float originDistance = length(uv);
                float edgeDistance = length(uv - edgeDir * radius);
                float cornerDistance = abs(dot(cross(edgeDir, float3(0, 0, 1)), uv));
                
                float distance = lerp
                (
                    originDistance, lerp(cornerDistance, edgeDistance, 
                    step(radius, projToEdge)) , step(0, projToEdge)
                );
                distance = lerp(distance, sectorDistance, step(cos(rectorRadian), uvdir.x));
                
                return distance;
            }
            float SDF_RingSector(float2 uv, float radius, float thickness, float degree)
            {
                float halfThickness = thickness * 0.5;
                float sector = SDF_Sector(uv, radius + halfThickness, degree);
                float circle = SDF_circle(uv, radius - halfThickness);
                return combina_cull(sector, circle);
            }
            float SDF_RingSector(float2 uv, float radius, float thickness, float degree, float angle)
            {
                uv = rotate(uv, angle);
                return SDF_RingSector(uv, radius, thickness, degree);
            }

            float SDF_Rect(float2 uv, float2 size)
            {
                float2 dist = abs(uv) - size;

                float inside = max(dist.x, dist.y);
                float edge = length(dist);
                return lerp(inside, lerp(inside, edge, step(0, dist.x)), step(0, dist.y));
            }
            float SDF_Rect(float2 uv, float2 size, float2 offset, float angle)
            {
                uv = translate(uv, offset);
                uv = rotate(uv, angle);
                return SDF_Rect(uv, size);
            }
            float SDF_ringRectangles(float2 uv, float radius, float amount, float2 size)
            {
                float2 direction; 

                uv = radial(uv, amount, direction);
                uv = rotate(uv, 180 / amount); 
                
                return SDF_Rect(uv, size, float2(radius, 0), 0);                
            }
           

            fixed4 frag (v2f i) : SV_Target
            {


                return 0;
            }
            ENDCG
        }
    }
}
