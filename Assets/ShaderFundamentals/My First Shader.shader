// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/My First Shader" {
    Properties {
	    _Tint ("Tint", Color) = (1,1,1,1)
	    _MainTex("MainTex", 2D) = "White" {}
	}
    SubShader {
        

		Pass 
		{
            CGPROGRAM
			#pragma vertex MyVertexProgram
			#pragma fragment MyFragmentProgram
			#include "UnityCG.cginc"
			
			float4 _Tint;
			sampler2D _MainTex;
			//_ST 表示缩放，平移或者类似的操作   xy 表示缩放，zw表示平移
		    //TRANSFORM_TEX（v.uv，_MainTex 
			float4 _MainTex_ST;
			struct Interpolators {
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
			};
			struct VertexData {
				float4 position : POSITION;
				float2 uv : TEXCOORD0;
			};
			
			Interpolators MyVertexProgram (
				VertexData v
			) {
			    Interpolators i;
                //投影到摄像机上
				i.position = UnityObjectToClipPos(v.position);
				i.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return i;
			}

			float4 MyFragmentProgram (
				Interpolators i
			) : SV_TARGET {
				return tex2D(_MainTex, i.uv) * _Tint;
			}
			
			ENDCG
		}
	}
}


