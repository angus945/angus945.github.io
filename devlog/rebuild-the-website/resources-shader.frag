#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

uniform float u_shape;
uniform float u_colorR;
uniform float u_colorG;
uniform float u_colorB;
uniform vec2 u_position;

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
float sdf_cross(vec2 uv, vec2 pos)
{
    uv = (uv - 0.5) * 2.0;
    pos = (pos - 0.5) * 2.0;

    float scale = 10.0;
    uv = uv * scale;
    pos = pos * scale;
    
    float dist = 100.0;
    dist = min(dist, rect(uv, pos, u_time * 50.0, vec2(3, 1)));
    dist = min(dist, rect(uv, pos, u_time * 50.0, vec2(1, 3)));
    dist = ceil(dist);

    return dist;
}
float sdf_dimand(vec2 uv, vec2 pos)
{
    uv = (uv - 0.5) * 2.0;
    pos = (pos - 0.5) * 2.0;
    
    float dist = 100.0;
    dist = min(dist, rect(uv, pos, u_time * 50.0, vec2(.5, .5)));
    dist = ceil(dist);

    return dist;
}
float sdf_blend(vec2 uv, vec2 pos)
{
    float crossSDF = sdf_cross(uv, pos);
    float dimondSDF = sdf_dimand(uv, pos);

    float t = sin(u_time) * 0.5 + 0.5;
    float dis = mix(crossSDF, dimondSDF, t);
    
    return dis;
}
void main() 
{
    vec2 uv = gl_FragCoord.xy / u_resolution.xx;
    // vec2 pos = u_mouse / u_resolution.xx;

    uv = mix(vec2(-1), vec2(1), uv);
    // gl_FragColor = vec4(uv, 0.0, 1.0);
    float dist = 0.0;
    if(u_shape == 0.0)
    {
        dist = sdf_dimand(uv, u_position);        
    }
    else if(u_shape == 1.0)
    {
        dist = sdf_cross(uv, u_position);
    }
    else dist = sdf_blend(uv, u_position);

    vec4 color = vec4(u_colorR, u_colorG, u_colorB, 1);
    gl_FragColor = mix(vec4(0), color, 1.0 - dist);
    // gl_FragColor = u_color;
}




