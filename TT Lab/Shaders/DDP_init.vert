#version 410 compatibility
layout (location = 0) in vec3 in_Position;

uniform mat4 Projection;
uniform mat4 View;
uniform mat4 Model;
uniform mat3 NormalMatrix;

void main()
{
     gl_Position = Projection * View * Model * vec4(in_Position, 1.0);
}