/*
Beginning DirectX 11 Game Programming
By Allen Sherrod and Wendy Jones

Texture Mapping Shader
*/

/////////////
// GLOBALS //
/////////////
cbuffer MatrixBuffer
{
	matrix worldMatrix;
	matrix viewMatrix;
	matrix projectionMatrix;
};


Texture2D shaderTexture;
SamplerState sampleType;


struct VS_Input
{
	float4 pos  : POSITION;
	float2 tex0 : TEXCOORD0;
};

struct PS_Input
{
	float4 pos  : SV_POSITION;
	float2 tex0 : TEXCOORD0;
};


PS_Input VS_Main(VS_Input input)
{
	PS_Input output = (PS_Input)0;

	//vsOut.pos = input.pos;
	//vsOut.tex0 = input.tex0;

	// Change the position vector to be 4 units for proper matrix calculations.
	input.position.w = 1.0f;

	// Calculate the position of the vertex against the world, view, and projection matrices.
	output.position = mul(input.position, worldMatrix);
	output.position = mul(output.position, viewMatrix);
	output.position = mul(output.position, projectionMatrix);

	// Store the texture coordinates for the pixel shader.
	output.tex = input.tex;

	return output;
}


float4 PS_Main(PS_Input input) : SV_TARGET
{
	return shaderTexture.Sample(sampleType, input.tex0);
}