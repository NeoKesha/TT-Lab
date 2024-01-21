#include "Includes/ModelLayout.frag"

uniform float Alpha;
uniform vec3 LightPosition;
uniform vec3 LightDirection;
uniform vec3 AmbientMaterial;
uniform vec3 SpecularMaterial;

vec4 ShadeFragment(vec3 texCoord, vec4 col, vec3 diffuse, vec3 eyespaceNormal)
{
    vec3 N = normalize(eyespaceNormal);
    vec3 L = normalize(LightPosition);
    vec3 E = normalize(LightDirection);
    vec3 H = normalize(L + E);
    
    float df = max(0.0, dot(N, L));
    float sf = max(0.0, dot(N, H));

    vec4 color = col * vec4(AmbientMaterial, 1.0) + df * vec4(diffuse, 0.2);// + sf * vec4(SpecularMaterial, 1.0);
    color *= Alpha;

    return color;
}