Shader "Custom/EnemyAttackMiniLine"
{
    Properties
    {
        _Color("Color", Color) = (1,0,0,1)
        _RuleTex("Sprite Texture", 2D) = "white" {}
        _Alpha("Alpha", Range(0, 1)) = 0
    }

    SubShader
    {
        Tags 
        {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
        }

        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _Color;
            float _Alpha;
            sampler2D _RuleTex;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float center = 0.5;
                float dist = abs(IN.uv.y - center);

                float smooth = 1.0 - smoothstep(
                    0.0,
                    1.0,
                    dist
                );

                float4 color = _Color;
                color.w = smooth;

                float ruleAlpha = tex2D(_RuleTex, IN.uv).a;
                float saturateAlpha = saturate(ruleAlpha + (_Alpha * 2 - 1));
                color.w = smooth * saturateAlpha;

                return color;
            }
            ENDHLSL
        }
    }
}
