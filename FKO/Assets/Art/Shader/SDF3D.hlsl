#ifndef SDF3D_INCLUDED
#define SDF3D_INCLUDED

void SdBox_float( float3 p, float3 b ,out float output)
{
  float3 q = abs(p) - b;
  output =  length(max(q,0.0)) + min(max(q.x,max(q.y,q.z)),0.0);
}

void SdRoundBox_float( float3 p, float3 b, float r , out float output)
{
  float3 q = abs(p) - b;
  output =  length(max(q,0.0)) + min(max(q.x,max(q.y,q.z)),0.0) - r;
}

#endif 