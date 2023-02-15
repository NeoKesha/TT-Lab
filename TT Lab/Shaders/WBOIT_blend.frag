#version 410 compatibility

in vec3 Texpos;
in vec3 Diffuse;
in vec4 Color;
in vec3 EyespaceNormal;
in float Depth;

vec4 ShadeFragment(vec3 texCoord, vec4 color, vec3 diffuse, vec3 eyespaceNormal);
float GetWeight(float z, vec4 color);

void main()
{
    vec4 color = ShadeFragment(Texpos, Color, Diffuse, EyespaceNormal);
    float weight = GetWeight(Depth, color);

    gl_FragData[0] = vec4(color.rgb, color.a) * weight;
    gl_FragData[1] = vec4(color.a);
}