#version 450 core

vec3 ShadeVertex(mat4 normalMat, vec3 vertex, vec3 normal)
{
	return vec3(0.25, 0.2, 0.2);
}

vec3 PositionVertex(vec3 position, vec3 offset, float weight)
{
	vec3 resultPosition = position + offset * weight;
	return resultPosition;
}