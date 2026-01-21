#version 330 core

in vec3 vertexColor;
in vec2 TexCoord;

out vec4 FragColor;

flat in int textureIndex;

uniform sampler2D texture0;
uniform sampler2D texture1;
//uniform sampler2D textures[2];

void main()
{
//     FragColor = texture(textures[textureIndex], TexCoord);

     FragColor = mix(texture(texture0, TexCoord), texture(texture1, TexCoord), 0.5);

     
//    This just visualises the UV of the primitive
    FragColor = vec4(TexCoord, 0.0, 1.0);
}
