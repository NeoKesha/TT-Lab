#include "digits.hlsl"

/*
contributors: Patricio Gonzalez Vivo
description: |
    Draws all the digits of a matrix, useful for debugging.
use: <float4> matrix(<float2> st, <float2x2|float3x3|float4x4> M)
options:
    DIGITS_DECIMALS: number of decimals after the point, defaults to 2
    DIGITS_SIZE: size of the font, defaults to float2(.025)
license:
    - Copyright (c) 2021 Patricio Gonzalez Vivo under Prosperity License - https://prosperitylicense.com/versions/3.0.0
    - Copyright (c) 2021 Patricio Gonzalez Vivo under Patron License - https://lygia.xyz/license
*/

#ifndef FNC_DRAW_MATRIX
#define FNC_DRAW_MATRIX

float4 matrix(in float2 st, in float2x2 M) {
    float4 rta = float4(0.0, 0.0, 0.0, 0.0);
    float2 size = DIGITS_SIZE * abs(DIGITS_VALUE_OFFSET) * 2.0;
    rta.a = 0.5 * step(-DIGITS_SIZE.x, st.x) * step(st.x, size.x) * step(-DIGITS_SIZE.y, st.y) * step(st.y, size.y);
    rta += digits(st, M);
    return rta;
}

float4 matrix(in float2 st, in float3x3 M) {
    float4 rta = float4(0.0, 0.0, 0.0, 0.0);
    float2 size = DIGITS_SIZE * abs(DIGITS_VALUE_OFFSET) * 3.0;
    rta.a = 0.5 * step(-DIGITS_SIZE.x, st.x) * step(st.x, size.x) * step(-DIGITS_SIZE.y, st.y) * step(st.y, size.y);
    rta += digits(st, M);
    return rta;
}

float4 matrix(in float2 st, in float4x4 M) {
    float4 rta = float4(0.0, 0.0, 0.0, 0.0);
    float2 size = DIGITS_SIZE * abs(DIGITS_VALUE_OFFSET) * 4.0;
    rta.a = 0.5 * step(-DIGITS_SIZE.x, st.x) * step(st.x, size.x) * step(-DIGITS_SIZE.y, st.y) * step(st.y, size.y);
    rta += digits(st, M);
    return rta;
}

#endif