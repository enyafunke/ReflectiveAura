Shader"Julian/ToonPoint"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        [HDR]
        _AmbientColor ("Ambient Color", Color) = (0.6, 0.6, 0.6, 1.0)
        _SpecularColor("Specular Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _Glossiness("Glossiness", Float) = 23.03
        _RimColor("Rim Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _RimAmount("Rim Amount", Range(0, 1)) = 0.379
        _RimThreshold("Rim Threshold", Range(0, 1)) = 0.149
    }
    SubShader
    {
        LOD 100
        Tags { "RenderType"="Opaque" }

        Pass
        {
            Tags
            {
	            "LightMode" = "ForwardBase" // directional, ambient
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
                float4 worldVertex : TEXCOORD1;
                SHADOW_COORDS(2)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			
            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldVertex = mul(unity_ObjectToWorld, v.vertex);
    
                TRANSFER_SHADOW(o)
    
                return o;
            }
			
            float4 _AmbientColor;

            float4 frag(v2f i) : SV_Target
            {
                float4 sample = tex2D(_MainTex, i.uv);
                return sample * _AmbientColor;
            }
			ENDHLSL
		}
        Pass {
            Tags { "LightMode" = "ForwardAdd" } 
            Blend One One // additive blending 
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdadd
            
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
                float4 worldVertex : TEXCOORD1;
                SHADOW_COORDS(2)
            };
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
			
            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
    
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldVertex = mul(unity_ObjectToWorld, v.vertex);
    
                TRANSFER_SHADOW(o)
    
                return o;
            }
			
            float4 _AmbientColor;
            float4 _SpecularColor;
            float _Glossiness;
            float4 _RimColor;
            float _RimAmount;
            float _RimThreshold;

            float4 frag(v2f i) : SV_Target
            {
                float3 normal = normalize(i.worldNormal);
                float3 viewDir = normalize(UnityWorldSpaceViewDir(i.worldVertex.xyz));
                float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
                float lightDist = length(_WorldSpaceLightPos0.xyz - i.worldVertex.xyz);
                float3 lightDir = (_WorldSpaceLightPos0.xyz - i.worldVertex.xyz) / lightDist;
                float lightAtt;
                
                float NdotL = dot(normal, lightDir);
                float NdotV = dot(normal, viewDir);
                float NdotH = dot(normal, halfVector);
    
                // Shadow
                float shadow = SHADOW_ATTENUATION(i);
    
                // Light attenuation
                if (NdotL > 0.0f && NdotV > 0.0f)
                {
                    lightAtt = 1.0 / lightDist;
                }
                else
                {
                    lightAtt = 1.0;
                }
                
                // Point light
                float lightIntensity = smoothstep(0, 0.01, NdotL * shadow);
                float4 point_light = lightIntensity * _LightColor0;
                
                // Specular component
                float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
                float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
                float4 specular = specularIntensitySmooth * _SpecularColor;
                
                // Rim lighting
                float rimDot = 1 - dot(viewDir, normal);
                float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
                rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
                float4 rim = rimIntensity * _RimColor;
    
                float4 sample = tex2D(_MainTex, i.uv);

                return sample * lightAtt * (point_light + specular + rim);
            }
            ENDHLSL
        }
        UsePass"Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}
