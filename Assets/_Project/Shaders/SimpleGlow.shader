Shader "UI/SimpleGlow" {
  Properties { _Color("Tint", Color) = (1,1,1,1) }
  SubShader {
    Blend SrcAlpha One  // Аддитивное смешивание
    Pass { SetTexture [_MainTex] { constantColor[_Color] combine constant * texture } }
  }
}