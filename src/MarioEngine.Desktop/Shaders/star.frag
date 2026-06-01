#version 460
in vec2 vTexCoord;
out vec4 FragColor;
uniform sampler2D uTexture;
uniform float uTime;
void main()
{
    vec4 color = texture(uTexture, vTexCoord);
    float pulse = 0.7 + 0.3 * sin(uTime * 1.5 + vTexCoord.x * 100.0 + vTexCoord.y * 80.0);
    color.rgb *= pulse;
    FragColor = color;
}
