#version 450 core

layout (location = 0) in vec3 in_Position;
layout (location = 1) in vec4 in_Color;
layout (location = 2) in vec3 in_Normal;
layout (location = 3) in vec3 in_Texpos;

out vec3 Texpos;
out vec4 Color;
out vec3 Diffuse;
out vec3 EyespaceNormal;

uniform mat4 Projection;
uniform mat4 View;
uniform mat4 Model;
uniform mat3 NormalMatrix;

vec3 ShadeVertex(mat3 normalMat, vec3 vertex, vec3 normal);

void main()
{
	gl_Position = Projection * View * Model * vec4(in_Position, 1.0);
	Texpos = in_Texpos;
	Diffuse = ShadeVertex(NormalMatrix, in_Position, in_Normal);
	EyespaceNormal = NormalMatrix * in_Normal;
	Color = in_Color;
}