#version 330 core

// Receive the input from the vertex shader in an attribute
in vec2 frag_texCoords;
uniform vec2 PlayerPosition;

out vec4 out_color;

void main()
{
    
    if(distance(PlayerPosition, frag_texCoords) < 0.1){
        out_color = vec4(frag_texCoords.x, frag_texCoords.y, 0.0, 1.0);
    }
    else{
        out_color = vec4(0.0, 0.0, 0.0, 1.0);
    }
}