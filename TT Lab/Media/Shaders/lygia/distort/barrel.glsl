#include "../math/lengthSq.glsl"
#include "../sampler.glsl"

/*
contributors: Patricio Gonzalez Vivo
description: Barrel distortion
use: barrel(SAMPLER_TYPE tex, <vec2> st [, <vec2|float> sdf])
options:
    - SAMPLER_FNC(TEX, UV): optional depending the target version of GLSL (texture2D(...) or texture(...))
    - BARREL_DISTANCE: function used to shape the distortion, defaults to radial shape with lengthSq
    - BARREL_TYPE: return type, defaults to vec3
    - BARREL_SAMPLER_FNC: function used to sample the input texture, defaults to texture2D(TEX, UV).rgb
    - BARREL_OCT_1: one octave of distortion
    - BARREL_OCT_2: two octaves of distortion
    - BARREL_OCT_3: three octaves of distortion
examples:
    - https://raw.githubusercontent.com/eduardfossas/lygia-study-examples/main/distort/barrel.frag
license:
    - Copyright (c) 2021 Patricio Gonzalez Vivo under Prosperity License - https://prosperitylicense.com/versions/3.0.0
    - Copyright (c) 2021 Patricio Gonzalez Vivo under Patron License - https://lygia.xyz/license
*/

#ifndef BARREL_DISTANCE
#define BARREL_DISTANCE dist
#endif

#ifndef BARREL_TYPE
#define BARREL_TYPE vec3
#endif

#ifndef BARREL_SAMPLER_FNC
#define BARREL_SAMPLER_FNC(TEX, UV) SAMPLER_FNC(TEX, UV).rgb
#endif

#ifndef FNC_BARREL
#define FNC_BARREL
vec2 barrel(vec2 st, float amt, float dist) {
    return st + (st-.5) * (BARREL_DISTANCE) * amt;
}

vec2 barrel(vec2 st, float amt) {
    return barrel(st, amt, lengthSq(st-.5));
}

BARREL_TYPE barrel(in SAMPLER_TYPE tex, in vec2 st, float dist) {
    BARREL_TYPE a1 = BARREL_SAMPLER_FNC(tex, barrel(st, .0, dist));
    BARREL_TYPE a2 = BARREL_SAMPLER_FNC(tex, barrel(st, .2, dist));
    BARREL_TYPE a3 = BARREL_SAMPLER_FNC(tex, barrel(st, .4, dist));
    BARREL_TYPE a4 = BARREL_SAMPLER_FNC(tex, barrel(st, .6, dist));
#ifdef BARREL_OCT_1
    return (a1+a2+a3+a4)/4.;
#endif
    BARREL_TYPE a5 = BARREL_SAMPLER_FNC(tex, barrel(st, .8, dist));
    BARREL_TYPE a6 = BARREL_SAMPLER_FNC(tex, barrel(st, 1.0, dist));
    BARREL_TYPE a7 = BARREL_SAMPLER_FNC(tex, barrel(st, 1.2, dist));
    BARREL_TYPE a8 = BARREL_SAMPLER_FNC(tex, barrel(st, 1.4, dist));
#ifdef BARREL_OCT_2
    return (a1+a2+a3+a4+a5+a6+a7+a8)/8.;
#endif
    BARREL_TYPE a9 = BARREL_SAMPLER_FNC(tex, barrel(st, 1.6, dist));
    BARREL_TYPE a10 = BARREL_SAMPLER_FNC(tex, barrel(st, 1.8, dist));
    BARREL_TYPE a11 = BARREL_SAMPLER_FNC(tex, barrel(st, 2.0, dist));
    BARREL_TYPE a12 = BARREL_SAMPLER_FNC(tex, barrel(st, 2.2, dist));
    return (a1+a2+a3+a4+a5+a6+a7+a8+a9+a10+a11+a12)/12.;
}

BARREL_TYPE barrel(in SAMPLER_TYPE tex, in vec2 st, in vec2 dist) {
    return barrel(tex, st, dot(vec2(.5), dist));
}

BARREL_TYPE barrel(in SAMPLER_TYPE tex, in vec2 st) {
    return barrel(tex, st, lengthSq(st-.5));
}

#endif
