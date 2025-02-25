/*
contributors: Bjorn Ottosson (@bjornornorn)
description: |
    Linear rgb to OKLab https://bottosson.github.io/posts/oklab/
use: <float3\float4> rgb2oklab(<float3|float4> rgb)
license: 
    - MIT License (MIT) Copyright (c) 2020 Björn Ottosson
*/

#ifndef MAT_RGB2OKLAB
#define MAT_RGB2OKLAB
static const float3x3 RGB2OKLAB_A = float3x3(
    0.2104542553, 1.9779984951, 0.0259040371,
    0.7936177850, -2.4285922050, 0.7827717662,
    -0.0040720468, 0.4505937099, -0.8086757660);

static const float3x3 RGB2OKLAB_B = float3x3(
    0.4122214708, 0.2119034982, 0.0883024619,
    0.5363325363, 0.6806995451, 0.2817188376,
    0.0514459929, 0.1073969566, 0.6299787005);
#endif

#ifndef FNC_RGB2OKLAB
#define FNC_RGB2OKLAB
float3 rgb2oklab(float3 rgb) {
    float3 lms = mul(RGB2OKLAB_B, rgb);
    return mul(RGB2OKLAB_A, sign(lms) * pow(abs(lms), float3(0.3333333333333, 0.3333333333333, 0.3333333333333)));

}
float4 rgb2oklab(float4 rgb) { return float4(rgb2oklab(rgb.rgb), rgb.a); }
#endif
