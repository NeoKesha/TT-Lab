#include "Includes/ShadingLibrary.vert" //! #include "../Includes/ShadingLibrary.vert"

uniform int BillboardMode;

mat4 GetViewModelMatrix(mat4 view, mat4 model)
{
	mat4 invView = inverse(view);
	mat4 resultMatrix = view * mat4(vec4(normalize(cross(vec3(0.0, 1.0, 0.0), invView[2].xyz)), 0.0), vec4(0.0, 1.0, 0.0, 0.0), vec4(normalize(cross(invView[0].xyz, vec3(0.0, 1.0, 0.0))), 0.0), model[3]);
	float doBillboard = BillboardMode;
	float noBillboard = 1.0 - BillboardMode;
	resultMatrix *= doBillboard;
	mat4 viewModelMatrix = view * model;
	viewModelMatrix *= noBillboard;
	return resultMatrix + viewModelMatrix;
}

mat4 GetNormalMatrix(mat4 viewModel)
{
	float doBillboard = BillboardMode;
	float noBillboard = 1.0 - BillboardMode;

	return mat4(viewModel) * doBillboard + transpose(inverse(viewModel)) * noBillboard;
}

vec3 ShadeVertex(mat4 normalMat, vec3 vertex, vec3 normal)
{
	return vec3(1.0);
}