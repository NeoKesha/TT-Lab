#version 450 core

layout (location = 0) uniform  sampler2DMS colorTextureNT;
layout (location = 1) uniform  sampler2DMS colorTexture;
layout (location = 2) uniform  sampler2DMS alphaTexture;

out vec4 outColor;

void main()
{
    ivec2 upos = ivec2(gl_FragCoord.xy);
    vec4 cc = texelFetch(colorTexture, upos, gl_SampleID);
    vec3 sumOfColors = cc.rgb;
    float sumOfWeights = cc.a;

    vec3  colorNT = texelFetch(colorTextureNT, upos, gl_SampleID).rgb;

    if (sumOfWeights == 0)
    { outColor = vec4(colorNT, 1.0); return; }

    float alpha = 1 - texelFetch(alphaTexture, upos, gl_SampleID).r;
    colorNT = sumOfColors / sumOfWeights * alpha +
              colorNT * (1 - alpha);
    outColor = vec4(colorNT, 1.0);
}