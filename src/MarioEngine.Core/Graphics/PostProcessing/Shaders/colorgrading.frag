#version 460
in vec2 vTexCoord;
out vec4 FragColor;
uniform sampler2D uTexture;
uniform sampler2D uLUT;
void main() {
    vec4 color = texture(uTexture, vTexCoord);
    float index = color.r + color.g * 16.0 + color.b * 256.0;
    float lutX = mod(index, 16.0) / 16.0 + 0.5 / 256.0;
    float lutY = floor(index / 16.0) / 16.0 + 0.5 / 16.0;
    vec3 graded = texture(uLUT, vec2(lutX, lutY)).rgb;
    FragColor = vec4(graded, color.a);
}
