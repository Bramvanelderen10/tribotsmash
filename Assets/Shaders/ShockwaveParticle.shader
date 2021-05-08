// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.30 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.30;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:2,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33455,y:33052,varname:node_3138,prsc:2|diff-7438-OUT,emission-7438-OUT;n:type:ShaderForge.SFN_Color,id:6732,x:32505,y:33292,ptovrint:False,ptlb:Inner Color,ptin:_InnerColor,varname:node_6732,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:0.217;n:type:ShaderForge.SFN_Slider,id:4439,x:32764,y:33755,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_4439,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.07301889,max:1;n:type:ShaderForge.SFN_Fresnel,id:4777,x:32505,y:33488,varname:node_4777,prsc:2|EXP-4439-OUT;n:type:ShaderForge.SFN_OneMinus,id:4761,x:32698,y:33443,varname:node_4761,prsc:2|IN-4777-OUT;n:type:ShaderForge.SFN_Color,id:6127,x:32407,y:32783,ptovrint:False,ptlb:Outer Color,ptin:_OuterColor,varname:node_6127,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.258215,c3:0.5588235,c4:1;n:type:ShaderForge.SFN_Multiply,id:5180,x:32962,y:33271,varname:node_5180,prsc:2|A-6732-RGB,B-4761-OUT;n:type:ShaderForge.SFN_Slider,id:4402,x:32314,y:33061,ptovrint:False,ptlb:Edge size,ptin:_Edgesize,varname:node_4402,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.6923077,max:1;n:type:ShaderForge.SFN_Fresnel,id:6715,x:32755,y:33039,varname:node_6715,prsc:2|EXP-4402-OUT;n:type:ShaderForge.SFN_Multiply,id:128,x:32972,y:33013,varname:node_128,prsc:2|A-6127-RGB,B-6715-OUT;n:type:ShaderForge.SFN_Add,id:7438,x:33241,y:33098,varname:node_7438,prsc:2|A-128-OUT,B-5180-OUT;proporder:6732-4439-6127-4402;pass:END;sub:END;*/

Shader "Shader Forge/ShockWaveParticle" {
    Properties {
        [HDR]_InnerColor ("Inner Color", Color) = (1,1,1,0.217)
        _Opacity ("Opacity", Range(0, 1)) = 0.07301889
        [HDR]_OuterColor ("Outer Color", Color) = (0,0.258215,0.5588235,1)
        _Edgesize ("Edge size", Range(0, 1)) = 0.6923077
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _InnerColor;
            uniform float _Opacity;
            uniform float4 _OuterColor;
            uniform float _Edgesize;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 node_7438 = ((_OuterColor.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_Edgesize))+(_InnerColor.rgb*(1.0 - pow(1.0-max(0,dot(normalDirection, viewDirection)),_Opacity))));
                float3 diffuseColor = node_7438;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = node_7438;
/// Final Color:
                float3 finalColor = diffuse + emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _InnerColor;
            uniform float _Opacity;
            uniform float4 _OuterColor;
            uniform float _Edgesize;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 node_7438 = ((_OuterColor.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_Edgesize))+(_InnerColor.rgb*(1.0 - pow(1.0-max(0,dot(normalDirection, viewDirection)),_Opacity))));
                float3 diffuseColor = node_7438;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
