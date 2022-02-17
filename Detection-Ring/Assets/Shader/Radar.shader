Shader "Custom/Radar"
{
    Properties
    {
        [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _Scale("Scale", Float) = 1
        _Test("Test", Float) = 1
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
            const float Tau = 6.28318530718;
            const float RevPerRad = 0.1591549430919;
            float _Test;
            Interpolators vert(MeshData v) {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(Interpolators IN) : SV_Target{
                float2 positionFromCenter = IN.uv - float2(0.5, 0.5);
                clip(0.5 - length(positionFromCenter));

                float2 atan2Coord = float2(lerp(-1, 1, IN.uv.x), lerp(-1, 1, IN.uv.y));
                float atanAngle = atan2(atan2Coord.x, atan2Coord.y) * 57.3; // angle in degrees
                if (atanAngle < 0) atanAngle = 360 + atanAngle;
                float angle = atanAngle / 360;


                float index = lerp(0, _SegmentsCount, angle);
                float strength = _Segments[index] * _Scale;
                clip(strength - length(positionFromCenter));
                return _Color;
            }
            ENDCG
        }
    }
}