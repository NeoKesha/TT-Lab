#version 450 core
in vec3 EyespaceNormal;
in float Depth;
in vec4 Color;
in vec3 Diffuse;

uniform vec3 LightPosition;
uniform vec3 LightDirection;
uniform vec3 AmbientMaterial;
uniform vec3 SpecularMaterial;

float GetWeight(float z, vec4 color);

void main()
{
    vec3 N = normalize(EyespaceNormal);
    vec3 L = normalize(LightPosition);
    vec3 E = normalize(LightDirection);
    vec3 H = normalize(L + E);
    
    float df = max(0.0, dot(N, L));
    float sf = max(0.0, dot(N, H));

    vec4 color = Color * vec4(AmbientMaterial, 1.0) + df * vec4(Diffuse, 1.0);// + sf * vec4(SpecularMaterial, 1.0);

    float weight = GetWeight(Depth, color);

    gl_FragData[0] = vec4(color.rgb * color.a, color.a) * weight;
    gl_FragData[1] = vec4(color.a);
}