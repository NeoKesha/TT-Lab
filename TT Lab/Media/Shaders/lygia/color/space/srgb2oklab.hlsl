#include "srgb2rgb.hlsl"
#include "rgb2oklab.hlsl"

/*
contributors: Bjorn Ottosson (@bjornornorn)
description: sRGB to OKLab https://bottosson.github.io/posts/oklab/
use: <float3\float4> srgb2oklab(<float3|float4> srgb)
license: 
    - MIT License (MIT) Copyright (c) 2020 Björn Ottosson
*/

#ifndef FNC_SRGB2OKLAB
#define FNC_SRGB2OKLAB
float3 srgb2oklab(const in float3 srgb) { return rgb2oklab( srgb2rgb(srgb) ); }
float4 srgb2oklab(const in float4 srgb) { return float4(srgb2oklab(srgb.rgb), srgb.a); }
#endif
