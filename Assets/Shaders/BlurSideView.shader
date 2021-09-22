Shader "Unlit/BlurSideView"
{
    // simple masked blur shader.
    // MainTex is used by unity to input camera image
    // Amount is the amount of blur
    // Mask is an image that uses brightness of pixel
    // to determine how much of the blur is applied to pixel
    // white = no blur, black = full blur, values inbetween = mix
 
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {} // bit input from Unity
        _Mask("Mask", 2D) = "white" {} // blend mask, white = no blur, black = full blur

        _Width ("TexWidth", Float) = 512	
		_Height ("TexHeight", Float) = 512

        _Blurryness ("Blurryness", float) = 2
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
 
            #include "UnityCG.cginc"
 
            struct appdata   
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            struct v2f  
            {
                float2 uv : TEXCOORD0;
                float2 muv : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            sampler2D _Mask;

            uniform float _Width;
			uniform float _Height;
            uniform float _Blurryness;

			uniform float3 _KernelLine1;
			uniform float3 _KernelLine2;
			uniform float3 _KernelLine3;

            float4 _MainTex_ST, _Mask_ST;
 
            v2f vert (appdata v) 
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.muv = TRANSFORM_TEX(v.uv, _Mask);
                return o;
            }
 
 
            fixed4 frag (v2f i) : SV_Target 
            {
                fixed4 col = tex2D(_MainTex, i.uv);
				fixed2 uv = i.uv;

				//textureSize to UV coords (0 to 1)//
				float w = _Blurryness / _Width;
				float h = _Blurryness / _Height;

				//texture sampling
				// tl , tc , tr
				// cl , cc , cr
				// bl , bc , br
				
				float3 tl = tex2D(_MainTex, uv + fixed2(-w, -h));	// Top Left
				float3 tc = tex2D(_MainTex, uv + fixed2( 0, -h));	// Top Centre
				float3 tr = tex2D(_MainTex, uv + fixed2(+w, -h));	// Top Right

				float3 cl = tex2D(_MainTex, uv + fixed2(-w, 0));	// Centre Left
				float3 cc = tex2D(_MainTex, uv);					// Centre Centre
				float3 cr = tex2D(_MainTex, uv + fixed2(+w, 0));	// Centre Right

				float3 bl = tex2D(_MainTex, uv + fixed2(-w, +h));	// Bottom Left
				float3 bc = tex2D(_MainTex, uv + fixed2( 0, +h));	// Bottom Centre
				float3 br = tex2D(_MainTex, uv + fixed2(+w, +h));	// Bottom Right

				//combine results
				float3 result = 
				tl + tc + tr +
				cl + cc + cr +
				bl + bc + br;

				result = result / 9;//surrounding pixels average for blur

                col = fixed4(result.r, result.g, result.b, 1);
 
                // now sample the original value
                float4 orig = tex2D(_MainTex, i.uv);
                float4 mask = tex2D(_Mask, i.muv);
                float unblurred = mask.r;
                float blurred = (1.0 - unblurred);
                col = col * blurred + orig * unblurred;
                return col;
            }
            ENDCG
        }
    }
}
