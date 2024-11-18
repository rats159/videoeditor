#version 330 core

in vec2 fPos;
in vec2 fUv;

out vec4 fragColor;

void main()
{
    fragColor = vec4(fUv, 0, 1.0f);
} 