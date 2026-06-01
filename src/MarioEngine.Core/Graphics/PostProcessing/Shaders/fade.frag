#version 460
in vec2 vTexCoord;
out vec4 FragColor;
uniform sampler2D uTexture;
uniform float uAlpha;
void main() {
    vec4 color = texture(uTexture, vTexCoord);
    FragColor = vec4(color.rgb * (1.0 - uAlpha), 1.0);
}
