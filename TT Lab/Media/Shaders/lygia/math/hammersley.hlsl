# include "const.hlsl"

/*
contributors: Holger Dammertz
description: Return a Hammersley point
use: hammersley(uint index)
license: CC BY 3.0 Copyright (c) 2012 Holger Dammertz
*/

#ifndef FNC_HAMMERSLEY
#define FNC_HAMMERSLEY

float2 hammersley(uint index, int numSamples) {
    const float tof = 0.5 / float(0x80000000U);
    uint bits = index;
    bits = (bits << 16u) | (bits >> 16u);
    bits = ((bits & 0x55555555u) << 1u) | ((bits & 0xAAAAAAAAu) >> 1u);
    bits = ((bits & 0x33333333u) << 2u) | ((bits & 0xCCCCCCCCu) >> 2u);
    bits = ((bits & 0x0F0F0F0Fu) << 4u) | ((bits & 0xF0F0F0F0u) >> 4u);
    bits = ((bits & 0x00FF00FFu) << 8u) | ((bits & 0xFF00FF00u) >> 8u);
    return float2(float(index) / numSamples, float(bits) * tof);
}

float3 hemisphereCosSample(float2 u) {
    float phi = 2.0 * PI * u.x;
    float cosTheta2 = 1.0 - u.y;
    float cosTheta = sqrt(cosTheta2);
    float sinTheta = sqrt(1.0 - cosTheta2);
    return float3(cos(phi) * sinTheta, sin(phi) * sinTheta, cosTheta);
}

#endif