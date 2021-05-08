// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.30 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.30;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:2,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32957,y:32689,varname:node_3138,prsc:2|diff-1016-OUT,emission-4464-OUT,transm-3491-OUT,lwrap-3491-OUT,alpha-4777-OUT,refract-2867-OUT,voffset-5324-OUT;n:type:ShaderForge.SFN_Fresnel,id:298,x:32691,y:32477,varname:node_298,prsc:2|EXP-8368-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8368,x:32495,y:32499,ptovrint:False,ptlb:node_8368,ptin:_node_8368,varname:node_8368,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_Color,id:3199,x:32372,y:32565,ptovrint:False,ptlb:Diff,ptin:_Diff,varname:node_3199,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:0;n:type:ShaderForge.SFN_ValueProperty,id:3491,x:32755,y:32887,ptovrint:False,ptlb:Edge Lighting,ptin:_EdgeLighting,varname:node_3491,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Multiply,id:1016,x:32755,y:32658,varname:node_1016,prsc:2|A-3199-RGB,B-298-OUT;n:type:ShaderForge.SFN_Slider,id:4439,x:32075,y:33074,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_4439,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Fresnel,id:4777,x:32435,y:32932,varname:node_4777,prsc:2|EXP-4439-OUT;n:type:ShaderForge.SFN_Multiply,id:2867,x:32629,y:33062,varname:node_2867,prsc:2|A-9454-OUT,B-2849-OUT;n:type:ShaderForge.SFN_Slider,id:9454,x:32683,y:33385,ptovrint:False,ptlb:Refraction,ptin:_Refraction,varname:node_9454,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_TexCoord,id:7807,x:30841,y:32440,varname:node_7807,prsc:2,uv:0;n:type:ShaderForge.SFN_ComponentMask,id:8639,x:31214,y:32553,varname:node_8639,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-7807-V;n:type:ShaderForge.SFN_Sin,id:3736,x:31761,y:32757,varname:node_3736,prsc:2|IN-7713-OUT;n:type:ShaderForge.SFN_Multiply,id:7713,x:31689,y:32486,varname:node_7713,prsc:2|A-5329-OUT,B-9716-OUT,C-1680-OUT;n:type:ShaderForge.SFN_RemapRange,id:3931,x:31698,y:32985,varname:node_3931,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-3736-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5329,x:31251,y:32343,ptovrint:False,ptlb:PulseAmount,ptin:_PulseAmount,varname:node_5329,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_Clamp01,id:7532,x:31089,y:33533,varname:node_7532,prsc:2|IN-3931-OUT;n:type:ShaderForge.SFN_Tau,id:1680,x:31259,y:33042,varname:node_1680,prsc:2;n:type:ShaderForge.SFN_Color,id:1819,x:31533,y:33379,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_1819,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.8344831,c3:1,c4:1;n:type:ShaderForge.SFN_Color,id:3488,x:31533,y:33628,ptovrint:False,ptlb:Second COlor,ptin:_SecondCOlor,varname:node_3488,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.8758622,c3:1,c4:1;n:type:ShaderForge.SFN_Lerp,id:4464,x:31882,y:33449,varname:node_4464,prsc:2|A-1819-RGB,B-3488-RGB,T-7532-OUT;n:type:ShaderForge.SFN_Time,id:2454,x:30629,y:32734,varname:node_2454,prsc:2;n:type:ShaderForge.SFN_Add,id:9716,x:31243,y:32817,varname:node_9716,prsc:2|A-8639-OUT,B-7218-OUT;n:type:ShaderForge.SFN_Divide,id:4449,x:30830,y:32882,varname:node_4449,prsc:2|A-2454-T,B-5956-OUT;n:type:ShaderForge.SFN_Vector1,id:5956,x:30550,y:33012,varname:node_5956,prsc:2,v1:10;n:type:ShaderForge.SFN_NormalVector,id:9280,x:31673,y:33859,prsc:2,pt:False;n:type:ShaderForge.SFN_Multiply,id:5324,x:32031,y:33740,varname:node_5324,prsc:2|A-7532-OUT,B-9280-OUT,C-739-OUT;n:type:ShaderForge.SFN_Vector1,id:739,x:31887,y:33985,varname:node_739,prsc:2,v1:0.03;n:type:ShaderForge.SFN_Color,id:9273,x:32200,y:33478,ptovrint:False,ptlb:RefractionColor,ptin:_RefractionColor,varname:node_9273,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_ComponentMask,id:2849,x:32377,y:33343,varname:node_2849,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-9273-RGB;n:type:ShaderForge.SFN_Slider,id:5901,x:30493,y:33214,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_5901,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5410391,max:1;n:type:ShaderForge.SFN_RemapRange,id:5789,x:30810,y:33139,varname:node_5789,prsc:2,frmn:0,frmx:1,tomn:1,tomx:10|IN-5901-OUT;n:type:ShaderForge.SFN_Multiply,id:7218,x:30983,y:33053,varname:node_7218,prsc:2|A-4449-OUT,B-5789-OUT;proporder:8368-3199-3491-4439-9454-5329-1819-3488-9273-5901;pass:END;sub:END;*/

