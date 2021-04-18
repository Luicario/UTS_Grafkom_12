#version 330 core

layout(location = 0) in vec3 aPosition;

// penambahan texture coordinates

layout(location = 1) in vec2 aTexCoord;

out vec2 texCoord;

void main(void)
{
    texCoord = aTexCoord;
    gl_Position = vec4(aPosition, 0.5);
}