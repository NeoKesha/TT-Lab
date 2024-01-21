#include "Includes/ShadingLibrary.vert"

void main()
{
	mat4 viewModel = GetViewModelMatrix(StartView, StartModel);
	vec3 processedPosition = in_Position;
	processedPosition = PositionVertex(processedPosition, Morphs, gl_VertexID, MorphWeights);
	gl_Position = StartProjection * viewModel * vec4(processedPosition, 1.0);
	Projection = StartProjection;
	View = StartView;
	Model = StartModel;
	Texpos = in_Texpos;
	mat4 normalMat = GetNormalMatrix(viewModel);
	Diffuse = ShadeVertex(normalMat, in_Position, in_Normal);
	EyespaceNormal = normalize(normalMat * vec4(in_Normal, 0.0)).xyz;
	Color = in_Color;
	Depth = in_Position.z;
}