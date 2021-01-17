#version 450 core

layout (location = 0) uniform sampler2DMS colorTexture;
layout (location = 1) uniform sampler2DMS alphaTexture;

out vec4 outColor;

void main()
{
    ivec2 upos = ivec2(gl_FragCoord.xy);
    vec4 accum = texelFetch(colorTexture, upos, gl_SampleID);
    float reveal = texelFetch(alphaTexture, upos, gl_SampleID).r;
    outColor = vec4(accum.rgb / max(accum.a, 1e-5), reveal);
}