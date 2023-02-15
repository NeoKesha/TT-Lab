#version 410 compatibility

uniform float Alpha;
uniform sampler2D tex;

vec4 ShadeFragment(vec3 texCoord, vec4 col, vec3 diffuse, vec3 eyespaceNormal)
{
    vec4 color = texture(tex, texCoord.st);
    color.a *= Alpha;
    return color;
}