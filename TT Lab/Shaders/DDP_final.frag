#version 410 compatibility

uniform sampler2DRect DepthBlenderTex;
uniform sampler2DRect FrontBlenderTex;
uniform sampler2DRect BackBlenderTex;

void main()
{
	vec4 frontColor = texelFetch(FrontBlenderTex, ivec2(gl_FragCoord.xy));
	vec3 backColor = texelFetch(BackBlenderTex, ivec2(gl_FragCoord.xy)).rgb;
	float alphaMultiplier = 1.0 - frontColor.w;

	gl_FragColor.rgb = frontColor.rgb + backColor * alphaMultiplier;
}
