/*{
  "DESCRIPTION": "A breathing RGB channel split that pulses outward from center on its own — no keyframing, just a living, faintly holographic shimmer that never fully settles.",
  "CATEGORIES": ["Guillotine", "Distortion"],
  "INPUTS": [
    { "NAME": "inputImage", "TYPE": "image" },
    { "NAME": "intensity", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.0, "MAX": 1.0 },
    { "NAME": "speed", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.1, "MAX": 3.0 }
  ]
}*/

void main() {
  vec2 uv = isf_FragNormCoord;
  vec2 dir = uv - vec2(0.5);
  float dist = length(dir);
  vec2 dirNorm = dist > 0.0001 ? dir / dist : vec2(0.0);

  // Breathing pulse: 0..1..0, so the split never fully re-aligns — it always reads as "alive".
  float pulse = 0.5 + 0.5 * sin(TIME * speed * 2.0);
  float split = intensity * 0.02 * pulse * dist;

  vec4 center = IMG_THIS_PIXEL(inputImage);
  float r = IMG_NORM_PIXEL(inputImage, uv + dirNorm * split).r;
  float b = IMG_NORM_PIXEL(inputImage, uv - dirNorm * split).b;

  gl_FragColor = vec4(r, center.g, b, center.a);
}
