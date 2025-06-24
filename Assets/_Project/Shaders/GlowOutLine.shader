Shader "UI/OutlineGlow"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)
        _OutlineColor("Outline Color", Color) = (1,0.5,0,1)
        _OutlineWidth("Outline Width", Range(0, 0.2)) = 0.02
        _GlowPower("Glow Power", Range(1, 10)) = 3
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float2 texcoord : TEXCOORD0;
                float4 color    : COLOR;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                float2 texcoord : TEXCOORD0;
                float4 color    : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainTex_TexelSize;
            fixed4 _Color;
            fixed4 _OutlineColor;
            float _OutlineWidth;
            float _GlowPower;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.color = v.color * _Color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Основной цвет пикселя
                fixed4 col = tex2D(_MainTex, i.texcoord) * i.color;
                
                // Если пиксель полностью прозрачный - пропускаем расчеты
                if (col.a < 0.01) return col;
                
                // Определение краев
                float alpha = 0;
                float2 offsets[8] = {
                    float2(-1, -1), float2(0, -1), float2(1, -1),
                    float2(-1, 0),                 float2(1, 0),
                    float2(-1, 1),  float2(0, 1),  float2(1, 1)
                };
                
                [unroll]
                for (int j = 0; j < 8; j++) {
                    float2 offset = offsets[j] * _OutlineWidth * _MainTex_TexelSize.xy;
                    alpha += tex2D(_MainTex, i.texcoord + offset).a;
                }
                
                // Нормализация и усиление эффекта
                alpha = saturate(alpha * _GlowPower / 8);
                
                // Смешивание цветов
                fixed4 glow = _OutlineColor * alpha * (1 - col.a);
                col.rgb = lerp(col.rgb, _OutlineColor.rgb, alpha * _OutlineColor.a);
                col.a = max(col.a, alpha);
                
                return col;
            }
            ENDCG
        }
    }
}
