﻿Shader "Robbans/WaterSurface"
{
    Properties
    {
        _Color("Color", Color) = (.34, .85, .92, 1)
        _MainTex ("Texture", 2D) = "white" {}
        _AlphaTex("AlphaMap", 2D) = "white" {}
        _AnimSpeed("AnimSpeed", Range(0,4)) = 1
        _WaveStr("WaveStrength", Range (0,0.1)) = 0.1
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
        LOD 100
         ZWrite Off
     Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _AlphaTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _AnimSpeed;
            float _WaveStr;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                
                // sample the texture
                float2 flipUV = i.uv;
                flipUV.y *= -1;
                flipUV.y += 1;

                flipUV.x += sin(flipUV.y + _Time.y * _AnimSpeed) * flipUV.y * _WaveStr;

                fixed4 col = tex2D(_MainTex, flipUV) * tex2D(_AlphaTex, i.uv).g * _Color;
                //col -= 0.5;
                //col *= 2;
                //col += 0.5;
                //col = clamp(col, 0, 1);
               // col.a = 1;
                col.a = max(max( tex2D( _AlphaTex, i.uv).g, _Color.a), max(col.r, max(col.g, col.b)));
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
                ENDCG
        }
    }
}
