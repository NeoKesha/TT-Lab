#version 450 core

vec3 ShadeVertex(mat3 normalMat, vec3 vertex, vec3 normal)
{
	float diffuse = abs(normalize(normalMat * normal).z);
	return vec3(vertex.xy, diffuse);
}