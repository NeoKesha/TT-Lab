#include "Includes/ShadingLibrary.frag"

layout (location = 1) out vec4 alpha;

void main()
{
    vec4 color = ShadeFragment(Texpos, Color, Diffuse, EyespaceNormal);

    outColor = color;
    alpha = vec4(1 - color.a);
}