#version 450 core
in vec3 Texpos;
in float Depth;

uniform sampler2D tex;

float GetWeight(float z, vec4 color);

void main()
{
    vec4 color = texture(tex, Texpos.st);
    float weight = GetWeight(Depth, color);

    gl_FragData[0] = vec4(color.rgb, color.a) * weight;
    gl_FragData[1] = vec4(color.a);
}