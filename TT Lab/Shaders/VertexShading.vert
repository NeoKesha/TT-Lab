#version 450 core

uniform vec3 BlendShape;
uniform int ShapeOffset[15];
uniform int ShapeStart;

vec3 GetVertexOffset(sampler2D offsets, int shapeId, int vertexId)
{
	int shapeCoords = vertexId + ShapeStart + ShapeOffset[shapeId];
	int xCoord = shapeCoords % 512;
	int yCoord = shapeCoords / 512;
	vec4 offset = texelFetch(offsets, ivec2(xCoord, yCoord), 0);
	offset.r = (offset.r - 0.5) * 127.0;
	return offset.r * BlendShape;
}

vec3 ShadeVertex(mat4 normalMat, vec3 vertex, vec3 normal)
{
	return vec3(0.25, 0.2, 0.2);
}

vec3 PositionVertex(vec3 position, sampler2D offsets, int vertexId, float weights[15])
{
	vec3 resultPosition = position;
	for (int i = 0; i < 15; i += 1)
	{
		resultPosition += GetVertexOffset(offsets, i, vertexId) * weights[i];
	}
	return resultPosition;
}