#version 450 core
layout (location = 0) in vec3 in_Position;
layout (location = 1) in vec4 in_Color;
layout (location = 2) in vec3 in_Normal;

uniform mat4 Projection;
uniform mat4 View;
uniform mat4 Model;
uniform mat3 NormalMatrix;
uniform vec3 DiffuseMaterial;

out vec3 EyespaceNormal;
out vec4 Color;
out vec3 Diffuse;

void main()
{
    EyespaceNormal = NormalMatrix * in_Normal;
    gl_Position = Projection * View * Model * vec4(in_Position, 1.0);
    Color = in_Color;
    Diffuse = DiffuseMaterial;
}