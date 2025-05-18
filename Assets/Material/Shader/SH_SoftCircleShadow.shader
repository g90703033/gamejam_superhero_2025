Shader"Custom/SoftCircleShadow"
{
    Properties
    {
        _ShadowColor ("Shadow Color", Color) = (0,0,0,1)
        _Alpha ("Alpha", Range(0,1)) = 1
        _Softness ("Softness", Range(0.01, 1)) = 0.5
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        ZWrite
Off
        Blend
SrcAlpha OneMinusSrcAlpha

Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
#include "UnityCG.cginc"

float4 _ShadowColor;
float _Alpha;
float _Softness;

struct appdata
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
};

struct v2f
{
    float2 uv : TEXCOORD0;
    float4 vertex : SV_POSITION;
};

v2f vert(appdata v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = v.uv;
    return o;
}

fixed4 frag(v2f i) : SV_Target
{
    float2 centerUV = i.uv - 0.5;
    float dist = length(centerUV) / 0.5;

                // Fade from center (0) to edge (1)
    float fade = smoothstep(1.0 - _Softness, 1.0, dist);
    float alpha = (1.0 - fade) * _Alpha;

    return float4(_ShadowColor.rgb, _ShadowColor.a * alpha);
}
            ENDCG
        }
    }
}
