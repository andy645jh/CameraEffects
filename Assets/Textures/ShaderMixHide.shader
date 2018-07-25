Shader "Custom/ShaderMixHide" {
	Properties{        
        _MainTex("Albedo Primary (RGB)", 2D) = "white" {}
		_SecondTex("Albedo Second (RGB)", 2D) = "white" {}
        _DissolverTex("Dissolver Tex (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
 
        _DissolvePercentage("DissolvePercentage", Range(0,1)) = 0.0
       
    }
        SubShader{
        Tags{ "RenderType" = "Opaque" }
        LOD 200
 
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows
 
                // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0
 
        sampler2D _MainTex;
		sampler2D _SecondTex;
        sampler2D _DissolverTex;
 
    struct Input 
    {
        float2 uv_MainTex;
		float2 uv_SecondTex;
        float2 uv_DissolverTex;
    };
 
    half _Glossiness;
    half _Metallic;
    half _DissolvePercentage;   
    fixed4 _Color;
 
    void surf(Input IN, inout SurfaceOutputStandard o)
    {       
        // Albedo comes from a texture tinted by color
        fixed4 mainCol = tex2D (_MainTex, IN.uv_MainTex);
		fixed4 secondCol = tex2D (_SecondTex, IN.uv_SecondTex);
        half gradient = tex2D(_DissolverTex, IN.uv_DissolverTex).r;       
        clip(gradient- _DissolvePercentage);
 
        //fixed4 c = lerp(1, gradient, 0) * secondCol.r;
		fixed4 c = lerp(secondCol.r, mainCol.g ,  gradient);

        o.Albedo = mainCol.rgb;
 
        // Metallic and smoothness come from slider variables
        o.Metallic = _Metallic;
        o.Smoothness = _Glossiness;
        o.Alpha = c.a;
    }
    ENDCG
    }
    FallBack "Diffuse"
}
