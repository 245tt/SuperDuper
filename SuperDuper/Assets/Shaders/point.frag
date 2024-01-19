#version 330 core
out vec4 color;

uniform sampler2D sprite;
uniform vec3 pointColor;
//uniform vec3 spriteColor;

void main()
{
    
    color = vec4(pointColor,1.0);
}