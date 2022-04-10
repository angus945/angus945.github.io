#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

#define deg2rad 0.0174532925;

vec2 uvRotate(vec2 uv, float degree)
{
    float radian = degree * deg2rad;
    vec2 ihat = uv.x * vec2(cos(radian), sin(radian));
    vec2 jhat = uv.y * vec2(-sin(radian), cos(radian));

    return ihat + jhat;
}
float rect(vec2 uv, vec2 position, float rotate, vec2 size)
{
    uv = uv - position;
    uv = uvRotate(uv, rotate);

    vec2 dist = abs(uv) - size;

    return max(dist.x, dist.y);
}
vec4 frag(vec2 uv, vec2 mouse)
{
    uv = (uv - 0.5) * 2.0;
    mouse = (mouse - 0.5) * 2.0;

    float scale = 10.0;
    uv = uv * scale;
    mouse = mouse * scale;
    
    float dist = 100.0;
    dist = min(dist, rect(uv, mouse, u_time * 50.0, vec2(3, 1)));
    dist = min(dist, rect(uv, mouse, u_time * 50.0, vec2(1, 3)));
    dist = ceil(dist);
    dist = 1.0 - dist;

    return vec4(dist, dist, dist, dist);
}
void main() 
{
    vec2 uv = gl_FragCoord.xy / u_resolution.xy;
    vec2 mouse = u_mouse / u_resolution.xy;

    gl_FragColor = frag(uv, mouse);
}




