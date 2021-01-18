#version 450 core

uniform sampler2DRect DepthBlenderTex;
uniform sampler2DRect FrontBlenderTex;
uniform sampler2DRect BackBlenderTex;

void main(void)
{
	vec4 frontColor = texelFetch(FrontBlenderTex, ivec2(gl_FragCoord.xy));
	vec3 backColor = texelFetch(BackBlenderTex, ivec2(gl_FragCoord.xy)).rgb;
	float alphaMultiplier = 1.0 - frontColor.w;

	// front + back
	gl_FragColor.rgb = frontColor.rgb + backColor * alphaMultiplier;
	
	// front blender
	//gl_FragColor.rgb = frontColor + vec3(alphaMultiplier);
	
	// back blender
	//gl_FragColor.rgb = backColor;
}
