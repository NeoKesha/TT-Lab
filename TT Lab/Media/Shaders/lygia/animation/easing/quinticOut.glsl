/*
contributors: Hugh Kennedy (https://github.com/hughsk)
description: Quintic out easing. From https://github.com/stackgl/glsl-easings
use: quinticOut(<float> x)
examples:
    - https://raw.githubusercontent.com/patriciogonzalezvivo/lygia_examples/main/animation_easing.frag
*/

#ifndef FNC_QUINTICOUT
#define FNC_QUINTICOUT
float quinticOut(in float t) { return 1.0 - (pow(t - 1.0, 5.0)); }
#endif