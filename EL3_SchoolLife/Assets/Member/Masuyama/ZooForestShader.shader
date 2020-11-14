
Shader "Custom/ZooForest" {
	Properties{
		// Diffuse texture
		_MainTex("Base (RGB)", 2D) = "white" {}
	// Degree of curvature
	_Curvature("Curvature", Float) = 0.001
	}
		SubShader{
		Blend SrcAlpha OneMinusSrcAlpha
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM

#pragma surface surf Lambert vertex:vert addshadow
		# pragma multi_compile FANCY_STUFF_OFF FANCY_STUFF_ON

		uniform sampler2D _MainTex;
	uniform float _Curvature;


	struct Input {
		float2 uv_MainTex;
	};

	// This is where the curvature is applied
	void vert(inout appdata_full v)
	{
		// Transform the vertex coordinates from model space into world space
		float4 vv = mul(unity_ObjectToWorld, v.vertex);

		// Now adjust the coordinates to be relative to the camera position
		vv.xyz -= _WorldSpaceCameraPos.xyz;

		// Reduce the y coordinate (i.e. lower the "height") of each vertex based
		// on the square of the distance from the camera in the z axis, multiplied
		// by the chosen curvature factor
		vv = float4(0.0f, (vv.z * vv.z) * -_Curvature, 0.0f, 0.0f);

		// Now apply the offset back to the vertices in model space
		v.vertex += mul(unity_WorldToObject, vv);
	}

	// This is just a default surface shader
	void surf(Input IN, inout SurfaceOutput o) {
		half4 c = tex2D(_MainTex, IN.uv_MainTex);
		o.Albedo = c.rgb;
		clip(c.a - 0.5);
		o.Alpha = c.a;
	}
	ENDCG
	}
		// FallBack "Diffuse"
}