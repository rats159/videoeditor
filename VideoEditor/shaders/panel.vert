#version 330 core
layout (location = 0) in vec2 aPos;
layout (location = 1) in vec2 aUv;

out vec2 fPos;
out vec2 fUv;

void main()
{
    fPos = aPos;
    fUv = aUv;
    gl_Position = vec4(aPos.x, aPos.y, 0, 1.0);
}