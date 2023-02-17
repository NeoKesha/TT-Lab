#version 430

in vec3 Texpos;
in vec3 Diffuse;
in vec4 Color;
in vec3 EyespaceNormal;

layout (location = 0) out vec4 outColor;

vec4 ShadeFragment(vec3 texCoord, vec4 color, vec3 diffuse, vec3 eyespaceNormal);

void main()
{
    outColor = ShadeFragment(Texpos, Color, Diffuse, EyespaceNormal);
}