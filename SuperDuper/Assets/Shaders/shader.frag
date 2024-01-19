#version 330 core
in vec2 TexCoords;
out vec4 color;

uniform sampler2D sprite;
//uniform vec3 spriteColor;

void main()
{
    if(texture(sprite,TexCoords).a <= 0.1)
    {
        discard;        
    }
    color = vec4(vec3(1), 1.0) * texture(sprite, TexCoords);
}