#version 460
in vec2 vTexCoord;
out vec4 FragColor;
uniform sampler2D uTexture;
uniform float uTime;
void main()
{
    vec2 uv = vTexCoord;
    uv.x += sin(uTime * 0.15 + uv.y * 3.0) * 0.006;
    uv.y += cos(uTime * 0.12 + uv.x * 3.0) * 0.006;
    FragColor = texture(uTexture, uv);
}
