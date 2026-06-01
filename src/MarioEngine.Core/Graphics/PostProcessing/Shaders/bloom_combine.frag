#version 460
in vec2 vTexCoord;
out vec4 FragColor;
uniform sampler2D uOriginal;
uniform sampler2D uBloom;
uniform float uIntensity;
void main() {
    vec4 original = texture(uOriginal, vTexCoord);
    vec4 bloom = texture(uBloom, vTexCoord);
    FragColor = original + bloom * uIntensity;
}
