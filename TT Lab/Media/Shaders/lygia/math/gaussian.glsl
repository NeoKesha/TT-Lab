/*
contributors: Patricio Gonzalez Vivo
description: gaussian coeficient
use: <vec4|vec3|vec2|float> gaussian(<float> sigma, <vec4|vec3|vec2|float> d)
examples:
    - https://raw.githubusercontent.com/patriciogonzalezvivo/lygia_examples/main/math_gaussian.frag
license:
    - Copyright (c) 2021 Patricio Gonzalez Vivo under Prosperity License - https://prosperitylicense.com/versions/3.0.0
    - Copyright (c) 2021 Patricio Gonzalez Vivo under Patron License - https://lygia.xyz/license
*/

#ifndef FNC_GAUSSIAN
#define FNC_GAUSSIAN
float gaussian(float d, float s) { return exp(-(d*d) / (2.0 * s*s)); }
float gaussian( vec2 d, float s) { return exp(-( d.x*d.x + d.y*d.y) / (2.0 * s*s)); }
float gaussian( vec3 d, float s) { return exp(-( d.x*d.x + d.y*d.y + d.z*d.z ) / (2.0 * s*s)); }
float gaussian( vec4 d, float s) { return exp(-( d.x*d.x + d.y*d.y + d.z*d.z + d.w*d.w ) / (2.0 * s*s)); }
#endif