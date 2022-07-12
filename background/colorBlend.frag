#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

uniform float u_seed;
uniform float u_theme;

void randomColor(float seed, out vec3 colorA, vec3 colorB)
{
    colorA = fract(vec3(seed * 1.0, seed * 10.0, seed * 100.0));
    colorB = fract(vec3(seed * 1000.0, seed * 10000.0, seed * 100000.0));
}

void main() 
{
    vec2 st = gl_FragCoord.xy / u_resolution.xy;
    // vec2 mouse = u_mouse / u_resolution;

    vec3 colorA, colorB;
    randomColor(u_seed, colorA, colorB);
    
    vec3 color = mix(colorA, colorB, dot(st, colorA.xy));
    color.r += sin(u_time * +5.0) * 0.1;
    color.g += cos(u_time * -3.0) * 0.2;
    color.b += sin(u_time * +1.0) * 0.3;
        
    vec4 darkColor = vec4(color * 0.5, 0);
    vec4 lightColor = vec4(color * 0.3, 0.1);
    gl_FragColor = mix(darkColor, lightColor, u_theme);
}