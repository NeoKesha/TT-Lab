// Add any changes or additions you want for the model's format

#version 450 core

const int MAX_TEXTURES = 5;

in vec3 Texpos;
in vec3 Diffuse;
in vec4 Color;
in vec3 EyespaceNormal;
in mat4 Projection;
in mat4 View;
in mat4 Model;
in float Depth;

layout (location = 0) out vec4 outColor;

#include "Includes/GlobalUniformsDeclaration.glsl" //! #include "GlobalUniformsDeclaration.glsl"
// Fragment shader specific uniforms
uniform float Opacity = 1.0;
layout (binding = 0) uniform sampler2D Texture[MAX_TEXTURES];
layout (binding = 5) uniform sampler2D Screen;