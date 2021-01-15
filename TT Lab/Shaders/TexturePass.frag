#version 450 core
in vec3 Texpos;

uniform sampler2D tex;

out vec4 FragColor;

void main()
{
    vec4 color = texture(tex, Texpos.st);
    FragColor = color;
}