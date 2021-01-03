#version 130
in vec3 EyespaceNormal;
in vec4 Color;
in vec3 Diffuse;
out vec4 FragColor;

uniform vec3 LightPosition;
uniform vec3 LightDirection;
uniform vec3 AmbientMaterial;
uniform vec3 SpecularMaterial;

void main()
{
    vec3 N = normalize(EyespaceNormal);
    vec3 L = normalize(LightPosition);
    vec3 E = normalize(LightDirection);
    vec3 H = normalize(L + E);
    
    float df = max(0.0, dot(N, L));
    float sf = max(0.0, dot(N, H));

    vec4 color = Color * vec4(AmbientMaterial, 1.0) + df * vec4(Diffuse, 1.0);// + sf * vec4(SpecularMaterial, 1.0);
    FragColor = color;
}