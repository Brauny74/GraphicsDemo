float3 RGBtoHSV(float3 c)
{
    float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
    float4 p = lerp(float4(c.bg, K.wz),
        float4(c.gb, K.xy),
        step(c.b, c.g));
    float4 q = lerp(float4(p.xyw, c.r),
        float4(c.r, p.yzx),
        step(p.x, c.r));

    float d = q.x - min(q.w, q.y);
    float e = 1e-10;

    float h = abs(q.z + (q.w - q.y) / (6.0 * d + e));
    float s = d / (q.x + e);
    float v = q.x;

    return float3(h, s, v);
}


void ClampToPalette_float(float3 inputColor, UnityTexture2D paletteTex, UnitySamplerState paletteSampler, int paletteSize, out float3 outColor)
{
    float minDist = 1e20;
    float3 bestColor = inputColor;

    for (int i = 0; i < paletteSize; i++)
    {
        float2 uv = float2((i + 0.5) / paletteSize, 0.5);
        float3 paletteColor = RGBtoHSV(paletteTex.SampleLevel(paletteSampler, uv, 0).rgb);

        float hueDiff = abs(inputColor.x - paletteColor.x);
        hueDiff = min(hueDiff, 1.0 - hueDiff);

        float satDiff = inputColor.y - paletteColor.y;
        float valDiff = inputColor.z - paletteColor.z;

        float3 diff = float3(
            hueDiff * 1.0,   // Hue weight
            satDiff * 0.5,   // Saturation weight
            valDiff * 1.0   // Value weight
        );

        float dist = dot(diff, diff);

        if (dist < minDist)
        {
            minDist = dist;
            bestColor = paletteColor;
        }
    }

    outColor = bestColor;
}