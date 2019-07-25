Shader "MyShader/BackGround_Buble"
{
	Properties
	{
		_MainColor("Color", Color) = (1,1,1,1)
		_Tex("Texture", 2D) = "white"{}
		_alpha("Alpha", Range(0,1)) = 0
		_speed("Speed", Float) = 1
	}
		SubShader
		{
			Tags{"Queue" = "Transparent"}
			LOD 200
			CGPROGRAM
			#pragma surface surf Standard alpha:fade
			#pragma target 3.0

			struct Input
			{
				float2 uv_Tex;
			};

			fixed4 _MainColor;
			half _alpha;
			half _speed;
			sampler2D _Tex;

			void surf(Input IN, inout SurfaceOutputStandard o)
			{
				//アニメーション
				fixed2 uv = IN.uv_Tex;
				//uv += sin(IN.uv_Tex) * speed * _Time;
				//uv.x += _speed * _Time;

				fixed4 c = tex2D(_Tex, uv) * _MainColor;
				o.Albedo = c;
				o.Alpha = _alpha;
			}
			ENDCG

		}
}