#version 330 core

in vec3 vColor;
in vec2 vTexCoord;

out vec4 FragColor;

void main()
{
    // PURE colour proof-of-life
    FragColor = vec4(vColor, 1.0);

    // Optional UV debug:
    // FragColor = vec4(vTexCoord, 0.0, 1.0);
}
