#version 460
in vec2 vTexCoord;
out vec4 FragColor;
uniform sampler2D uTexture;
uniform vec2 uDirection;
uniform float uSize;
void main() {
    vec4 color = texture(uTexture, vTexCoord) * 0.227;
    vec2 off = uDirection * uSize;
    color += texture(uTexture, vTexCoord + off * 1.0) * 0.194;
    color += texture(uTexture, vTexCoord - off * 1.0) * 0.194;
    color += texture(uTexture, vTexCoord + off * 2.0) * 0.121;
    color += texture(uTexture, vTexCoord - off * 2.0) * 0.121;
    color += texture(uTexture, vTexCoord + off * 3.0) * 0.054;
    color += texture(uTexture, vTexCoord - off * 3.0) * 0.054;
    FragColor = color;
}
