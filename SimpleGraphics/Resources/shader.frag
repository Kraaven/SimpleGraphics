#version 330 core

in vec2 frag_texCoords;
uniform vec3 PlayerPosition;
uniform vec2 WindowSize;
uniform float Focus;
out vec4 out_color;

void main()
{
    float aspect = WindowSize.x / WindowSize.y;

    // Normalized screen space
    vec2 uv = frag_texCoords * 2.0 - 1.0;

    // Ray with correct aspect handling
    vec3 RayOrigin = vec3(0.0, 0.0, 0.0);
    vec3 RayDir = normalize(vec3(uv.x * aspect, uv.y, Focus));

    vec3 RaySample = RayOrigin;
    bool hit = false;

    for(int i = 0; i < 100; i++)
    {
        if (distance(RaySample, PlayerPosition) < 1.0)
        {
            hit = true;
            break;
        }
        RaySample += RayDir;
    }

    if(hit)
    out_color = vec4(1.0, 1.0, 1.0, 1.0);
    else
    out_color = vec4(0.0, 0.0, 0.0, 1.0);
}
