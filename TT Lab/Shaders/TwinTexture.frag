#version 450 core
in vec4 passColor;
in float Depth;

layout (location = 0) out vec4 FragColor;
layout (location = 1) out float alpha;

float GetWeight(float z, vec4 color);

void main(void) {
	float weight = GetWeight(Depth, passColor);

	FragColor = vec4(passColor.rgb * passColor.a, passColor.a) * weight;
    alpha = weight;
}