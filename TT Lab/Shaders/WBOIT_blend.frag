#version 450 core

in vec3 Texpos;
in vec3 Diffuse;
in vec4 Color;
in vec3 EyespaceNormal;
in float Depth;

layout (location = 0) out vec4 outData;
layout (location = 1) out vec4 alpha;

vec4 ShadeFragment(vec3 texCoord, vec4 color, vec3 diffuse, vec3 eyespaceNormal);

void main()
{
    vec4 color = ShadeFragment(Texpos, Color, Diffuse, EyespaceNormal);

    outData = color;
    alpha = vec4(1 - color.a);
}