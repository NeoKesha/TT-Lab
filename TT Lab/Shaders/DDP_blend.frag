#version 450 core

uniform sampler2DRect TempTex;

void main()
{
	gl_FragColor = texelFetch(TempTex, ivec2(gl_FragCoord.xy));
	// for occlusion query
	if (gl_FragColor.a == 0) discard;
}
