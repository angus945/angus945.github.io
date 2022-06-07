#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

uniform float u_seed;
uniform float u_theme;

vec3 ran1To3(float seed)
{
    float x = fract(sin(dot(vec2(seed, 31.12), vec2(12.9898, 78.233))) * 43758.5453);
    float y = fract(x * 21.5 + 451.548);
    float z = fract((y - x) * (x + y));
    return vec3(x, y, z);
}

void main() 
{
    vec2 st = gl_FragCoord.xy / u_resolution.xy;
    // vec2 mouse = u_mouse / u_resolution;

    vec3 colorA = ran1To3(u_seed);
    vec3 colorB = fract(colorA * 31.5 + 784.451);
    vec3 color = mix(colorA, colorB, dot(st, colorA.xy));
        
    vec4 darkColor = vec4(color * 0.2, 0.1);
    vec4 lightColor = vec4(color * 0.1, 0.1);
    gl_FragColor = mix(darkColor, lightColor, u_theme);
}