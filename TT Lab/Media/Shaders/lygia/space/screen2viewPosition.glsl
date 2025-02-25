#include "../math/inverse.glsl"

/*
contributors: Patricio Gonzalez Vivo
description: derive view surface position from screen coordinates and depth
use: <vec3> screen2viewPosition( const in <vec2> screenPosition, const in <float> depth, const in <float> viewZ )
options:
    - CAMERA_PROJECTION_MATRIX: mat4 matrix with camera projection
    - INVERSE_CAMERA_PROJECTION_MATRIX: mat4 matrix with the inverse camara projection
license:
    - Copyright (c) 2021 Patricio Gonzalez Vivo under Prosperity License - https://prosperitylicense.com/versions/3.0.0
    - Copyright (c) 2021 Patricio Gonzalez Vivo under Patron License - https://lygia.xyz/license
*/

#ifndef CAMERA_PROJECTION_MATRIX
#define CAMERA_PROJECTION_MATRIX u_projectionMatrix
#endif

#ifndef INVERSE_CAMERA_PROJECTION_MATRIX
// #define INVERSE_CAMERA_PROJECTION_MATRIX u_inverseProjectionMatrix
#define INVERSE_CAMERA_PROJECTION_MATRIX inverse(CAMERA_PROJECTION_MATRIX)
#endif

#ifndef FNC_SCREEN2VIEWPOSITION
#define FNC_SCREEN2VIEWPOSITION

vec4 screen2viewPosition( const in vec2 screenPosition, const in float depth, const in float viewZ ) {
    float clipW = CAMERA_PROJECTION_MATRIX[2][3] * viewZ + CAMERA_PROJECTION_MATRIX[3][3];
    vec4 clipPosition = vec4( ( vec3(screenPosition, depth ) - 0.5 ) * 2.0, 1.0 ) * clipW;
    return INVERSE_CAMERA_PROJECTION_MATRIX * clipPosition;
}

#endif