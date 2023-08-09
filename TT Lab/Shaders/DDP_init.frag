#version 410 compatibility

void main()
{
	gl_FragColor.xy = vec2(-gl_FragCoord.z, gl_FragCoord.z);
}