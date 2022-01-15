Shader "Custom/Radar"
{
    Properties
    {
        [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _Scale("Scale", Float) = 1
        _SegmentsCount("SegmentsCount", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        
        pass {
            CGPROGRAM
            // Upgrade NOTE: excluded shader from DX11, OpenGL ES 2.0 because it uses unsized arrays
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #define TAU 6.28318530718
            #define PI 3.14159265358
            #define radToDeg 57.29578

            struct MeshData {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _Color;
            float _Scale = 1;
            int _SegmentsCount = 0;
            float _Segments[180];

            Interpolators vert(MeshData v) {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(Interpolators i) : SV_Target{
                float4 col = tex2D(_MainTex, i.uv);

                float index = lerp(0, _SegmentsCount, col.r);
                float segmentScale = _Segments[index] * _Scale;

                clip(segmentScale - col.g);

                return _Color;
            }
            ENDCG
        }
    }
}