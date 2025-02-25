#include <OgreUnifiedShader.h>

OGRE_UNIFORMS(
uniform mat4 worldViewProj;
        
#ifdef USE_LIGHTING
        
 uniform mat4 worldView;
        
#endif
)

void main(
 in vec4 iVertex : POSITION,
 in vec3 iNormal : NORMAL,
 in vec4 iColor : COLOR0,
 out vec4 gl_Position : POSITION0,
#ifdef USE_LIGHTING
 out vec4 oPosition : POSITION1,
#endif
 out vec4 oNormal : NORMAL,
 out vec4 oDiffuse : COLOR0
)
{
    vec4 resultVertex = iVertex;
    resultVertex.w = 1.0;
    oDiffuse = iColor;
    oNormal = vec4(iNormal, 1.0);
#ifdef USE_LIGHTING
    oPosition = mul(worldView, resultVertex);
#endif
    gl_Position = mul(worldViewProj, resultVertex);
}