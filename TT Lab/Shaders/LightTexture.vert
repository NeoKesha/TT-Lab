#version 130
in vec3 in_Position;
in vec4 in_Color;
in vec3 in_Normal;
in vec3 in_Texpos;

uniform mat4 Projection;
uniform mat4 View;
uniform mat4 Model;
uniform mat3 NormalMatrix;
uniform vec3 DiffuseMaterial;

out vec3 EyespaceNormal;
out vec4 Color;
out vec3 Diffuse;
out vec3 Texpos;

void main()
{
    EyespaceNormal = NormalMatrix * in_Normal;
    gl_Position = Projection * Model * View * vec4(in_Position, 1.0);
    Color = in_Color;
    Diffuse = DiffuseMaterial;
    Texpos = in_Texpos;
}