#version 330 core

// Receive the input from the vertex shader in an attribute
in vec2 frag_texCoords; // Normalized
uniform vec2 PlayerPosition; // In pixels
uniform vec2 WindowSize; // In Pixels
out vec4 out_color;

void main()
{
    vec2 PixelCord = vec2(frag_texCoords.x * WindowSize.x, frag_texCoords.y * WindowSize.y);
    if(distance(PlayerPosition, PixelCord) < 100){
        out_color = vec4(frag_texCoords.x, frag_texCoords.y, 0.0, 1.0);
    }
    else{
        out_color = vec4(0.0, 0.0, 0.0, 1.0);
    }
}