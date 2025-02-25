/*
contributors: Patricio Gonzalez Vivo, Anton Marini
description: Porter Duff Destination Out Compositing
use: <float4> compositeDestinationOut(<float4> src, <float4> dst)
license:
    - Copyright (c) 2021 Patricio Gonzalez Vivo under Prosperity License - https://prosperitylicense.com/versions/3.0.0
    - Copyright (c) 2021 Patricio Gonzalez Vivo under Patron License - https://lygia.xyz/license
*/

#ifndef FNC_COMPOSITE_DST_OUT
#define FNC_COMPOSITE_DST_OUT

float compositeDestinationOut(float src, float dst) {
    return dst * (1.0 - src);
}

float3 compositeDestinationOut(float3 srcColor, float3 dstColor, float srcAlpha, float dstAlpha) {
    return dstColor * (1.0 - srcAlpha);
}

float4 compositeDestinationOut(float4 srcColor, float4 dstColor)  {
    float4 result;
   
    result.rgb = compositeDestinationOut(srcColor.rgb, dstColor.rgb, srcColor.a, dstColor.a);
    result.a = compositeDestinationOut(srcColor.a, dstColor.a);

    return result;
}
#endif
