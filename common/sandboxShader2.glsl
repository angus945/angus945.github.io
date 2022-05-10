#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

uniform float u_input0;
uniform float u_input1;
uniform float u_input2;

#define deg2rad 0.0174532925;

void main() 
{
    vec2 uv = gl_FragCoord.xy / u_resolution.xx;
    vec2 mouse = u_mouse / u_resolution.xx;

    uv = (uv * 2.0) - 1.0 + 0.05;
    uv = abs(uv);

    vec2 grid = fract(uv * 10.0);
    grid = step(grid, vec2(0.1));

    vec3 color = vec3(1) * 0.8;
    // grid = uv;
    // gl_FragColor = mix(vec4(0), color, 1.0 - dist);
    // gl_FragColor = mix(vec4(0), vec4(1), 1.0 - dist);
    gl_FragColor = vec4(color, length(grid));
}




