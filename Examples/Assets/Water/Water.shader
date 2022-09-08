Shader "Water"
{
    Properties
    {
        _MainColor("Main Color", Color ) = (1 , 1 , 1 , 0)
        _MainTex ("Texture", 2D) = "white" {}
        _OffsetRange("Offset Range", Range(0.1 , 3)) = 1
        _AnimationSpeed("Animation Speed", Range(1 , 10)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            #pragma target 4.6
            #include "Tessellation.cginc"
            #pragma tessellate:tess


            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            fixed4 _MainColor; 
            float _OffsetRange;
            float _AnimationSpeed;
            float4 _MainTex_ST;

            float tess()
            {
                return 4;
            }
            
            v2f vert (appdata v)
            {
                v2f o;
                
                v.vertex.y += sin(_Time.y * _AnimationSpeed + v.vertex.x + v.vertex.z) * _OffsetRange;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) * _MainColor;
                
                return col;
            }
            ENDCG
        }
    }
}
