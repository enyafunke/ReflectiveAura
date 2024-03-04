Shader"Julian/Toon"
{
    Properties
    {
        _Color ("Color", Color) = (0.5, 0.65, 1, 1)
        _MainTex ("Main Texture", 2D) = "white" {}
        [HDR]
        _AmbientColor ("Ambient Color", Color) = (0.4, 0.4, 0.4, 1)
        _SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
        _Glossiness("Glossiness", Float) = 32
        _RimColor("Rim Color", Color) = (1,1,1,1)
        _RimAmount("Rim Amount", Range(0, 1)) = 0.716
        _RimThreshold("Rim Threshold", Range(0, 1)) = 0.1
    }
    SubShader
    {
        Pass
        {
            // 1. Tags to receive lighting data
            Tags
            {
	            "LightMode" = "ForwardBase" // directional, ambient, baked lightning, reflections
	            "PassFlags" = "OnlyDirectional"
            }

            HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
            #pragma multi_compile_fwdbase
			
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"
            
            // 2. Access objects normal data
            struct appdata
            {
                float4 vertex : POSITION;
                float4 uv : TEXCOORD0;
                float3 normal : NORMAL; // populated automatically
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldNormal : NORMAL; // manually populated in the vertex shader
                float3 viewDir : TEXCOORD1;
                SHADOW_COORDS(2)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			
            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
    
                // 3. Transform the normal from object space to world space, as the light's direction is provided in world space
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                // 5. Specular reflection is view dependent
                o.viewDir = WorldSpaceViewDir(v.vertex);
                TRANSFER_SHADOW(o)
    
                return o;
            }
			
            float4 _Color;
            float4 _AmbientColor;
            float4 _SpecularColor;
            float _Glossiness;
            float4 _RimColor;
            float _RimAmount;
            float _RimThreshold;

            float4 frag(v2f i) : SV_Target
            {
                //  4. Compare the world normal to the light's direction using the Dot Product.
                float3 normal = normalize(i.worldNormal);
                float3 viewDir = normalize(i.viewDir);
    
                // Shadow
                float shadow = SHADOW_ATTENUATION(i);
                
                // Directional light
                float NdotL = dot(_WorldSpaceLightPos0, normal);
                float lightIntensity = smoothstep(0, 0.01, NdotL * shadow); //NdotL > 0 ? 1 : 0; // 5. Divide the lighting into two bands: light and dark
                float4 light = lightIntensity * _LightColor0;
                
                // Sepcular component
                float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
                float NdotH = dot(normal, halfVector);
                float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
                float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
                float4 specular = specularIntensitySmooth * _SpecularColor;
                
                // Rim lighting
                float4 rimDot = 1 - dot(viewDir, normal);
                float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
                rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
                float4 rim = rimIntensity * _RimColor;
    
                float4 sample = tex2D(_MainTex, i.uv);

                return _Color * sample * (_AmbientColor + light + specular + rim);
                
                // reflexions-konstate(ambient) * light(ambient) + für jedes licht(reflexions-konstante(diffuse))
                // Phong: k_a * i_a + SUM_m (k_d * (L_m dot N) * i_m,d + k_s * (R_m dot V)^a * i_m,s // L_m richtungsvektor zwischen punkt und punktlichtquelle
                // Toon (Wang et al.): k_a * i_a + SUM_m (k_d * u_m * i_m,d + k_s * v_m * i_m,s
}
			ENDHLSL
		}
        /* Pass
        {
            Tags { "LightMode" = "ForwardAdd" } 
            Blend One One // additive blending    
        } */
        UsePass"Legacy Shaders/VertexLit/SHADOWCASTER" // grabs a pass from a different shader and inserts it into our shader
    }
}
