void BayerDither4x4_float(float3 inputColor, float2 pixelPos, float ditherStrength, out float3 outColor)
{
    int x = (int)pixelPos.x & 3;
    int y = (int)pixelPos.y & 3;

    static const float bayer[16] = {
         0,  8,  2, 10,
        12,  4, 14,  6,
         3, 11,  1,  9,
        15,  7, 13,  5
    };

    int index = y * 4 + x;

    float dither = (bayer[index] + 0.5) / 16.0 - 0.5;

    outColor = inputColor + dither * ditherStrength;
}