Shader "Shader Forge/Shield" {
    Properties {
        _node_8368 ("node_8368", Float ) = 4
        _Diff ("Diff", Color) = (0.5,0.5,0.5,0)
        _EdgeLighting ("Edge Lighting", Float ) = 2
        _Opacity ("Opacity", Range(0, 1)) = 1
        _Refraction ("Refraction", Range(0, 1)) = 0
        _PulseAmount ("PulseAmount", Float ) = 4
        [HDR]_Color ("Color", Color) = (0,0.8344831,1,1)
        [HDR]_SecondCOlor ("Second COlor", Color) = (0,0.8758622,1,1)
        _RefractionColor ("RefractionColor", Color) = (0.5,0.5,0.5,1)
        _Speed ("Speed", Range(0, 1)) = 0.5410391
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
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform float4 _TimeEditor;
            uniform float _node_8368;
            uniform float4 _Diff;
            uniform float _EdgeLighting;
            uniform float _Opacity;
            uniform float _Refraction;
            uniform float _PulseAmount;
            uniform float4 _Color;
            uniform float4 _SecondCOlor;
            uniform float4 _RefractionColor;
            uniform float _Speed;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                float4 screenPos : TEXCOORD7;
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD8;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float4 node_2454 = _Time + _TimeEditor;
                float node_7532 = saturate((sin((_PulseAmount*(o.uv0.g.r+((node_2454.g/10.0)*(_Speed*9.0+1.0)))*6.28318530718))*0.5+0.5));
                v.vertex.xyz += (node_7532*v.normal*0.03);
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
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (_Refraction*_RefractionColor.rgb.rg);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - 0;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 w = float3(_EdgeLighting,_EdgeLighting,_EdgeLighting)*0.5; // Light wrapping
                float3 NdotLWrap = NdotL * ( 1.0 - w );
                float3 forwardLight = max(float3(0.0,0.0,0.0), NdotLWrap + w );
                float3 backLight = max(float3(0.0,0.0,0.0), -NdotLWrap + w ) * float3(_EdgeLighting,_EdgeLighting,_EdgeLighting);
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = (forwardLight+backLight) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float3 diffuseColor = (_Diff.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_node_8368));
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 node_2454 = _Time + _TimeEditor;
                float node_7532 = saturate((sin((_PulseAmount*(i.uv0.g.r+((node_2454.g/10.0)*(_Speed*9.0+1.0)))*6.28318530718))*0.5+0.5));
                float3 emissive = lerp(_Color.rgb,_SecondCOlor.rgb,node_7532);
/// Final Color:
                float3 finalColor = diffuse + emissive;
                return fixed4(lerp(sceneColor.rgb, finalColor,pow(1.0-max(0,dot(normalDirection, viewDirection)),_Opacity)),1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _PulseAmount;
            uniform float _Speed;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float2 uv1 : TEXCOORD2;
                float2 uv2 : TEXCOORD3;
                float4 posWorld : TEXCOORD4;
                float3 normalDir : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_2454 = _Time + _TimeEditor;
                float node_7532 = saturate((sin((_PulseAmount*(o.uv0.g.r+((node_2454.g/10.0)*(_Speed*9.0+1.0)))*6.28318530718))*0.5+0.5));
                v.vertex.xyz += (node_7532*v.normal*0.03);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _node_8368;
            uniform float4 _Diff;
            uniform float _PulseAmount;
            uniform float4 _Color;
            uniform float4 _SecondCOlor;
            uniform float _Speed;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_2454 = _Time + _TimeEditor;
                float node_7532 = saturate((sin((_PulseAmount*(o.uv0.g.r+((node_2454.g/10.0)*(_Speed*9.0+1.0)))*6.28318530718))*0.5+0.5));
                v.vertex.xyz += (node_7532*v.normal*0.03);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 node_2454 = _Time + _TimeEditor;
                float node_7532 = saturate((sin((_PulseAmount*(i.uv0.g.r+((node_2454.g/10.0)*(_Speed*9.0+1.0)))*6.28318530718))*0.5+0.5));
                o.Emission = lerp(_Color.rgb,_SecondCOlor.rgb,node_7532);
                
                float3 diffColor = (_Diff.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_node_8368));
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
