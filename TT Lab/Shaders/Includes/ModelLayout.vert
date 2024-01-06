// Add any changes or additions you want for the model's format

#version 450 core

const int MAX_BLENDS = 15;

layout (location = 0) in vec3 in_Position;
layout (location = 1) in vec4 in_Color;
layout (location = 2) in vec3 in_Normal;
layout (location = 3) in vec3 in_Texpos;
layout (location = 4) in vec3 in_Offset;

out vec3 Texpos;
out vec4 Color;
out vec3 Diffuse;
out vec3 EyespaceNormal;
out float Depth;

uniform mat4 Projection;
uniform mat4 View;
uniform mat4 Model;
uniform float MorphWeight;