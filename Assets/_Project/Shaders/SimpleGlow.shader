Shader "UI/SimpleGlow" {
  Properties { _Color("Tint", Color) = (1,1,1,1) }
  SubShader {
    Blend SrcAlpha One  // ���������� ����������
    Pass { SetTexture [_MainTex] { constantColor[_Color] combine constant * texture } }
  }
}