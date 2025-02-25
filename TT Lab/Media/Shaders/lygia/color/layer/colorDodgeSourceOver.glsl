#include "../blend.glsl"
#include "../composite/sourceOver.glsl"

/*
contributors: Patricio Gonzalez Vivo, Anton Marini
description: Color Dodge Blending with Porter Duff Source Over Compositing
use: <vec4> layerColorDodgeSourceOver(<vec4> src, <vec4> dst)
license:
    - Copyright (c) 2021 Patricio Gonzalez Vivo under Prosperity License - https://prosperitylicense.com/versions/3.0.0
    - Copyright (c) 2021 Patricio Gonzalez Vivo under Patron License - https://lygia.xyz/license
*/

#ifndef FNC_LAYER_COLOR_DODGE_SRC_OVER
#define FNC_LAYER_COLOR_DODGE_SRC_OVER

vec4 layerColorDodgeSourceOver(vec4 src, vec4 dest) {
    vec4 result = vec4(0.0, 0.0, 0.0, 0.0);

    // Compute color dodge for RGB channels
    vec3 blendedColor = blendColorDodge(src.rgb, dest.rgb);

    // Compute source-over for RGB channels
    result.rgb = compositeSourceOver(blendedColor, dest.rgb, src.a, dest.a);

    // Compute source-over for the alpha channel
    result.a = compositeSourceOver(src.a, dest.a);

    return result;
}
#endif
