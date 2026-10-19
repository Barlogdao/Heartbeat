Shader "Custom/OutlineSimple"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (0,1,0,1)
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineWidth ("Outline Width", Range(0.0, 0.1)) = 0.03
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        Cull Off
        ZWrite On

        // 1. Сначала рисуем основной шар (без смещения)
        Pass
        {
            Name "BASE"
            Tags { "LightMode"="UniversalForward" }
            CGPROGRAM
            #pragma vertex vertBase
            #pragma fragment fragBase
            #include "UnityCG.cginc"

            float4 _BaseColor;

            struct appdataBase
            {
                float4 vertex : POSITION;
            };

            struct v2fBase
            {
                float4 pos : SV_POSITION;
            };

            v2fBase vertBase(appdataBase v)
            {
                v2fBase o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 fragBase(v2fBase i) : SV_Target
            {
                return _BaseColor;
            }
            ENDCG
        }

        // 2. Потом рисуем аутлайн (со смещением по нормали)
        Pass
        {
            Name "OUTLINE"
            Tags { "LightMode"="UniversalForward" }
            ZWrite On
            // Важно: очередь выше, чем у BASE, чтобы рисоваться поверх
            Tags { "Queue"="Geometry+1" }
            CGPROGRAM
            #pragma vertex vertOutline
            #pragma fragment fragOutline
            #include "UnityCG.cginc"

            float4 _OutlineColor;
            float _OutlineWidth;

            struct appdataOutline
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2fOutline
            {
                float4 pos : SV_POSITION;
            };

            v2fOutline vertOutline(appdataOutline v)
            {
                v2fOutline o;
                float3 offset = v.normal * _OutlineWidth;
                o.pos = UnityObjectToClipPos(v.vertex + offset);
                return o;
            }

            fixed4 fragOutline(v2fOutline i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }
    }
}
