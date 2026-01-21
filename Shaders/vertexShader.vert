#version 330 core

layout (location = 0) in vec3 aPos;   // Position
layout (location = 1) in vec3 aColour; // Colour
layout (location = 2) in vec2 aTexCoord; // Texture Coordinates
layout (location = 3) in int aTexIndex;

out vec3 FragColour;
out vec2 TexCoord;

uniform float time; // Time variable

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

flat out int TexIndex;

void main()
{
    gl_Position = projection * view * vec4(aPos.xy, 0.0, 1.0);
//    gl_Position = vec4(aPos.xy, 0.0, 1.0);


    FragColour = aColour;
    TexCoord = aTexCoord;// + vec2(time, 0.0);
    TexIndex = aTexIndex;
}
