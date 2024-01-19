#version 330 core
layout (location = 0) in vec2 Apos;
layout (location = 1) in vec2 Atex;

out vec2 TexCoords;

uniform mat4 model;
uniform mat4 projection;
uniform mat4 view;

void main()
{
    TexCoords = Atex;
    gl_Position = vec4(vec3(Apos.x,Apos.y,0.5),1.0) * model * view * projection;
}