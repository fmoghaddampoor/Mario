#version 460
in vec2 vTexCoord;
out vec4 FragColor;
uniform sampler2D uTexture;
uniform float uTime;

// High-quality coordinate hash function
float hash(vec2 p)
{
    return fract(sin(dot(p, vec2(12.9898, 78.233))) * 43758.5453);
}

void main()
{
    vec4 color = texture(uTexture, vTexCoord);
    
    // Smooth time factor for calm twinkling
    float t = uTime * 2.0;
    
    // Quantize UV coordinates to a grid.
    // This ensures that all pixels belonging to the same star dot fall into 
    // the same grid cell and share the exact same hash offset, keeping the star solid.
    vec2 grid = floor(vTexCoord * 160.0);
    
    // Obtain a unique random phase offset for this specific star dot
    float offset = hash(grid) * 6.28;
    
    // Twinkle wave unique to this star's hash
    float wave = sin(t + offset);
    float twinkle = 0.5 + 0.5 * wave;
    
    // Sharpen peaks slightly for a beautiful sparkling feel
    twinkle = pow(twinkle, 2.0);
    
    // Calm glow range: from dim ambient (0.3) to bright flare (1.8)
    float pulse = 0.3 + 1.5 * twinkle;
    
    color.rgb *= pulse;
    FragColor = color;
}
