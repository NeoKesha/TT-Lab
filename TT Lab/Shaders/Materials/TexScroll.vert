#include "Includes/ShadingLibrary.vert" //! #include "../Includes/ShadingLibrary.vert"

uniform int BillboardMode;

vec3 ShadeVertex(mat4 normalMat, vec3 vertex, vec3 normal)
{
	return vec3(1.0);
}