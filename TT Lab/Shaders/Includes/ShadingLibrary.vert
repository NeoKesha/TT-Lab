// Declare the necessary vertex shading function declarations here

vec3 ShadeVertex(mat4 normalMat, vec3 vertex, vec3 normal);
vec3 PositionVertex(vec3 position, sampler2D offsets, int vertexId, float weights[15]);