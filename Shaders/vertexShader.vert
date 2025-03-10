#version 330 core

layout (location = 0) in vec3 aPos;   // Position
layout (location = 1) in vec3 aColor; // Color
layout (location = 2) in vec2 aTexCoord; // Texture Coordinates

out vec3 vertexColor;
out vec2 TexCoord;

uniform float time; // Time variable
uniform mat4 transform;

void main()
{
    gl_Position = transform * vec4(aPos, 1.0);
    
    TexCoord = aTexCoord;// + vec2(time, 0.0);
}
