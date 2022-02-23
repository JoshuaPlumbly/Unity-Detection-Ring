Shader "Custom/Billboard"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ScaleFactor ("Scale Factor", Range(0,1)) = 0
        _ReferenceDistance("Reference Distance", Float) = 10
        _UseVertexColor("Use Vertex Color", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent+500"}
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 color : COLOR0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _ScaleFactor;
            float _ReferenceDistance;
            float _UseVertexColor;

            v2f vert (appdata v)
            {
                v2f o;

                float4 world_origin = mul(UNITY_MATRIX_M, float4(0, 0, 0, 1));
                float4 view_origin = float4(UnityObjectToViewPos(float3(0, 0, 0)), 1);

                float scale = (_ScaleFactor * (length(view_origin) / _ReferenceDistance)) + ((1 - _ScaleFactor));
                float4 world_pos = mul(UNITY_MATRIX_M, float4(scale, scale, scale, 1) * v.vertex);
                float4 flipped_world_pos = float4(-1, 1, -1, 1) * (world_pos - world_origin) + world_origin;
                float4 view_pos = flipped_world_pos - world_origin + view_origin;
                float4 clip_pos = mul(UNITY_MATRIX_P, view_pos);

                o.vertex = clip_pos;

                o.color = v.color;

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                float4 col = i.color;
                fixed4 tex = tex2D(_MainTex, i.uv);
                col.a = tex.a;

                col.rgb = (col.rgb * _UseVertexColor) + (tex.rgb * (1 - _UseVertexColor));

                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
