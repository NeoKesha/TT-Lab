#version 410 compatibility

in vec3 Texpos;
in vec3 Diffuse;
in vec4 Color;
in vec3 EyespaceNormal;

vec4 ShadeFragment(vec3 texCoord, vec4 color, vec3 diffuse, vec3 eyespaceNormal);

void main()
{
    gl_FragColor = ShadeFragment(Texpos, Color, Diffuse, EyespaceNormal);
}