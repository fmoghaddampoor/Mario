#version 460
in vec2 vTexCoord;
in vec4 vColor;

out vec4 FragColor;

uniform sampler2D uTexture;
uniform float uTime;

// Effect flags (0 or 1)
uniform float uFlash;
uniform float uTint;
uniform float uDissolve;
uniform float uShimmer;
uniform float uPulse;

// Effect parameters
uniform vec4 uTintColor;
uniform float uDissolveProgress;
uniform float uShimmerSpeed;
uniform float uPulseSpeed;

// Simple noise function for dissolve
float rand(vec2 co)
{
    return fract(sin(dot(co.xy, vec2(12.9898, 78.233))) * 43758.5453);
}

void main()
{
    vec4 color = texture(uTexture, vTexCoord) * vColor;

    // Flash: white overlay
    if (uFlash > 0.5)
    {
        color.rgb = mix(color.rgb, vec3(1.0), 0.7);
    }

    // Tint: color overlay
    if (uTint > 0.5)
    {
        color.rgb = mix(color.rgb, uTintColor.rgb, uTintColor.a);
    }

    // Dissolve: noise-based cutout
    if (uDissolve > 0.5)
    {
        float noise = rand(vTexCoord * 100.0);
        float cutoff = uDissolveProgress;
        float edge = 1.0 - smoothstep(cutoff - 0.05, cutoff + 0.05, noise);
        color.a *= edge;
    }

    // Shimmer: rainbow cycling
    if (uShimmer > 0.5)
    {
        float hue = fract(uTime * uShimmerSpeed + vTexCoord.x * 0.5 + vTexCoord.y * 0.3);
        vec3 rainbow = 0.5 + 0.5 * cos(6.28318 * (hue + vec3(0.0, 0.333, 0.667)));
        color.rgb = mix(color.rgb, rainbow, 0.4);
    }

    // Pulse: alpha oscillation
    if (uPulse > 0.5)
    {
        float pulse = 0.5 + 0.5 * sin(uTime * uPulseSpeed * 6.28318);
        color.a *= 0.5 + 0.5 * pulse;
    }

    FragColor = color;
}
