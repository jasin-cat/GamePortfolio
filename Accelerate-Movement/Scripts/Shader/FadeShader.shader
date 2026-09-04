Shader "Custom/FadeShader"  //ここの名前を変えるのを忘れずに！
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "brack" {}
		_RuleTex("Sprite Texture", 2D) = "brack" {}
		_Color("Tint", Color) = (0,0,0,1)
		_Alpha ("Time", Range(0, 1)) = 0
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest[unity_GUIZTestMode]
		Fog{ Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
# pragma vertex vert
# pragma fragment frag
# include "UnityCG.cginc"

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				half2 texcoord  : TEXCOORD0;
			};

			fixed4 _Color;
			fixed _Alpha;
			sampler2D _MainTex;
			sampler2D _RuleTex;

			// 頂点シェーダーの基本
			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
# ifdef UNITY_HALF_TEXEL_OFFSET
				OUT.vertex.xy += (_ScreenParams.zw - 1.0) * float2(-1,1);
# endif
				return OUT;
			}

			// 通常のフラグメントシェーダー
			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 color = tex2D(_MainTex, IN.texcoord);
				half ruleAlpha = tex2D(_RuleTex, IN.texcoord).a;
				half alpha = saturate(ruleAlpha + (_Alpha * 2 - 1));
				return fixed4(
					_Color.r,
					_Color.g,
					_Color.b,
					color.a * alpha
				);
			}
			ENDCG
		}
	}

	FallBack "UI/Default"
}

