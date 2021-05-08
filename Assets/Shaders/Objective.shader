// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.30 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.30;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33185,y:32763,varname:node_3138,prsc:2|normal-4451-RGB,emission-6612-OUT,clip-2786-OUT,voffset-7537-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32575,y:32352,ptovrint:False,ptlb:Color Empty,ptin:_ColorEmpty,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.01703849,c3:0.3529412,c4:1;n:type:ShaderForge.SFN_TexCoord,id:3450,x:31569,y:32639,varname:node_3450,prsc:2,uv:0;n:type:ShaderForge.SFN_ComponentMask,id:8984,x:31894,y:32762,varname:node_8984,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-3450-V;n:type:ShaderForge.SFN_Floor,id:232,x:32380,y:32889,varname:node_232,prsc:2|IN-8-OUT;n:type:ShaderForge.SFN_Multiply,id:2786,x:32643,y:32845,varname:node_2786,prsc:2|A-4500-OUT,B-232-OUT;n:type:ShaderForge.SFN_Slider,id:689,x:31647,y:32996,ptovrint:False,ptlb:Bar Floor,ptin:_BarFloor,varname:node_689,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.55,max:1;n:type:ShaderForge.SFN_Slider,id:1184,x:31725,y:33261,ptovrint:False,ptlb:Bar Ceiling,ptin:_BarCeiling,varname:node_1184,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.45,max:1;n:type:ShaderForge.SFN_OneMinus,id:6821,x:32449,y:33078,varname:node_6821,prsc:2|IN-6657-OUT;n:type:ShaderForge.SFN_Ceil,id:4500,x:32588,y:33013,varname:node_4500,prsc:2|IN-6821-OUT;n:type:ShaderForge.SFN_Slider,id:8919,x:32179,y:32762,ptovrint:False,ptlb:Health,ptin:_Health,varname:node_8919,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Color,id:1517,x:32307,y:32422,ptovrint:False,ptlb:Color Full,ptin:_ColorFull,varname:node_1517,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5073529,c2:0.7553751,c3:1,c4:1;n:type:ShaderForge.SFN_Lerp,id:6612,x:32745,y:32546,varname:node_6612,prsc:2|A-7241-RGB,B-1517-RGB,T-8919-OUT;n:type:ShaderForge.SFN_Add,id:6657,x:32204,y:33199,varname:node_6657,prsc:2|A-8984-OUT,B-1184-OUT;n:type:ShaderForge.SFN_Add,id:8,x:32162,y:32911,varname:node_8,prsc:2|A-8984-OUT,B-689-OUT;n:type:ShaderForge.SFN_TexCoord,id:7575,x:33629,y:32461,varname:node_7575,prsc:2,uv:0;n:type:ShaderForge.SFN_ComponentMask,id:7335,x:33475,y:32590,varname:node_7335,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-7575-V;n:type:ShaderForge.SFN_Sin,id:8268,x:33591,y:33269,varname:node_8268,prsc:2|IN-7359-OUT;n:type:ShaderForge.SFN_Multiply,id:7359,x:33630,y:32808,varname:node_7359,prsc:2|A-7386-OUT,B-4969-OUT,C-4627-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4969,x:33265,y:32709,ptovrint:False,ptlb:Lines,ptin:_Lines,varname:node_4969,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:5;n:type:ShaderForge.SFN_Tau,id:4627,x:33402,y:33013,varname:node_4627,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7537,x:32991,y:33234,varname:node_7537,prsc:2|A-8268-OUT,B-4302-OUT,C-1560-OUT;n:type:ShaderForge.SFN_NormalVector,id:4302,x:32760,y:33320,prsc:2,pt:False;n:type:ShaderForge.SFN_Vector1,id:1560,x:33178,y:33442,varname:node_1560,prsc:2,v1:0.03;n:type:ShaderForge.SFN_Tex2d,id:4451,x:32883,y:32275,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:node_4451,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8aec15173b59c5f4b8f9825c442dc6b4,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Time,id:5399,x:34049,y:32444,varname:node_5399,prsc:2;n:type:ShaderForge.SFN_Add,id:7386,x:33796,y:32642,varname:node_7386,prsc:2|A-7335-OUT,B-5963-OUT;n:type:ShaderForge.SFN_Multiply,id:5963,x:34129,y:32717,varname:node_5963,prsc:2|A-5399-TSL,B-4326-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4326,x:33951,y:32924,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_4326,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:5;proporder:7241-689-1184-8919-1517-4969-4451-4326;pass:END;sub:END;*/

Shader "Shader Forge/Objective" {
    Properties {
        [HDR]_ColorEmpty ("Color Empty", Color) = (0,0.01703849,0.3529412,1)
        _BarFloor ("Bar Floor", Range(0, 1)) = 0.55
        _BarCeiling ("Bar Ceiling", Range(0, 1)) = 0.45
        _Health ("Health", Range(0, 1)) = 1
        [HDR]_ColorFull ("Color Full", Color) = (0.5073529,0.7553751,1,1)
        _Lines ("Lines", Float ) = 5
        _Normal ("Normal", 2D) = "bump" {}
        _Speed ("Speed", Float ) = 5
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
            uniform float4 _TimeEditor;
            uniform float4 _ColorEmpty;
            uniform float _BarFloor;
            uniform float _BarCeiling;
            uniform float _Health;
            uniform float4 _ColorFull;
            uniform float _Lines;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform float _Speed;
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
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float4 node_5399 = _Time + _TimeEditor;
                float node_8268 = sin(((o.uv0.g.r+(node_5399.r*_Speed))*_Lines*6.28318530718));
                v.vertex.xyz += (node_8268*v.normal*0.03);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 normalLocal = _Normal_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float node_8984 = i.uv0.g.r;
                clip((ceil((1.0 - (node_8984+_BarCeiling)))*floor((node_8984+_BarFloor))) - 0.5);
////// Lighting:
////// Emissive:
                float3 node_6612 = lerp(_ColorEmpty.rgb,_ColorFull.rgb,_Health);
                float3 emissive = node_6612;
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
            uniform float4 _TimeEditor;
            uniform float _BarFloor;
            uniform float _BarCeiling;
            uniform float _Lines;
            uniform float _Speed;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_5399 = _Time + _TimeEditor;
                float node_8268 = sin(((o.uv0.g.r+(node_5399.r*_Speed))*_Lines*6.28318530718));
                v.vertex.xyz += (node_8268*v.normal*0.03);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float node_8984 = i.uv0.g.r;
                clip((ceil((1.0 - (node_8984+_BarCeiling)))*floor((node_8984+_BarFloor))) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
