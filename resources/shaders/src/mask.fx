// Based on https://www.shadertoy.com/view/MdlXz8

#include "Common.fxh"

#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0
#define PS_SHADERMODEL ps_4_0
#endif

#define TAU 6.28318530718
#define MAX_ITER 5

float Time;
float RippleFrequency;
float RippleAmplitude;
float2 TextureSize;

float4x4 MatrixTransform;
float4x4 CameraOffset;

sampler inputTexture;

DECLARE_TEXTURE(mask, 1)
{
    AddressU =
Clamp;
    AddressV = Clamp;
    MinFilter =
Point;
    MagFilter = Point;
    MipFilter =
Point;
};

float Map(float value, float min1, float max1, float min2, float max2)
{
    // Convert the current value to a percentage
    // 0% - min1, 100% - max1
    float perc = (value - min1) / (max1 - min1);

    // Do the same operation backwards with min2 and max2
    value = perc * (max2 - min2) + min2;
    return value;
}

// THis goes out of your Vertex Shader into your Pixel Shader
struct VSOutput
{
    float4 position : SV_Position;
    float2 texCoord0 : TEXCOORD0;
    float4 worldPosition : TEXCOORD1;
    float4 color : COLOR0;
};

VSOutput SpriteVertexShader(
	float4 position : POSITION0,
	float2 texCoord0 : TEXCOORD0,
	float4 color : COLOR0)
{
    VSOutput output;
    output.position = mul(position, MatrixTransform);
    output.worldPosition = mul(position, CameraOffset);
    output.texCoord0 = texCoord0;
    output.color = color;
    return output;
}

float4 SpritePixelShader(VSOutput input) : SV_Target0
{
    float4 mask = SAMPLE_TEXTURE(mask, input.texCoord0);
    
    // Calculate the texture coordinates based on the world position
    float2 texCoord = (input.worldPosition.xy * 4) / TextureSize;

    float2 p = fmod(texCoord * TAU, TAU) - 250.0;
    float2 i = float2(p);
    float c = 1.0;
    float inten = .005;

    for (int n = 0; n < MAX_ITER; n++)
    {
        float t = (Time * 0.2) * (1.0 - (3.5 / float(n + 1)));
        i = p + float2(cos(t - i.x) + sin(t + i.y), sin(t - i.y) + cos(t + i.x));
        c += 1.0 / length(float2(p.x / (sin(i.x + t) / inten), p.y / (cos(i.y + t) / inten)));
    }
    c /= float(MAX_ITER);
    c = 1.17 - pow(c, 1.4);
    
    float wave = pow(abs(c), 8.0) - 1;
    float edge = Map(wave, -2.5, 1, -0.5, 0.5);
    
    float2 samplePosition = input.texCoord0 + edge * 0.015 * float2(2.5, 1);
    
    float4 sampled = tex2D(inputTexture, samplePosition);
    
    float edgeColor = 0.15;
    sampled = lerp(float4(edgeColor, edgeColor, edgeColor, 1), sampled, SAMPLE_TEXTURE(mask, samplePosition).r);
    
    float4 color = lerp(sampled, float4(wave, wave, wave, 1), 0.015);
    
    return lerp(float4(0, 0, 0, 0), color, mask.r);
}

technique Simple
{
    pass Pass1
    {
        PixelShader = compile PS_SHADERMODEL SpritePixelShader();
        VertexShader = compile VS_SHADERMODEL SpriteVertexShader();
    }
}
