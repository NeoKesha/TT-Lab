#include "Includes/ModelLayout.frag"

vec4 ShadeFragment(vec3 texCoord, vec4 col, vec3 diffuse, vec3 eyespaceNormal)
{
    vec4 color = texture(Texture[0], vec2(texCoord.s, texCoord.t));
    color *= Opacity;
    return color;
}