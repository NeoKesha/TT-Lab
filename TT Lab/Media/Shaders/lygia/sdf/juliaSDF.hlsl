#include "../math/pow2.hlsl"

/*
description: |
    Returns the Juia set SDF
    For more information about the Julia set, check [this article](https://en.wikipedia.org/wiki/Julia_set)
    Some values for c:
        * float2(−0.8, 0.156)
        * float2(0.285, 0.0)
        * float2(-0.8, 0.156);
        * float2(0.27334, 0.00742)
        * float2(−0.835, −0.2321)
use: juliaSDF(<float2> st, <float2> c, <float> r)
examples:
    - https://gist.githubusercontent.com/kfahn22/246988bac1f346c3112a8ea1cd0b114d/raw/8f3a563e3c88cbbfb267a0277ba9b262a9e63570/julia.frag
*/

#ifndef FNC_JULIASDF
#define FNC_JULIASDF
float juliaSDF( float2 st, float2 c, float r) {
    st = st * 2.0 - 1.0;
    float2 z = float2(0.0) - (st) * r;
    float n = 0.0;
    const int I = 500;
    for (int i = I; i > 0; i --) { 
        if ( length(z) > 4.0 ) { 
        n = float(i)/float(I); 
        break;
        } 
        z = float2( (pow2(z.x) - pow2(z.y)) + c.x, (2.0*z.x*z.y) + c.y ); 
    } 
    return n;
}
#endif