# Task 044 — Implement Post-Processing Stack

## Description
Create a post-processing pipeline for screen-space effects.

## Steps
1. Create full-screen quad for post-processing passes
2. Implement framebuffer object (FBO) for offscreen rendering
3. Create PostProcessStack with ordered pass list
4. Implement Bloom pass: bright-pass → blur → combine
5. Implement Color Grading pass with LUT
6. Implement fade-to-black transition pass
7. Support enable/disable per-pass (based on quality settings)
8. Add performance tracking per pass (GPU time)

## Files to Create
- src/MarioEngine.Core/Graphics/PostProcessing/PostProcessStack.cs
- src/MarioEngine.Core/Graphics/PostProcessing/BloomPass.cs
- src/MarioEngine.Core/Graphics/PostProcessing/ColorGradingPass.cs
- src/MarioEngine.Core/Graphics/PostProcessing/FadePass.cs
- src/MarioEngine.Core/Graphics/PostProcessing/FrameBuffer.cs

## Acceptance Criteria
- Bloom adds glow to bright areas
- Fade transition works (in/out)
- Color grading applies LUT correctly
- Disabling passes recovers frame time
- Low quality setting disables all passes
