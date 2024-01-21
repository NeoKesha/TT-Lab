// Add any changes or additions you want for the model's format

#version 450 core

const int MAX_BLENDS = 15;

layout (location = 0) in vec3 in_Position;
layout (location = 1) in vec4 in_Color;
layout (location = 2) in vec3 in_Normal;
layout (location = 3) in vec3 in_Texpos;


out vec3 Texpos;
out vec4 Color;
out vec3 Diffuse;
out vec3 EyespaceNormal;
out mat4 Projection;
out mat4 View;
out mat4 Model;
out float Depth;

#include "Includes/GlobalUniformsDeclaration.glsl" //! #include "GlobalUniformsDeclaration.glsl"
// Vertex shader specific uniforms
uniform mat4 StartProjection;
uniform mat4 StartView;
uniform mat4 StartModel;
uniform vec3 BlendShape;
uniform int BlendShapesAmount;
uniform int ShapeOffset[MAX_BLENDS];
uniform int ShapeStart;
uniform float MorphWeights[MAX_BLENDS];
layout (binding = 2) uniform sampler2D Morphs;