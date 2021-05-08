// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.30 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.30;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:2,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:34573,y:32559,varname:node_3138,prsc:2|alpha-7790-OUT,refract-8877-OUT;n:type:ShaderForge.SFN_Fresnel,id:7072,x:32560,y:32971,varname:node_7072,prsc:2|EXP-4572-OUT;n:type:ShaderForge.SFN_Slider,id:4572,x:32182,y:33074,ptovrint:False,ptlb:Refraction Fresnel,ptin:_RefractionFresnel,varname:node_4572,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_ComponentMask,id:1680,x:32472,y:32731,varname:node_1680,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-4192-RGB;n:type:ShaderForge.SFN_Vector1,id:4262,x:32811,y:32653,varname:node_4262,prsc:2,v1:1;n:type:ShaderForge.SFN_Tex2d,id:4192,x:32332,y:32562,ptovrint:False,ptlb:Refraction Texture,ptin:_RefractionTexture,varname:node_4192,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:33a9188cb13b74e7493476e4060bcb32,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:1746,x:32736,y:32853,varname:node_1746,prsc:2|A-4042-OUT,B-7072-OUT;n:type:ShaderForge.SFN_ComponentMask,id:6952,x:32891,y:32921,varname:node_6952,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-1746-OUT;n:type:ShaderForge.SFN_Divide,id:4042,x:32682,y:32693,varname:node_4042,prsc:2|A-1680-OUT,B-5837-OUT;n:type:ShaderForge.SFN_Slider,id:5754,x:32280,y:32428,ptovrint:False,ptlb:Dampen Refraction,ptin:_DampenRefraction,varname:node_5754,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.7492597,max:1;n:type:ShaderForge.SFN_RemapRange,id:5837,x:32574,y:32505,varname:node_5837,prsc:2,frmn:0,frmx:1,tomn:0,tomx:10|IN-5754-OUT;n:type:ShaderForge.SFN_Color,id:26,x:32834,y:31830,ptovrint:False,ptlb:Edge Color,ptin:_EdgeColor,varname:node_26,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.5034485,c3:1,c4:1;n:type:ShaderForge.SFN_Fresnel,id:7857,x:33110,y:32647,varname:node_7857,prsc:2|EXP-4262-OUT;n:type:ShaderForge.SFN_Multiply,id:399,x:33286,y:32277,varname:node_399,prsc:2|A-26-RGB,B-8678-OUT;n:type:ShaderForge.SFN_Multiply,id:8877,x:33559,y:32969,varname:node_8877,prsc:2|A-6952-OUT,B-9934-OUT,C-5447-OUT;n:type:ShaderForge.SFN_Slider,id:9934,x:33089,y:32800,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_9934,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:7790,x:33860,y:32706,varname:node_7790,prsc:2|A-4371-OUT,B-7606-A;n:type:ShaderForge.SFN_Fresnel,id:8678,x:33110,y:32396,varname:node_8678,prsc:2|EXP-2117-OUT;n:type:ShaderForge.SFN_Slider,id:5813,x:32456,y:32299,ptovrint:False,ptlb:ColorEdgeSize,ptin:_ColorEdgeSize,varname:node_5813,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.3103267,max:1;n:type:ShaderForge.SFN_RemapRange,id:2117,x:32852,y:32259,varname:node_2117,prsc:2,frmn:0,frmx:1,tomn:0,tomx:10|IN-5813-OUT;n:type:ShaderForge.SFN_Multiply,id:3581,x:33830,y:31867,varname:node_3581,prsc:2|A-26-RGB,B-1791-OUT;n:type:ShaderForge.SFN_Fresnel,id:1791,x:33853,y:32033,varname:node_1791,prsc:2|EXP-8789-OUT;n:type:ShaderForge.SFN_Slider,id:2445,x:33267,y:32130,ptovrint:False,ptlb:node_2445,ptin:_node_2445,varname:node_2445,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_RemapRange,id:8789,x:33615,y:32043,varname:node_8789,prsc:2,frmn:0,frmx:1,tomn:0,tomx:20|IN-2445-OUT;n:type:ShaderForge.SFN_Add,id:2012,x:33921,y:32259,varname:node_2012,prsc:2|A-3581-OUT,B-399-OUT;n:type:ShaderForge.SFN_ComponentMask,id:5447,x:34191,y:33200,varname:node_5447,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-7606-RGB;n:type:ShaderForge.SFN_VertexColor,id:7606,x:33913,y:32986,varname:node_7606,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8870,x:34218,y:32356,varname:node_8870,prsc:2|A-2012-OUT,B-7606-RGB;n:type:ShaderForge.SFN_ValueProperty,id:4371,x:33630,y:32530,ptovrint:False,ptlb:node_4371,ptin:_node_4371,varname:node_4371,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;proporder:4572-4192-5754-26-9934-5813-2445-4371;pass:END;sub:END;*/

Shader "RefractionParticle" {
    Properties {
        _RefractionFresnel ("Refraction Fresnel", Range(0, 1)) = 1
        _RefractionTexture ("Refraction Texture", 2D) = "white" {}
        _DampenRefraction ("Dampen Refraction", Range(0, 1)) = 0.7492597
        [HDR]_EdgeColor ("Edge Color", Color) = (0,0.5034485,1,1)
        _Opacity ("Opacity", Range(0, 1)) = 1
        _ColorEdgeSize ("ColorEdgeSize", Range(0, 1)) = 0.3103267
        _node_2445 ("node_2445", Range(0, 1)) = 1
        _node_4371 ("node_4371", Float ) = 0
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
            uniform sampler2D _GrabTexture;
            uniform float _RefractionFresnel;
            uniform sampler2D _RefractionTexture; uniform float4 _RefractionTexture_ST;
            uniform float _DampenRefraction;
            uniform float _Opacity;
            uniform float _node_4371;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 screenPos : TEXCOORD3;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
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
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float4 _RefractionTexture_var = tex2D(_RefractionTexture,TRANSFORM_TEX(i.uv0, _RefractionTexture));
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (((_RefractionTexture_var.rgb.rg/(_DampenRefraction*10.0+0.0))*pow(1.0-max(0,dot(normalDirection, viewDirection)),_RefractionFresnel)).rg*_Opacity*i.vertexColor.rgb.rg);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
                float3 finalColor = 0;
                return fixed4(lerp(sceneColor.rgb, finalColor,(_node_4371*i.vertexColor.a)),1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
