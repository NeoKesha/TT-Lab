#include "Includes/ModelLayout.vert"

mat4 GetViewModelMatrix(mat4 view, mat4 model)
{
	return view * model;
}

mat4 GetNormalMatrix(mat4 viewModel)
{
	return transpose(inverse(viewModel));
}

vec3 ShadeVertex(mat4 normalMat, vec3 vertex, vec3 normal)
{
	return vec3(0.25, 0.2, 0.2);
}
