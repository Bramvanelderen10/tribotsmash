// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.34 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.34;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33883,y:32859,varname:node_3138,prsc:2|emission-1886-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32909,y:33192,ptovrint:False,ptlb:Color2,ptin:_Color2,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.8154159,c3:0.04411763,c4:1;n:type:ShaderForge.SFN_Tex2d,id:9508,x:32550,y:33032,ptovrint:False,ptlb:Color2Tex,ptin:_Color2Tex,varname:node_9508,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a45287b3fb672ad4392282d21aad554f,ntxv:3,isnm:False;n:type:ShaderForge.SFN_Multiply,id:5551,x:33184,y:33069,varname:node_5551,prsc:2|A-472-OUT,B-7241-RGB;n:type:ShaderForge.SFN_Slider,id:675,x:32459,y:33392,ptovrint:False,ptlb:Color2Strength,ptin:_Color2Strength,varname:node_675,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:472,x:32785,y:32991,varname:node_472,prsc:2|A-9508-RGB,B-675-OUT;n:type:ShaderForge.SFN_OneMinus,id:6068,x:32966,y:32883,varname:node_6068,prsc:2|IN-472-OUT;n:type:ShaderForge.SFN_Color,id:382,x:32936,y:32693,ptovrint:False,ptlb:Color1,ptin:_Color1,varname:node_382,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.9724138,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:2142,x:33227,y:32756,varname:node_2142,prsc:2|A-382-RGB,B-6068-OUT;n:type:ShaderForge.SFN_Add,id:6900,x:33388,y:32936,varname:node_6900,prsc:2|A-2142-OUT,B-5551-OUT;n:type:ShaderForge.SFN_Multiply,id:1886,x:33667,y:33055,varname:node_1886,prsc:2|A-6900-OUT,B-5824-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5824,x:33404,y:33240,ptovrint:False,ptlb:ColorIntensity,ptin:_ColorIntensity,varname:node_5824,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:7241-9508-675-382-5824;pass:END;sub:END;*/

Shader "Shader Forge/SphereShader" {
    Properties {
        [HDR]_Color2 ("Color2", Color) = (1,0.8154159,0.04411763,1)
        _Color2Tex ("Color2Tex", 2D) = "bump" {}
        _Color2Strength ("Color2Strength", Range(0, 1)) = 1
        [HDR]_Color1 ("Color1", Color) = (1,0.9724138,0,1)
        _ColorIntensity ("ColorIntensity", Float ) = 1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Color2;
            uniform sampler2D _Color2Tex; uniform float4 _Color2Tex_ST;
            uniform float _Color2Strength;
            uniform float4 _Color1;
            uniform float _ColorIntensity;
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
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 _Color2Tex_var = tex2D(_Color2Tex,TRANSFORM_TEX(i.uv0, _Color2Tex));
                float3 node_472 = (_Color2Tex_var.rgb*_Color2Strength);
                float3 emissive = (((_Color1.rgb*(1.0 - node_472))+(node_472*_Color2.rgb))*_ColorIntensity);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
