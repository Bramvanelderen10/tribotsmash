// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.30 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.30;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:2,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32957,y:32689,varname:node_3138,prsc:2|diff-1016-OUT,normal-237-RGB,emission-6732-RGB,transm-3491-OUT,lwrap-3491-OUT,alpha-4777-OUT,refract-2867-OUT;n:type:ShaderForge.SFN_Tex2d,id:237,x:32421,y:32730,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:node_237,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:62469bdfebacaa84581c08156a1e42ab,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Fresnel,id:298,x:32691,y:32477,varname:node_298,prsc:2|EXP-8368-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8368,x:32495,y:32499,ptovrint:False,ptlb:node_8368,ptin:_node_8368,varname:node_8368,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_Color,id:3199,x:32263,y:32581,ptovrint:False,ptlb:node_3199,ptin:_node_3199,varname:node_3199,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:0;n:type:ShaderForge.SFN_Tex2d,id:6178,x:32075,y:33214,ptovrint:False,ptlb:RefractionTexture,ptin:_RefractionTexture,varname:node_6178,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e08c295755c0885479ad19f518286ff2,ntxv:3,isnm:True;n:type:ShaderForge.SFN_ValueProperty,id:3491,x:32755,y:32887,ptovrint:False,ptlb:Edge Lighting,ptin:_EdgeLighting,varname:node_3491,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:1016,x:32755,y:32658,varname:node_1016,prsc:2|A-3199-RGB,B-298-OUT;n:type:ShaderForge.SFN_Color,id:6732,x:32205,y:32868,ptovrint:False,ptlb:Emission,ptin:_Emission,varname:node_6732,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.3676471,c3:0.3676471,c4:0.217;n:type:ShaderForge.SFN_Slider,id:4439,x:31747,y:33000,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_4439,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Fresnel,id:4777,x:32435,y:32932,varname:node_4777,prsc:2|EXP-8640-OUT;n:type:ShaderForge.SFN_Multiply,id:2867,x:32608,y:33031,varname:node_2867,prsc:2|A-9454-OUT,B-9219-OUT;n:type:ShaderForge.SFN_Slider,id:9454,x:32608,y:33306,ptovrint:False,ptlb:Refraction,ptin:_Refraction,varname:node_9454,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.04803206,max:1;n:type:ShaderForge.SFN_ComponentMask,id:9219,x:32291,y:33251,varname:node_9219,prsc:2,cc1:1,cc2:0,cc3:-1,cc4:-1|IN-6178-RGB;n:type:ShaderForge.SFN_RemapRange,id:8640,x:32261,y:33051,varname:node_8640,prsc:2,frmn:0,frmx:1,tomn:0,tomx:3|IN-4439-OUT;proporder:237-8368-3199-6178-3491-6732-4439-9454;pass:END;sub:END;*/

Shader "Shader Forge/PickUp" {
    Properties {
        _Normal ("Normal", 2D) = "bump" {}
        _node_8368 ("node_8368", Float ) = 4
        _node_3199 ("node_3199", Color) = (0.5,0.5,0.5,0)
        _RefractionTexture ("RefractionTexture", 2D) = "bump" {}
        _EdgeLighting ("Edge Lighting", Float ) = 1
        [HDR]_Emission ("Emission", Color) = (1,0.3676471,0.3676471,0.217)
        _Opacity ("Opacity", Range(0, 1)) = 1
        _Refraction ("Refraction", Range(0, 1)) = 0.04803206
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
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
            uniform sampler2D _GrabTexture;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform float _node_8368;
            uniform float4 _node_3199;
            uniform sampler2D _RefractionTexture; uniform float4 _RefractionTexture_ST;
            uniform float _EdgeLighting;
            uniform float4 _Emission;
            uniform float _Opacity;
            uniform float _Refraction;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 screenPos : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 normalLocal = _Normal_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 _RefractionTexture_var = UnpackNormal(tex2D(_RefractionTexture,TRANSFORM_TEX(i.uv0, _RefractionTexture)));
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (_Refraction*_RefractionTexture_var.rgb.gr);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 w = float3(_EdgeLighting,_EdgeLighting,_EdgeLighting)*0.5; // Light wrapping
                float3 NdotLWrap = NdotL * ( 1.0 - w );
                float3 forwardLight = max(float3(0.0,0.0,0.0), NdotLWrap + w );
                float3 backLight = max(float3(0.0,0.0,0.0), -NdotLWrap + w ) * float3(_EdgeLighting,_EdgeLighting,_EdgeLighting);
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = (forwardLight+backLight) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuseColor = (_node_3199.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_node_8368));
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = _Emission.rgb;
/// Final Color:
                float3 finalColor = diffuse + emissive;
                return fixed4(lerp(sceneColor.rgb, finalColor,pow(1.0-max(0,dot(normalDirection, viewDirection)),(_Opacity*3.0+0.0))),1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
