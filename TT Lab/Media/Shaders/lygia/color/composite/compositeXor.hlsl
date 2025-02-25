/*
contributors: Patricio Gonzalez Vivo, Anton Marini
description: Porter Duff Xor Compositing
use: <float4> compositeXor(<float4> src, <float4> dst)
license:
    - Copyright (c) 2021 Patricio Gonzalez Vivo under Prosperity License - https://prosperitylicense.com/versions/3.0.0
    - Copyright (c) 2021 Patricio Gonzalez Vivo under Patron License - https://lygia.xyz/license
*/

#ifndef FNC_COMPOSITE_XOR
#define FNC_COMPOSITE_XOR

float compositeXor(float src, float dst) {
    return src * (1.0 - dst) + dst * (1.0 - src);
}

float3 compositeXor(float3 srcColor, float3 dstColor, float srcAlpha, float dstAlpha) {
    return srcColor * (1.0 - dstAlpha) + dstColor * (1.0 - srcAlpha);
}

float4 compositeXor(float4 srcColor, float4 dstColor) {
    float4 result;
   
    result.rgb = compositeXor(srcColor.rgb, dstColor.rgb, srcColor.a, dstColor.a);
    result.a = compositeXor(srcColor.a, dstColor.a);

    return result;
}
#endif
