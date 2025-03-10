#version 330 core

in vec3 vertexColor;
in vec2 TexCoord;

out vec4 FragColor;

uniform sampler2D texture1;

void main()
{
     FragColor = texture(texture1, TexCoord);
    
    // This just visualises the UV of the primitive
//    FragColor = vec4(TexCoord, 0.0, 1.0);
}
