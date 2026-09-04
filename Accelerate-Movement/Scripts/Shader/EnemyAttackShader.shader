Shader "Custom/EnemyAttackShader"
{
    Properties
    {
        _OutLineBeginRange("OutLineRange", Range(0, 1)) = 0.7 
        _RuleTex("Sprite Texture", 2D) = "White" {}
        _Alpha("Alpha", Range(0, 1)) = 0
        _Rotation("Rotation", Float) = 0
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

            float _OutLineBeginRange;
            float _Alpha;
            float _IsRot;
            float _Rotation;
            sampler2D _RuleTex;

            // 回転
            float2 RotationUV(float2 uv, float angle, float center)
            {
                uv -= center;
                float s = sin(angle);
                float c = cos(angle);

                float2x2 rotation = 
                    float2x2(
                        c, -s,
                        s, c);

                uv = mul(rotation, uv);

                return uv + center;
            }

            Varyings vert(Attributes IN)
            {
                Varyings OUT;

                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;

                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float2 uv = RotationUV(
                    IN.uv,
                    radians(_Rotation),
                    float2(0.5, 0.5)
                );

                float center = 0.5;
                float dist = abs(uv.y - center);

                // 内側のグラデーション
                float innerGradient = smoothstep(
                    0.0,
                    _OutLineBeginRange,
                    dist
                );

                // アウトライン
                // 中心
                float outLineCenter = _OutLineBeginRange;
                // 幅
                float outLineWidth = 0.05;
                // 距離
                float outLineDist = abs(
                    dist - outLineCenter
                );

                float outLineSmooth = 1.0 - smoothstep(
                    0.0,
                    outLineWidth,
                    outLineDist
                );

                float alpha = max(innerGradient, outLineSmooth);
                float3 color = lerp(
                    float3(1,1,1),
                    float3(0.8,0,0),
                    alpha
                );

                float ruleAlpha = tex2D(_RuleTex, uv).a;
                float saturateAlpha = saturate(ruleAlpha + (_Alpha * 2 - 1));
                float totalAlpha = 0.7 * saturateAlpha;

                return half4(color, totalAlpha);
            }

            ENDHLSL
        }
    }
}
