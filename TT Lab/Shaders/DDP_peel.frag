#version 410 compatibility

in vec3 Texpos;
in vec3 Diffuse;
in vec4 Color;
in vec3 EyespaceNormal;

uniform sampler2DRect DepthBlenderTex;
uniform sampler2DRect FrontBlenderTex;

#define MAX_DEPTH 1.0

vec4 ShadeFragment(vec3 texCoord, vec4 color, vec3 diffuse, vec3 eyespaceNormal);

void main()
{
	// window-space depth interpolated linearly in screen space
	float fragDepth = gl_FragCoord.z;

	vec2 depthBlender = texelFetch(DepthBlenderTex, ivec2(gl_FragCoord.xy)).xy;
	vec4 forwardTemp = texelFetch(FrontBlenderTex, ivec2(gl_FragCoord.xy));
	
	// Depths and 1.0-alphaMult always increase
	// so we can use pass-through by default with MAX blending
	gl_FragData[0].xy = depthBlender;
	
	// Front colors always increase (DST += SRC*ALPHA_MULT)
	// so we can use pass-through by default with MAX blending
	gl_FragData[1] = forwardTemp;
	
	// Because over blending makes color increase or decrease,
	// we cannot pass-through by default.
	// Each pass, only one fragment writes a color greater than 0
	gl_FragData[2] = vec4(0.0);

	float nearestDepth = -depthBlender.x;
	float farthestDepth = depthBlender.y;
	float alphaMultiplier = 1.0 - forwardTemp.w;

	if (fragDepth < nearestDepth || fragDepth > farthestDepth) {
		// Skip this depth in the peeling algorithm
		gl_FragData[0].xy = vec2(-MAX_DEPTH);
		return;
	}
	
	if (fragDepth > nearestDepth && fragDepth < farthestDepth) {
		// This fragment needs to be peeled again
		gl_FragData[0].xy = vec2(-fragDepth, fragDepth);
		return;
	}
	
	// If we made it here, this fragment is on the peeled layer from last pass
	// therefore, we need to shade it, and make sure it is not peeled any farther
	vec4 color = ShadeFragment(Texpos, Color, Diffuse, EyespaceNormal);
	gl_FragData[0].xy = vec2(-MAX_DEPTH);
	
	if (fragDepth == nearestDepth) {
		gl_FragData[1].xyz += color.rgb * color.a * alphaMultiplier;
		gl_FragData[1].w = 1.0 - alphaMultiplier * (1.0 - color.a);
	} else {
		gl_FragData[2] += color;
	}
}