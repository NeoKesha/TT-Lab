OGRE_UNIFORMS(
uniform vec4 lightPos;
uniform vec4 lightAttenuation;
)

float attenuation(float r, float f, float d) {
    float denom = d / r + 1.0;
    float attenuation = 1.0 / (denom*denom);
    float t = (attenuation - f) / (1.0 - f);
    return max(t, 0.0);
}