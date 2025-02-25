#include "../math/const.glsl"
#include "../math/saturate.glsl"

// Stars deps
#include "../math/mod2.glsl"
#include "../math/rotate3dX.glsl"
#include "../math/rotate3dZ.glsl"
#include "../space/cart2polar.glsl"
#include "../color/space/k2rgb.glsl"
#include "../generative/random.glsl"

#include "ray.glsl"
#include "common/rayleigh.glsl"
#include "common/henyeyGreenstein.glsl"

/*
contributor: Patricio Gonzalez Vivo
description: |
    Rayleigh and Mie scattering atmosphere system. 
    Based on:
    - ["Accurate Atmospheric Scattering" from GPU Gems2](https://developer.nvidia.com/gpugems/GPUGems2/gpugems2_chapter16.html)
    - [Alan Zucconi's Atmospheric Scattering articles](https://www.alanzucconi.com/2017/10/10/atmospheric-scattering-6/)
    - [Dimas Leenman atmosphere.glsl](https://github.com/Dimev/atmospheric-scattering-explained)
    - [Simulating the Colors of the Sky](https://www.scratchapixel.com/lessons/procedural-generation-virtual-worlds/simulating-sky/simulating-colors-of-the-sky.html)
    - [License CC0: Stars and galaxy by mrange](https://www.shadertoy.com/view/stBcW1)
use: <vec3> atmosphere(<vec3> eye_dir, <vec3> sun_dir)
OPTIONS:
    ATMOSPHERE_ORIGIN: Defualt vec3(0.0)
    ATMOSPHERE_SUN_POWER: sun power. Default 20.0
    ATMOSPHERE_LIGHT_SAMPLES: Defualt 8 
    ATMOSPHERE_SAMPLES: Defualt 16
    ATMOSPHERE_GROUND: Example vec3( 0.37, 0.35, 0.34 )
    ATMOSPHERE_STARS_LAYERS: Example 3
    ATMOSPHERE_STARS_ELEVATION: Example u_time * 0.01
    ATMOSPHERE_STARS_AZIMUTH: Example u_time * 0.05
examples:
    - /shaders/lighting_atmosphere.frag
*/

#ifndef ATMOSPHERE_ORIGIN
#define ATMOSPHERE_ORIGIN vec3(0.0)
#endif

#ifndef ATMOSPHERE_SUN_POWER
#define ATMOSPHERE_SUN_POWER 20.0
#endif

#ifndef ATMOSPHERE_RAY
#define ATMOSPHERE_RAY vec3(55e-7, 13e-6, 22e-6)
#endif

#ifndef ATMOSPHERE_MIE
#define ATMOSPHERE_MIE vec3(21e-6)
#endif

#ifndef ATMOSPHERE_LIGHT_SAMPLES
#define ATMOSPHERE_LIGHT_SAMPLES 8
#endif

#ifndef ATMOSPHERE_SAMPLES
#define ATMOSPHERE_SAMPLES 16
#endif

#ifndef FNC_ATMOSPHERE
#define FNC_ATMOSPHERE

bool atmosphere_intersect(Ray ray, inout float t0, inout float t1) {
    vec3 L = ATMOSPHERE_ORIGIN - ray.origin; 
    float DT = dot(L, ray.direction);
    float D2 = dot(L, L) - DT * DT;

    const float R2 = 412164e8;
    if (D2 > R2) 
        return false;

    float AT = sqrt(R2 - D2);
    t0 = DT - AT;
    t1 = DT + AT;
    return true;
}

vec3 atmosphere_pos(Ray ray, float dist, float ds) {
    return ray.origin + ray.direction * (dist + ds * 0.5);
}

float atmosphere_height(Ray ray, float dist,  float ds, inout vec2 density) {
    vec3 p = atmosphere_pos(ray, dist, ds);
    float h = length(p) - 6371e3;

    #ifdef ATMOSPHERE_GROUND
    if (h <= 0.0)
        return 0.0;
    #endif

    density += exp(-h * vec2(125e-6, 833e-6)) * ds; // Rayleigh
    return h;
}

