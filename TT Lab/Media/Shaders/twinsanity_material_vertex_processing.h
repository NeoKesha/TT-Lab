if (deform)
{
    float vy = sin(elapsedTime + float(gl_VertexID)) * deformSpeed.y * 0.1;
    float vx = sin(elapsedTime + float(gl_VertexID)) * deformSpeed.x * 0.1;
    float vz = sin(elapsedTime + float(gl_VertexID)) * deformSpeed.x * 0.1;
    resultVertex *= vec3(vx + 1.0, vy + 1.0, vz + 1.0);
}

if (billboardMode == 2)
{
    mat4 modelViewMatrix = viewMatrix * mat4(vec4(normalize(cross(vec3(0.0, 1.0, 0.0), invViewMatrix[2].xyz)), 0.0), vec4(0.0, 1.0, 0.0, 0.0), vec4(normalize(cross(invViewMatrix[0].xyz, vec3(0.0, 1.0, 0.0))), 0.0), modelMatrix[3]);
}
