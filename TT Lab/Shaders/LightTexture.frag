#version 130
in vec3 EyespaceNormal;
in vec4 Color;
in vec3 Diffuse;
in vec3 Texpos;

uniform vec3 LightPosition;
uniform vec3 LightDirection;
uniform vec3 AmbientMaterial;
uniform vec3 SpecularMaterial;
uniform sampler2D tex;

out vec4 FragColor;

void main()
{
    vec3 N = normalize(EyespaceNormal);
    vec3 L = normalize(LightPosition);
    vec3 E = normalize(LightDirection);
    vec3 H = normalize(L + E);
    
    float df = max(0.0, dot(N, L));
    float sf = max(0.0, dot(N, H));
    vec2 tp = vec2(Texpos.s, 1.0 - Texpos.t);
    vec4 color = texture(tex, tp) * Color + vec4(0.3);
    FragColor = color;
}