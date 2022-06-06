#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

uniform float u_seed;

uniform sampler2D u_buffer0;

float random(vec2 st)
{
    return fract(sin(dot(st.xy, vec2(12.9898, u_seed))) * 43758.5453123);
}
void main()
{
    float width = min(u_resolution.x, u_resolution.y);
    vec2 uv = gl_FragCoord.xy / width;

    vec2 cell = fract(uv * 10.0);
    vec2 coord = floor(uv * 10.0);

    float value = random(coord.xy);
    gl_FragColor = vec4(value, value, value, 1);
    // gl_FragColor = vec4(cell, 0, 1);
    // gl_FragColor = vec4(coord / 10.0, 0, 1);
}
