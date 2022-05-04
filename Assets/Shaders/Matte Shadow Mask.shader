// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "FX/Matte Shadow Mask" {

	Properties{
		 _Color("Main Color", Color) = (1,1,1,1)
		 _MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		 _Cutoff("Alpha cutoff", Range(0,1)) = 0.5
		_FogColor("Fog Color (RGB)", Color) = (0.5, 0.5, 0.5, 1.0)
		_FogStart("Fog Start", Float) = 0.0
		_FogEnd("Fog End", Float) = 10.0
	}


		SubShader{

		 Tags {"Queue" = "Geometry-10" "IgnoreProjector" = "True" "RenderType" = "TransparentCutout"}
		 Fog { Mode off }

		 LOD 200
		 Blend Zero SrcColor
		 Lighting Off
		 ZTest LEqual
		 ZWrite On
		 ColorMask 0
		 Pass {}

		CGPROGRAM
		#pragma surface surf ShadowOnly alphatest:_Cutoff


		fixed4 _Color;

		struct Input {

		 float2 uv_MainTex;

		};

		inline fixed4 LightingShadowOnly(SurfaceOutput s, fixed3 lightDir, fixed atten)

		{
		 fixed4 c;
		 c.rgb = s.Albedo * atten;
		 c.a = s.Alpha;
		 return c;
		}

		void surf(Input IN, inout SurfaceOutput o) {
		 fixed4 c = _Color;
		 o.Albedo = c.rgb;
		 o.Alpha = 1;
		}

#pragma surface surf Lambert vertex:vert finalcolor:fcolor

			sampler2D _MainTex;
		fixed4 _FogColor;
		float _FogStart;
		float _FogEnd;

		struct Input {
			float2 uv_MainTex;
			float fogVar;
		};

		void vert(inout appdata_full v, out Input data) {
			data.uv_MainTex = v.texcoord.xy;
			float zpos = UnityObjectToClipPos(v.vertex).z;
			data.fogVar = saturate(1.0 - (_FogEnd - zpos) / (_FogEnd - _FogStart));
		}

		void surf(Input IN, inout SurfaceOutput o) {
			half4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}

		void fcolor(Input IN, SurfaceOutput o, inout fixed4 color) {
			fixed3 fogColor = _FogColor.rgb;
#ifndef UNITY_PASS_FORWARDBASE
			fogColor = 0;
#endif
			color.rgb = lerp(color.rgb, fogColor, IN.fogVar);
		}

		ENDCG
	 }
		 Fallback "Transparent/Cutout/VertexLit"
}