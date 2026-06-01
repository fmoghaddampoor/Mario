#version 460
in vec2 vTexCoord;
out vec4 FragColor;
uniform sampler2D uTexture;
uniform float uTime;
void main()
{
    vec4 color = texture(uTexture, vTexCoord);
    float pulse = 0.3 + 0.7 * (0.5 + 0.5 * sin(uTime * 2.0 + vTexCoord.x * 200.0 + vTexCoord.y * 150.0));
    color.rgb *= pulse;
    FragColor = color;
}
