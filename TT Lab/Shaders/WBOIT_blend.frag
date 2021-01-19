#version 450 core

in vec3 Texpos;
in vec4 passColor;
in float Depth;

vec4 ShadeFragment(vec3 texCoord);
float GetWeight(float z, vec4 color);

void main() {
    vec4 color = ShadeFragment(Texpos);
    float weight = GetWeight(Depth, color);

    gl_FragData[0] = vec4(color.rgb, color.a) * weight;
    gl_FragData[1] = vec4(color.a);
}