#version 450 core

uniform float Alpha;

#define COLOR_FREQ 30.0
#define ALPHA_FREQ 30.0

vec4 ShadeFragment(vec3 texCoord, vec4 col, vec3 diffuse, vec3 eyespaceNormal)
{
	float xWorldPos = texCoord.x;
	float yWorldPos = texCoord.y;

	vec4 color;
	float i = floor(xWorldPos * COLOR_FREQ);
	float j = floor(yWorldPos * ALPHA_FREQ);
	color.rgb = (mod(i, 2.0) == 0) ? vec3(.4,.85,.0) : vec3(1.0);
	color.a = Alpha;

	color.rgb *= diffuse;
	return color;
}