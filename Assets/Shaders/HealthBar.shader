// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.30 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.30;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32968,y:32794,varname:node_3138,prsc:2|emission-6612-OUT,clip-2786-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32650,y:32310,ptovrint:False,ptlb:Color Empty,ptin:_ColorEmpty,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_TexCoord,id:3450,x:31569,y:32639,varname:node_3450,prsc:2,uv:0;n:type:ShaderForge.SFN_ComponentMask,id:8984,x:31894,y:32762,varname:node_8984,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-3450-V;n:type:ShaderForge.SFN_Floor,id:232,x:32380,y:32889,varname:node_232,prsc:2|IN-8-OUT;n:type:ShaderForge.SFN_Multiply,id:2786,x:32643,y:32845,varname:node_2786,prsc:2|A-4500-OUT,B-232-OUT,C-475-OUT;n:type:ShaderForge.SFN_Slider,id:689,x:31647,y:32996,ptovrint:False,ptlb:Bar Floor,ptin:_BarFloor,varname:node_689,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.66,max:1;n:type:ShaderForge.SFN_Slider,id:1184,x:31725,y:33261,ptovrint:False,ptlb:Bar Ceiling,ptin:_BarCeiling,varname:node_1184,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.33,max:1;n:type:ShaderForge.SFN_OneMinus,id:6821,x:32449,y:33078,varname:node_6821,prsc:2|IN-6657-OUT;n:type:ShaderForge.SFN_Ceil,id:4500,x:32588,y:33013,varname:node_4500,prsc:2|IN-6821-OUT;n:type:ShaderForge.SFN_ComponentMask,id:1156,x:31902,y:32540,varname:node_1156,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-3450-U;n:type:ShaderForge.SFN_OneMinus,id:8243,x:32151,y:32550,varname:node_8243,prsc:2|IN-1156-OUT;n:type:ShaderForge.SFN_Slider,id:8919,x:32179,y:32762,ptovrint:False,ptlb:Health,ptin:_Health,varname:node_8919,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Floor,id:475,x:32462,y:32310,varname:node_475,prsc:2|IN-6246-OUT;n:type:ShaderForge.SFN_Add,id:6246,x:32407,y:32480,varname:node_6246,prsc:2|A-8243-OUT,B-8919-OUT;n:type:ShaderForge.SFN_Color,id:1517,x:32650,y:32502,ptovrint:False,ptlb:Color Full,ptin:_ColorFull,varname:node_1517,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:0.04827595,c4:1;n:type:ShaderForge.SFN_Lerp,id:6612,x:32975,y:32560,varname:node_6612,prsc:2|A-7241-RGB,B-1517-RGB,T-8919-OUT;n:type:ShaderForge.SFN_Add,id:6657,x:32204,y:33199,varname:node_6657,prsc:2|A-8984-OUT,B-1184-OUT;n:type:ShaderForge.SFN_Add,id:8,x:32162,y:32911,varname:node_8,prsc:2|A-8984-OUT,B-689-OUT;proporder:7241-689-1184-8919-1517;pass:END;sub:END;*/

Shader "Shader Forge/HealthBar" {
    Properties {
        [HDR]_ColorEmpty ("Color Empty", Color) = (1,0,0,1)
        _BarFloor ("Bar Floor", Range(0, 1)) = 0.66
        _BarCeiling ("Bar Ceiling", Range(0, 1)) = 0.33
        _Health ("Health", Range(0, 1)) = 1
        [HDR]_ColorFull ("Color Full", Color) = (0,1,0.04827595,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _ColorEmpty;
            uniform float _BarFloor;
            uniform float _BarCeiling;
            uniform float _Health;
            uniform float4 _ColorFull;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float node_8984 = i.uv0.g.r;
                clip((ceil((1.0 - (node_8984+_BarCeiling)))*floor((node_8984+_BarFloor))*floor(((1.0 - i.uv0.r.r)+_Health))) - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = lerp(_ColorEmpty.rgb,_ColorFull.rgb,_Health);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
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
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float _BarFloor;
            uniform float _BarCeiling;
            uniform float _Health;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float node_8984 = i.uv0.g.r;
                clip((ceil((1.0 - (node_8984+_BarCeiling)))*floor((node_8984+_BarFloor))*floor(((1.0 - i.uv0.r.r)+_Health))) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
