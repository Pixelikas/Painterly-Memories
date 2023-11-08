// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "GGJ/Bush"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment SpriteFrag
            #pragma target 2.0
            #pragma multi_compile_instancing
            #include "UnitySprites.cginc"

			float4 _PlayerPosition;
			float _PlayerPositionInfluence;

			float rand(float2 co) {
				return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453);
			}

			v2f vert(appdata_t v) {
				v2f o = SpriteVert(v);
				o.vertex = mul(UNITY_MATRIX_M, v.vertex);

				float4 delta = o.vertex - _PlayerPosition;
				delta.w = 0;
				float4 direction = normalize(delta);
				float strength = clamp(length(delta), 0, 0.025) * _PlayerPositionInfluence;
				strength = pow(strength, 2) * 20;
				delta = direction * strength;

				o.vertex = mul(UNITY_MATRIX_VP, o.vertex) + delta;
				return o;
			}
        ENDCG
        }
    }
}
