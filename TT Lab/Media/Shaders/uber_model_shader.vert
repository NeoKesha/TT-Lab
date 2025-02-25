#include <OgreUnifiedShader.h>

#ifdef USE_MORPHING
#include "vertex_morphing_declaration.h"
#endif

#ifdef USE_TWIN_MATERIAL
#include "twinsanity_material_vertex_vars.h"
#endif

OGRE_UNIFORMS(
uniform mat4 viewMatrix;
uniform mat4 worldMatrix;
uniform mat4 projMatrix;

uniform vec4 deformSpeed;
uniform vec4 shaderSettings;
uniform float elapsedTime;
        
#ifdef USE_LIGHTING
 uniform mat4 worldView;  
#endif

#ifdef USE_TWIN_MATERIAL
 uniform mat4 modelMatrix;
 uniform mat4 invViewMatrix;
#endif
)

void main(
 in vec4 iVertex : POSITION, // 4th component contains vertex ID
 in vec3 iNormal : NORMAL,
 in vec4 iColor : COLOR0,
 in vec2 iUv : TEXCOORD0,

 out vec4 gl_Position : POSITION0,
#ifdef USE_LIGHTING
 out vec4 oPosition : POSITION1,
#endif
 out vec4 oNormal : NORMAL,
 out vec2 oUv : TEXCOORD0,
 out vec4 oColor : COLOR0
)
{
#if OGRE_HLSL >= 4
    uint gl_VertexID = uint(iVertex.w);
#endif
    vec4 resultVertex = iVertex;
    resultVertex.w = 1.0;
    vec2 resultUv = iUv;
    oUv = resultUv;
    oColor = iColor;
    oNormal = vec4(iNormal, 1.0);
#ifdef USE_TWIN_MATERIAL
#endif
#ifdef USE_LIGHTING
    oPosition = mul(worldView, resultVertex);
#endif
#ifdef USE_MORPHING
    #include "vertex_morphing_processing.h"
#endif
    if (deformSpeed.z > 0.0f)
    {
        float vy = sin(elapsedTime + resultVertex.y) * deformSpeed.y * 0.1;
        float vx = sin(elapsedTime + resultVertex.y) * deformSpeed.x * 0.1;
        float vz = sin(elapsedTime + resultVertex.y) * deformSpeed.x * 0.1;
        resultVertex.xyz *= vec3(vx + 1.0, vy + 1.0, vz + 1.0);
    }
    if (shaderSettings.x > 0.0f) {
        resultVertex.x = -resultVertex.x;
    }
    resultVertex = mul(worldMatrix, resultVertex);
    if (shaderSettings.x > 0.0f) {
        resultVertex.x = -resultVertex.x;
    }
    gl_Position = mul(projMatrix, mul(viewMatrix, resultVertex));
}