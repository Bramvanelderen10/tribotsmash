// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.30 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.30;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:33036,y:32626,varname:node_4795,prsc:2|emission-5800-OUT,alpha-798-OUT,refract-495-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32430,y:32738,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Multiply,id:798,x:32850,y:32808,varname:node_798,prsc:2|A-2053-A,B-7219-OUT;n:type:ShaderForge.SFN_ComponentMask,id:9092,x:32614,y:32931,varname:node_9092,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-1306-OUT;n:type:ShaderForge.SFN_Fresnel,id:3109,x:32308,y:33032,varname:node_3109,prsc:2|EXP-1979-OUT;n:type:ShaderForge.SFN_Multiply,id:1306,x:32457,y:32931,varname:node_1306,prsc:2|A-7322-UVOUT,B-3109-OUT;n:type:ShaderForge.SFN_Multiply,id:495,x:32866,y:32931,varname:node_495,prsc:2|A-9092-OUT,B-9780-OUT,C-4917-OUT;n:type:ShaderForge.SFN_Slider,id:9780,x:32562,y:33161,ptovrint:False,ptlb:Refraction,ptin:_Refraction,varname:node_9780,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1196581,max:1;n:type:ShaderForge.SFN_TexCoord,id:7322,x:32282,y:32845,varname:node_7322,prsc:2,uv:0;n:type:ShaderForge.SFN_Vector1,id:1979,x:32308,y:33193,varname:node_1979,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:7219,x:32691,y:32855,varname:node_7219,prsc:2,v1:0;n:type:ShaderForge.SFN_ComponentMask,id:4917,x:32711,y:32690,varname:node_4917,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-2053-RGB;n:type:ShaderForge.SFN_Color,id:9930,x:32481,y:32424,ptovrint:False,ptlb:node_9930,ptin:_node_9930,varname:node_9930,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:5800,x:32866,y:32578,varname:node_5800,prsc:2|A-9930-RGB,B-2053-RGB;proporder:9780-9930;pass:END;sub:END;*/

Shader "Shader Forge/RefractionParticle" {
    Properties {
        _Refraction ("Refraction", Range(0, 1)) = 0.1196581
        _node_9930 ("node_9930", Color) = (0.5,0.5,0.5,1)
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
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform float _Refraction;
            uniform float4 _node_9930;
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
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
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
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + ((i.uv0*pow(1.0-max(0,dot(normalDirection, viewDirection)),1.0)).rg*_Refraction*i.vertexColor.rgb.rg);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
////// Emissive:
                float3 emissive = (_node_9930.rgb*i.vertexColor.rgb);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(lerp(sceneColor.rgb, finalColor,(i.vertexColor.a*0.0)),1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
