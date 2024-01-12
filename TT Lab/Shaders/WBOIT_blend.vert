#include "Includes/ModelLayout.vert"
#include "Includes/ShadingLibrary.vert"


void main()
{
	mat4 viewModel = View * Model;
	vec3 processedPosition = in_Position;
	processedPosition = PositionVertex(processedPosition, Morphs, gl_VertexID, MorphWeights);
	gl_Position = Projection * viewModel * vec4(processedPosition, 1.0);//vec4(in_Position, 1.0);
	Texpos = in_Texpos;
	mat4 normalMat = transpose(inverse(viewModel));
	Diffuse = ShadeVertex(normalMat, in_Position, in_Normal);
	EyespaceNormal = normalize(normalMat * vec4(in_Normal, 0.0)).xyz;
	Color = in_Color;
	Depth = in_Position.z;
}