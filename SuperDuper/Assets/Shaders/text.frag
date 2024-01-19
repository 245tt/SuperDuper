#version 330 core
out vec4 color;

uniform sampler2D sprite;
uniform vec3 textColor;

in vec2 texCoords;

void main()
{
    vec4 tex = texture(sprite,texCoords);
    if(tex.a < 0.1) discard;
    color = vec4(tex.rgb * textColor,1.0);
}