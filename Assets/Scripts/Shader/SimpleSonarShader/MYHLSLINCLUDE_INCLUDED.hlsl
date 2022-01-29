//UNITY_SHADER_NO_UPGRADE
#ifndef MYHLSLINCLUDE_INCLUDED
#define MYHLSLINCLUDE_INCLUDED

half4 _hitPts[20];
half _Intensity[20];
//sampler2D _RingTex;

void MyFunction_float(float4 c, UnityTexture2D _RingTex, float3 worldPos, out float4 Out)
{
    half DiffFromRingCol = abs(c.r - _RingColor.r) + abs(c.b - _RingColor.b) + abs(c.g - _RingColor.g);
    //Out = float4(0,0,0,0);
    for (int i = 0; i < 20; i++)
    {
        half d = distance(_hitPts[i], worldPos); // .xyz?
        half intensity = _Intensity[i] * _RingIntensityScale; // Just take it from the main file..
        half val = (1 - (d / intensity));
        
        // Check if _time is valid
        if (d < (_Time.y - _hitPts[i].w) * _RingSpeed && d > (_Time.y - _hitPts[i].w) * _RingSpeed - _RingWidth && val > 0)
        {
            half posInRing = (d - ((_Time.y - _hitPts[i].w) * _RingSpeed - _RingWidth)) / _RingWidth;

            // Calculate predicted RGB values sampling hte texture radially.
            float angle = acos(dot(normalize(worldPos - _hitPts[i]), float3(1, 0, 0)));
            
            //float4 _RingTex = float4(0, 0, 0, 0);
            val *= tex2D(_RingTex, half2(1 - posInRing, angle));
            half3 tmp = _RingColor * val + c * (1 - val);
            
            // Determine if predicted values will be closer to the Ring color
            half tempDiffFromRingCol = abs(tmp.r - _RingColor.r) + abs(tmp.b - _RingColor.b) + abs(tmp.g - _RingColor.g);
            
            if (tempDiffFromRingCol < DiffFromRingCol)
            {
				// Update values using our predicted ones.
                DiffFromRingCol = tempDiffFromRingCol;
                //o.Albedo.r = tmp.r;
                //o.Albedo.g = tmp.g;
                //o.Albedo.b = tmp.b;
                //o.Albedo.rgb *= _RingColorIntensity;
                Out.r = tmp.r;
                Out.g = tmp.g;
                Out.b = tmp.b;
                Out.rgb *= _RingColorIntensity;
            }
        }
        
        
        
        Out = DiffFromRingCol;
    }
}

#endif //MYHLSLINCLUDE_INCLUDED