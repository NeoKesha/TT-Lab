resultUvs = scrollSpeed * vec2(elapsedTime) + resultUvs;
vec4 tex = texture2D(uTexture, resultUvs.xy);
resultColor = texture2D(uTexture, resultUvs.xy) * lDiffuse * uDiffuseColor;