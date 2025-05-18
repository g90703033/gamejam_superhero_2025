Shader"Custom/InvisibleWithSilhouette"
{
    Properties
    {
        _RimColor("Rim Color", Color) = (1,1,1,1)
        _RimPower("Rim Power", Float) = 3.0
        _RimIntensity("Rim Intensity", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }

        Pass
        {
Name"ForwardLit"
            Tags
{"LightMode"="UniversalForward"
}

Blend SrcAlpha
OneMinusSrcAlpha
            ZWrite
Off
            Cull
Back

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

struct Attributes
{
    float4 positionOS : POSITION;
    float3 normalOS : NORMAL;
};

struct Varyings
{
    float4 positionHCS : SV_POSITION;
    float3 normalWS : TEXCOORD0;
    float3 viewDirWS : TEXCOORD1;
};

float4 _RimColor;
float _RimPower;
float _RimIntensity;

Varyings vert(Attributes IN)
{
    Varyings OUT;
    float3 positionWS = TransformObjectToWorld(IN.positionOS.xyz);
    OUT.positionHCS = TransformWorldToHClip(positionWS);
    OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);
    OUT.viewDirWS = _WorldSpaceCameraPos - positionWS;
    return OUT;
}

half4 frag(Varyings IN) : SV_Target
{
    float3 N = normalize(IN.normalWS);
    float3 V = normalize(IN.viewDirWS);
    float rim = pow(1.0 - saturate(dot(N, V)), _RimPower);
    float alpha = rim * _RimIntensity;
    float3 color = rim * _RimColor.rgb;

    return half4(color, alpha);
}
            ENDHLSL
        }

        // Shadow caster pass to allow shadows
UsePass"Universal Render Pipeline/Lit/ShadowCaster"
    }
FallBack Off
}
