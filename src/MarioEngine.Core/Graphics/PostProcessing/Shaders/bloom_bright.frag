#version 460
in vec2 vTexCoord;
out vec4 FragColor;
uniform sampler2D uTexture;
uniform float uThreshold;
void main() {
    vec4 color = texture(uTexture, vTexCoord);
    float lum = dot(color.rgb, vec3(0.299, 0.587, 0.114));
    float brightness = max(0.0, lum - uThreshold);
    FragColor = color * brightness / max(lum, 0.001);
}
