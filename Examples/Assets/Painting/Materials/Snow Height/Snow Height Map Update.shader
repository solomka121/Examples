Shader "Unlit/Snow Height Map Update"
{
    Properties
    {
        _DrawPosition ("Draw Position" , vector ) = (-1, -1, 0, 0)
        _Restore ("Restore " , float ) = 1
    }

    SubShader
    {
        Lighting Off
        Blend One Zero

        Pass
        {
            CGPROGRAM
            #include "UnityCustomRenderTexture.cginc"
            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment frag
            #pragma target 3.0
            
            float4 _DrawPosition;
            float _Restore;

            float4 frag(v2f_customrendertexture IN) : COLOR
            {
                float4 previousColor = tex2D(_SelfTexture2D, IN.localTexcoord.xy);
                float4 drawColor = smoothstep(.05 , .14, distance(IN.localTexcoord.xy , _DrawPosition) );
                
                return min(previousColor , drawColor) + _Restore;
            }
            ENDCG
        }
    }
}