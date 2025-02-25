#include "const.glsl"

/*
contributors: Alexander Griffis
description: |
    The range here in degrees is 0 to pi (0-180 degrees) and -pi to 0 (181 to 359 degrees
    More about it at https://github.com/Yaazarai/GLSL-ATAN2-DOC

use: <float> atan2(<float>y, <float> x)
*/

#ifndef FNC_ATAN2
#define FNC_ATAN2
float atan2(float y, float x) { return mod(atan(y,x) + PI, TAU); }
#endif