#version 330 core
layout (location = 0) in vec2 Apos;

uniform mat4 model;
uniform mat4 projection;
uniform mat4 view;

void main()
{
    //gl_PointSize = 5;
    gl_Position = vec4(vec3(Apos.x,Apos.y,0.5),1.0) * model * view * projection;
}