bool atmosphere_light(Ray ray, inout vec2 depth) {
    float t0 = 0.0;     // Atmosphere entry point 
    float t1 = 99999.0; // Atmosphere exit point

    #ifdef ATMOSPHERE_GROUND
    if (!atmosphere_intersect(ray, t0, t1))
        return false;
    #endif

    float dist = 0.;
    float dstep = t1 / float(ATMOSPHERE_LIGHT_SAMPLES);
    for (int i = 0; i < ATMOSPHERE_LIGHT_SAMPLES; i++) {
        if (atmosphere_height(ray, dist,  dstep, depth) <= 0.0)
            return false;

        dist += dstep;
    }

    return true;
}

vec3 atmosphere(Ray ray, vec3 sun_dir) {
    float t0 = 0.0;
    float t1 = 99999.0;

    #ifdef ATMOSPHERE_GROUND
    if (!atmosphere_intersect(ray, t0, t1))
        return vec3(0.0);
    #endif

    float dstep = t1 / float(ATMOSPHERE_SAMPLES);
    vec2 depth = vec2(0.0);

    vec3 sumR = vec3(0.0, 0.0, 0.0);
    vec3 sumM = vec3(0.0, 0.0, 0.0);
    float dist = 0.0;
    for (int i = 0; i < ATMOSPHERE_SAMPLES; i++) {
        vec2 density = vec2(0.);

        #ifdef ATMOSPHERE_GROUND
        if (atmosphere_height(ray, dist, dstep, density) <= 0.0)
            return ATMOSPHERE_GROUND * sun_dir.y;
        #else
            atmosphere_height(ray, dist, dstep, density);

        #endif

        depth += density;

        vec2 light = vec2(0.);
        if ( atmosphere_light(Ray(atmosphere_pos(ray, dist, dstep), sun_dir), light) ) {
            vec3 attn = exp(-ATMOSPHERE_RAY * (depth.x + light.x)
                            -ATMOSPHERE_MIE * (depth.y + light.y));
            sumR += density.x * attn;
            sumM += density.y * attn;
        }

        dist += dstep;
    }

    float mu = dot(ray.direction, sun_dir);
    sumR *= rayleigh(mu) * ATMOSPHERE_RAY;
    sumM *= henyeyGreenstein(mu) * ATMOSPHERE_MIE;
    vec3 color = ATMOSPHERE_SUN_POWER * (sumR + sumM);

    // Draw stars
    #ifdef ATMOSPHERE_STARS_LAYERS
    const float m = float(ATMOSPHERE_STARS_LAYERS);
    float hh = 1.0-saturate(sun_dir.y);
    hh *= hh;
    hh *= hh * hh * hh;
    vec3 dir = ray.direction;
    #ifdef ATMOSPHERE_GROUND
    hh *= step(0.0, dir.y);
    #endif

    #ifdef ATMOSPHERE_STARS_ELEVATION
    dir = rotate3dX(ATMOSPHERE_STARS_ELEVATION) * dir;
    #endif
    #ifdef ATMOSPHERE_STARS_AZIMUTH
    dir = rotate3dZ(ATMOSPHERE_STARS_AZIMUTH) * dir;
    #endif
    vec2 sp = cart2polar(dir.xzy).yz;
    for (float i = 0.0; i < m; ++i) {
        vec2 pp = sp + 0.5 * i;
        float s = i / (m-1.0);
        float dim = mix(0.05, 0.003, s) * PI;
        vec2 np = mod2(pp, dim);
        vec2 h = random2(np + 127.0 + i);
        vec2 o = -1.0+2.0*h;
        float y = sin(sp.x);
        pp += o * dim * 0.5;
        pp.y *= y;
        float l = length(pp);
    
        float h1 = fract(h.x * 1667.0);
        float h2 = fract(h.x * 1887.0);
        float h3 = fract(h.x * 2997.0);

        vec3 scol = mix(8.0 * h2, 0.25 * h2 * h2, s) * k2rgb(mix(3000.0, 22000.0, h1 * h1));
        vec3 ccol = color + exp(-(mix(6000.0, 2000.0, hh) / mix(2.0, 0.25, s)) * max(l-0.001, 0.0)) * scol * hh;
        color = h3 < y ? ccol : color;
    }

    #endif

    return color;
}

vec3 atmosphere(vec3 eye_dir, vec3 sun_dir) {
    Ray ray = Ray(vec3(0., 6371e3 + 1.0, 0.), eye_dir);
    return atmosphere(ray, sun_dir);
}

#endif