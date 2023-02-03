Shader "PG/OutLine"
{
    Properties
    {
        _Color("Outline Color", Color) = (0, 0, 0, 1)
        _Size("Size", Range(0, 1)) = 0.04                  //���������߿��
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            //��ʾ�����Ч��
            Cull Front	//�޳��������ȾЧ��
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
        // make fog work
        #pragma multi_compile_fog

        #include "UnityCG.cginc"

        struct appdata
        {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
        };
        struct v2f
        {
            float2 uv : TEXCOORD0;
            UNITY_FOG_COORDS(1)
            float4 vertex : SV_POSITION;
        };
        float4 _OutlineColor;
        float _Size;
        float4 calculatePos(float4 vertex)
        {
            float rr = length(vertex);
            if (rr > 0)
            {
                vertex.xyz = vertex.xyz * (1 + _Size);

                return vertex;
            }
            else
            {
                return vertex;
            }
        }
        v2f vert(appdata v)
        {
            v2f o;
            float4 vertex = v.vertex;
            vertex = calculatePos(vertex);
            vertex = UnityObjectToClipPos(vertex);
            o.uv = v.uv;
            o.vertex = vertex;
            UNITY_TRANSFER_FOG(o,o.vertex);
            return o;
        }
        fixed4 frag(v2f i) : SV_Target
        {
            return _OutlineColor;
        }
        ENDCG
    }
    Pass
    {
            //��ʾ���������Ч��
            Cull Back	//�޳��������ȾЧ�����˴��޳��������ʾЧ�����������һ��Pass�еı���Ч����ʾ��
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };
            float4 _OutlineColor;
            float _Size;
            //ģ�Ͷ�������������
            float4 calculatePos(float4 vertex)
            {
                float rr = length(vertex);
                if (rr > 0)
                {
                    vertex.xyz = vertex.xyz * (1 + _Size);

                    return vertex;
                }
                else
                {
                    return vertex;
                }
            }
            //�����������
            float calculateAlpha(float2 uv)
            {
                float size = _Size / (1 + _Size) * 0.5;

                if (uv.x < size || uv.x > 1 - size)
                {
                    return 1;
                }
                if (uv.y < size || uv.y > 1 - size)
                {
                    return 1;
                }
                return 0;
            }
            v2f vert(appdata v)
            {
                v2f o;
                float4 vertex = v.vertex;
                //��������
                vertex = calculatePos(vertex);
                //����ת��
                vertex = UnityObjectToClipPos(vertex);
                o.uv = v.uv;
                o.vertex = vertex;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = _OutlineColor;
                col.a = calculateAlpha(i.uv);
                return col;
            }
            ENDCG
        }
}
}
