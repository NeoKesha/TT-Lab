#include "Includes/ModelLayout.frag"

uniform vec3 LightPosition;
uniform vec3 LightDirection;
uniform vec3 AmbientMaterial;
uniform vec3 SpecularMaterial;

vec4 ShadeFragment(vec3 texCoord, vec4 col, vec3 diffuse, vec3 eyespaceNormal)
{
    vec4 textureAmbient = texture(Texture[0], vec2(texCoord.s, texCoord.t));

    vec3 N = normalize(eyespaceNormal);
    vec3 L = normalize(LightPosition);
    vec3 E = normalize(LightDirection);
    vec3 H = normalize(L + E);
    
    float df = max(0.0, dot(N, L));
    float sf = max(0.0, dot(N, H));

    vec4 color = col * vec4(AmbientMaterial + textureAmbient.rgb, 1.0) + df * vec4(diffuse, 0.2);// + sf * vec4(SpecularMaterial, 1.0);
    color *= Opacity;

    return color;
}