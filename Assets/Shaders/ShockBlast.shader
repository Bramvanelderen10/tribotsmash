// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.30 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.30;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:2,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33344,y:32777,varname:node_3138,prsc:2|emission-7438-OUT;n:type:ShaderForge.SFN_Color,id:6732,x:31746,y:32327,ptovrint:False,ptlb:Inner Color,ptin:_InnerColor,varname:node_6732,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.5514706,c3:0.5514706,c4:0.217;n:type:ShaderForge.SFN_Slider,id:4439,x:32645,y:33321,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_4439,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.07301889,max:1;n:type:ShaderForge.SFN_Fresnel,id:4777,x:32421,y:33117,varname:node_4777,prsc:2|EXP-4439-OUT;n:type:ShaderForge.SFN_TexCoord,id:2104,x:31261,y:32984,varname:node_2104,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:1217,x:31809,y:33085,varname:node_1217,prsc:2,spu:0.1,spv:0.1|UVIN-2104-UVOUT,DIST-1654-OUT;n:type:ShaderForge.SFN_Time,id:4347,x:31279,y:33159,varname:node_4347,prsc:2;n:type:ShaderForge.SFN_Slider,id:7493,x:31122,y:33377,ptovrint:False,ptlb:RotationSpeed,ptin:_RotationSpeed,varname:node_7493,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.6239316,max:1;n:type:ShaderForge.SFN_RemapRange,id:3867,x:31453,y:33365,varname:node_3867,prsc:2,frmn:0,frmx:1,tomn:1,tomx:10|IN-7493-OUT;n:type:ShaderForge.SFN_OneMinus,id:4761,x:32579,y:33009,varname:node_4761,prsc:2|IN-4777-OUT;n:type:ShaderForge.SFN_Color,id:6127,x:31842,y:32782,ptovrint:False,ptlb:OuterColor,ptin:_OuterColor,varname:node_6127,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.3346854,c3:0.8823529,c4:1;n:type:ShaderForge.SFN_Multiply,id:5180,x:32674,y:32902,varname:node_5180,prsc:2|A-8901-OUT,B-4761-OUT;n:type:ShaderForge.SFN_Slider,id:4402,x:31875,y:32986,ptovrint:False,ptlb:OuterColorWidth,ptin:_OuterColorWidth,varname:node_4402,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.6923077,max:1;n:type:ShaderForge.SFN_Fresnel,id:6715,x:32104,y:32819,varname:node_6715,prsc:2|EXP-4402-OUT;n:type:ShaderForge.SFN_Multiply,id:128,x:32848,y:32579,varname:node_128,prsc:2|A-7068-OUT,B-6715-OUT;n:type:ShaderForge.SFN_Add,id:7438,x:33139,y:32528,varname:node_7438,prsc:2|A-128-OUT,B-5180-OUT;n:type:ShaderForge.SFN_Tex2d,id:504,x:32046,y:33147,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_504,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e08c295755c0885479ad19f518286ff2,ntxv:3,isnm:False|UVIN-1217-UVOUT;n:type:ShaderForge.SFN_Add,id:7068,x:32706,y:32642,varname:node_7068,prsc:2|A-6127-RGB,B-504-RGB;n:type:ShaderForge.SFN_Multiply,id:8901,x:32194,y:32615,varname:node_8901,prsc:2|A-6732-RGB,B-504-RGB;n:type:ShaderForge.SFN_Multiply,id:1654,x:31539,y:33144,varname:node_1654,prsc:2|A-4347-T,B-3867-OUT;proporder:6732-4439-7493-6127-4402-504;pass:END;sub:END;*/

Shader "Shader Forge/ShockBlast" {
    Properties {
        [HDR]_InnerColor ("Inner Color", Color) = (1,0.5514706,0.5514706,0.217)
        _Opacity ("Opacity", Range(0, 1)) = 0.07301889
        _RotationSpeed ("RotationSpeed", Range(0, 1)) = 0.6239316
        [HDR]_OuterColor ("OuterColor", Color) = (0,0.3346854,0.8823529,1)
        _OuterColorWidth ("OuterColorWidth", Range(0, 1)) = 0.6923077
        _Texture ("Texture", 2D) = "bump" {}
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
            uniform float4 _TimeEditor;
            uniform float4 _InnerColor;
            uniform float _Opacity;
            uniform float _RotationSpeed;
            uniform float4 _OuterColor;
            uniform float _OuterColorWidth;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 node_4347 = _Time + _TimeEditor;
                float2 node_1217 = (i.uv0+(node_4347.g*(_RotationSpeed*9.0+1.0))*float2(0.1,0.1));
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_1217, _Texture));
                float3 emissive = (((_OuterColor.rgb+_Texture_var.rgb)*pow(1.0-max(0,dot(normalDirection, viewDirection)),_OuterColorWidth))+((_InnerColor.rgb*_Texture_var.rgb)*(1.0 - pow(1.0-max(0,dot(normalDirection, viewDirection)),_Opacity))));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
