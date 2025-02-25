SAMPLER2D(uMorphOffsets, 1);

OGRE_UNIFORMS(
uniform vec3 uBlendShape;
uniform int uShapeOffsets[15];
uniform int uShapeStart;
uniform int uShapeAmounts;
uniform float uMorphWeights[15];
)


vec3 GetVertexOffset(sampler2D offsets, int shapeId, int vertexId)
{
    int shapeCoords = vertexId + uShapeStart + uShapeOffsets[shapeId];
    int xCoord = shapeCoords % 256;
    int yCoord = shapeCoords / 256;
    vec4 offset = texelFetch(offsets, ivec2(xCoord, yCoord), 0);
    offset.r = offset.r * 127.0;
    offset.g = offset.g * 127.0;
    offset.b = offset.b * 127.0;
    return vec3(offset.r * uBlendShape.x, offset.g * uBlendShape.y, offset.b * uBlendShape.z);
}

vec3 BlendVertex(vec3 position, sampler2D offsets, int vertexId, float weights[15])
{
    vec3 resultPosition = position;
    for (int i = 0; i < uShapeAmounts; i += 1)
    {
        resultPosition += GetVertexOffset(offsets, i, vertexId) * weights[i];
    }
    return resultPosition;
}