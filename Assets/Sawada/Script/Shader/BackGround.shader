Shader "MyShader/BackGround"
{
	Properties
	{
		_MainColor("Color", Color) = (1,1,1,1)
		_Tex("Texture", 2D) = "white"{}
		_alpha("Alpha", Range(0,1)) = 0
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
		sampler2D _Tex;
		half _alpha;
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_Tex, IN.uv_Tex) * _MainColor;
			o.Albedo = c;
			o.Alpha = _alpha;
		}
		ENDCG
	}
}