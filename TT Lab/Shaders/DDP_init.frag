#version 450 core

void main()
{
	gl_FragColor.xy = vec2(-gl_FragCoord.z, gl_FragCoord.z);
}