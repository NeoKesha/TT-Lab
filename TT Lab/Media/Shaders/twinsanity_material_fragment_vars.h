OGRE_UNIFORMS(
uniform vec2 scrollSpeed;
)

vec4 texturePanorama(vec3 normal, sampler2D pano)
{
    vec2 st = vec2(atan(normal.x, normal.z), acos(normal.y));

    if (st.x < 0.0)
    {
        st.x += 3.14159 * 2.0;
    }

    st /= vec2(3.14159 * 2.0, 3.14159);

    return texture2DLod(pano, st, 0.0);
}