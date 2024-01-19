#version 330 core
layout (location = 0) in vec2 Apos;
layout (location = 1) in vec2 Atex;

//uniform mat4 model;

out vec2 texCoords;

void main()
{
    texCoords = Atex;
    gl_Position = vec4(vec3(Apos.x,Apos.y,0.5),1.0);// * model;
}