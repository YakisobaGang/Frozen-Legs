void IceSpecular_half(
    half3 Specular, 
    half Smoothess, 
    half3 Color,
    half3 WorldNormal,
    half3 WorldView,
    out half3 Out)
{
    #if SHADERGRAPH_PREVIEW
        Out = 1;
    #else
        Light light = GetMainLight();
        Smoothess = exp2(10 * Smoothess + 1);
        WorldNormal = normalize(WorldNormal);
        WorldView = SafeNormalize(WorldView);
        Out = LightingSpecular(Color, normalize(light.direction), WorldNormal, WorldView, half4(Specular, 0), Smoothess);
    #endif
}