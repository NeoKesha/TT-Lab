#version 450 core

uniform float Alpha;
uniform int TexturesAmount;
uniform sampler2D tex[5];
uniform bool AlphaBlending[5];
uniform float FIX[5];
uniform int SpecColA[5];
uniform int SpecColB[5];
uniform int SpecAlphaC[5];
uniform int SpecColD[5];


vec4 ShadeFragment(vec3 texCoord, vec4 col, vec3 diffuse, vec3 eyespaceNormal)
{
	vec4 color = vec4(1.0);
	for (int i = 0; i < TexturesAmount; ++i)
	{
		vec4 texCol = texture(tex[i], texCoord.st);
		if (AlphaBlending[i]) {
			vec4 colorA = vec4(0.0);
			vec4 colorB = vec4(0.0);
			vec4 alphaC = vec4(FIX[i]);
			vec4 colorD = vec4(0.0);
			if (SpecColA[i] == 0) {
				colorA = texCol;
			}
			else if (SpecColA[i] == 1) {
				colorA = color;
			}
			if (SpecColB[i] == 0) {
				colorB = texCol;
			}
			else if (SpecColB[i] == 1) {
				colorB = color;
			}
			if (SpecAlphaC[i] == 0) {
				alphaC = vec4(texCol.a);
			}
			else if (SpecAlphaC[i] == 1) {
				alphaC = vec4(color.a);
			}
			if (SpecColD[i] == 0) {
				colorD = texCol;
			}
			else if (SpecColD[i] == 1) {
				colorD = color;
			}
			color = (colorA - colorB) * alphaC + colorD;
		}
		else {
			color = texCol;
		}
	}
	color.a *= Alpha;
	return color;
}