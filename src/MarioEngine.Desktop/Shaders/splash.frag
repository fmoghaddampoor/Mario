#version 460
in vec2 vTexCoord;
out vec4 FragColor;
uniform sampler2D uTexture;
uniform float uTime;
void main()
{
    vec2 uv = vTexCoord;
    
    // Gentle, organic gas cloud warping (no rotation, no corner artifacts)
    uv.x += sin(uTime * 0.12 + uv.y * 3.0) * 0.004;
    uv.y += cos(uTime * 0.09 + uv.x * 3.0) * 0.004;
    
    FragColor = texture(uTexture, uv);
}
