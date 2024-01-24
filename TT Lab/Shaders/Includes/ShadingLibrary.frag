// Declare the necessary fragment library shading function declarations here
// MUST BE INCLUDED IN EVERY FRAGMENT SHADER WITH main() FUNCTION

#include "Includes/ModelLayout.frag" //! #include "ModelLayout.frag"

vec4 ShadeFragment(vec3 texCoord, vec4 color, vec3 diffuse, vec3 eyespaceNormal);