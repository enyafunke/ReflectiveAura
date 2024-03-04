Shader"Julian/ToonSurface2" {
    //show values to edit in inspector
    Properties 
    {
        _Color ("Color", Color) = (0.5, 0.65, 1, 1)
        _MainTex ("Texture", 2D) = "white" {}

        [HDR] 
        _AmbientColor ("Ambient Color", Color) = (0.4, 0.4, 0.4, 1) // emission
        _SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
        _Glossiness("Glossiness", Float) = 32
        _RimColor("Rim Color", Color) = (1,1,1,1)
        _RimAmount("Rim Amount", Range(0, 1)) = 0.716
        _RimThreshold("Rim Threshold", Range(0, 1)) = 0.1
    }
    SubShader {
        //the material is completely non-transparent and is rendered at the same time as the other opaque geometry
        Tags{ "RenderType"="Opaque" "Queue"="Geometry"}

        CGPROGRAM

        //the shader is a surface shader, meaning that it will be extended by unity in the background to have fancy lighting and other features
        //our surface shader function is called surf and we use our custom lighting model
        //fullforwardshadows makes sure unity adds the shadow passes the shader might need
        #pragma surface surf Stepped fullforwardshadows
        #pragma target 3.0

        #include "Lighting.cginc"

        sampler2D _MainTex;
        float4 _Color;
        float4 _AmbientColor;
        float4 _SpecularColor;
        float _Glossiness;
        float4 _RimColor;
        float _RimAmount;
        float _RimThreshold;

        //fixed4 _Color;
        //half3 _Emission;


        //our lighting function. Will be called once per light
        float4 LightingStepped(SurfaceOutput s, float3 lightDir, half3 viewDir, float shadowAttenuation)
        {
            // Shadow
            float shadow = shadowAttenuation;
            
            //  4. Compare the world normal to the light's direction using the Dot Product.
            float3 normal = normalize(s.Normal);
                
            // Directional light
            float NdotL = dot(normal, _WorldSpaceLightPos0);
            float lightIntensity = smoothstep(0, 0.01, NdotL * shadow); //NdotL > 0 ? 1 : 0; // 5. Divide the lighting into two bands: light and dark
            float4 light = lightIntensity * _LightColor0;
                
            // Sepcular component
            float3 halfVector = normalize(lightDir + viewDir);
            float NdotH = dot(normal, halfVector);
            float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
            float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
            float4 specular = specularIntensitySmooth * _SpecularColor;
                
            // Rim lighting
            float4 rimDot = 1 - dot(viewDir, normal);
            float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
            rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
            float4 rim = rimIntensity * _RimColor;
    
            //float4 sample = tex2D(_MainTex, i.uv);

            return (_AmbientColor + light + specular + rim);
}


        //input struct which is automatically filled by unity
        struct Input
        {
            float2 uv_MainTex;
        };

        //the surface shader function which sets parameters the lighting function then uses
        void surf(Input i, inout SurfaceOutput o)
        {
            //sample and tint albedo texture
            fixed4 col = tex2D(_MainTex, i.uv_MainTex);
            col *= _Color;
    
            o.Albedo = col.rgb;
            o.Emission = _AmbientColor;
        }
        ENDCG
    }
    FallBack"Standard"
}