#version 450 core

uniform float Alpha;
uniform sampler2D tex;

vec4 ShadeFragment(vec3 texPos)
{
    vec4 color = texture(tex, texPos.st);
    color.a *= Alpha;
    return color;
    /*float weight = GetWeight(Depth, color);

    gl_FragData[0] = vec4(color.rgb, color.a) * weight;
    gl_FragData[1] = vec4(color.a);*/
}