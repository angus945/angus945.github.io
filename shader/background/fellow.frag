#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

uniform float u_seed;

uniform sampler2D u_buffer0;


#ifdef BUFFER_0

vec4 readBuffer(sampler2D buffer, vec2 coord)
{
    vec2 uv = gl_FragCoord.xy / u_resolution.xy;
    return texture2D(buffer, uv, 0.0);
}

void main() 
{
    float width = min(u_resolution.x, u_resolution.y);
    vec2 uv = gl_FragCoord.xy / width;
    vec2 mouse = u_mouse.xy / width;

    float value = readBuffer(u_buffer0, gl_FragCoord.xy).a;
    float circle = 1. - ceil(length(uv - mouse) - .05);

    value = value * 0.95;
    value = max(value, circle);

    value = clamp(value, 0.0, 1.0);
    gl_FragColor = vec4(value);
}

#else

void main() {
    vec2 uv = gl_FragCoord.xy / u_resolution.xy;
    vec4 color = texture2D(u_buffer0, uv, 0.0);

    gl_FragColor = color;
}

#endif