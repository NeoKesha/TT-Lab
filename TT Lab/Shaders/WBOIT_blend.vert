#version 450 core

layout (location = 0) in vec3 in_Position;
layout (location = 1) in vec4 in_Color;
layout (location = 2) in vec3 in_Normal;
layout (location = 3) in vec3 in_Texpos;

out vec3 Texpos;
out vec4 Color;
out vec3 Diffuse;
out vec3 EyespaceNormal;
out float Depth;

uniform mat4 Projection;
uniform mat4 View;
uniform mat4 Model;

vec3 ShadeVertex(mat4 normalMat, vec3 vertex, vec3 normal);

void main()
{
	mat4 viewModel = View * Model;
	gl_Position = Projection * viewModel * vec4(in_Position, 1.0);
	Texpos = in_Texpos;
	mat4 normalMat = transpose(inverse(viewModel));
	Diffuse = ShadeVertex(normalMat, in_Position, in_Normal);
	EyespaceNormal = normalize(normalMat * vec4(in_Normal, 0.0)).xyz;
	Color = in_Color;
	Depth = in_Position.z;
}