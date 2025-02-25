// A reminder because this behaviour is not really documented but at least as far as HLSL
// compiler is concerned if a uniform is declared but never used for something meaningful
// then it will be optimized out by the shader compiler and if you do param binding in Ogre's scripts then Ogre will throw
// an error that the parameter wasn't found

#include <OgreUnifiedShader.h>
#ifdef USE_LIGHTING
#include "attenuation.h"
#endif

#include "alpha_test_lib.h"

SAMPLER2D(uTexture, 0);

#ifdef USE_TWIN_MATERIAL
#include "twinsanity_material_fragment_vars.h"
#endif

OGRE_UNIFORMS(
uniform vec4 uDiffuseColor;
uniform vec4 uScrollSpeedAndAlphaTest;
uniform float elapsedTime;
)

void main(
 in vec4 gl_Position : POSITION0,
#ifdef USE_LIGHTING
 in vec4 iPosition : POSITION1,
#endif
 in vec4 iNormal : NORMAL,
 in vec2 uv : TEXCOORD0,
 in vec4 iColor : COLOR0,
#if OGRE_HLSL >= 4 && defined(USE_LIGHTING)
 in float isBackFace : VFACE,
#endif
 out vec4 gl_FragColor : COLOR
)
{
    vec2 resultUvs = uv + uScrollSpeedAndAlphaTest.xy * elapsedTime;
#ifdef USE_LIGHTING
    vec3 lightVector = normalize(lightPos.xyz - iPosition.xyz);
    vec3 invertedNormal = iNormal.xyz;
#if OGRE_HLSL >= 4
    if (isBackFace < 0.0)
    {
        invertedNormal *= -1;
    }
#else
    if (!gl_FrontFacing)
    {
        invertedNormal *= -1;
    }
#endif // OGRE_HLSL
    float lDiffuse = max(dot(invertedNormal, lightVector), 0.1);
    if (lightPos.w > 0.0)
    {
        float distance = length(lightPos.xyz - iPosition.xyz);
        float falloff = attenuation(lightAttenuation.x, 0.2, distance);
        lDiffuse = max(lDiffuse * falloff, 0.0);
    }
#else
    float lDiffuse = 1.0;
    vec4 resultColor = lDiffuse * uDiffuseColor * iColor;
#endif // USE_LIGHTING
#ifdef USE_TWIN_MATERIAL
    #include "twinsanity_material_fragment_processing.h"
#endif
    resultColor = texture2D(uTexture, resultUvs.xy) * resultColor;
    if (uScrollSpeedAndAlphaTest.z > 0.0)
    {
        Alpha_Test(uScrollSpeedAndAlphaTest.w, uScrollSpeedAndAlphaTest.z, resultColor.a);
    }
    gl_FragColor = resultColor;
}
