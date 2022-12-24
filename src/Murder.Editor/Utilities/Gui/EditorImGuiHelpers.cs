﻿using Murder.Utilities;

namespace Murder.Editor.ImGuiExtended
{
    internal static class EditorImGuiHelpers
    {
        public static int WithDpi(this int value) =>
            Calculator.RoundToInt(value * Architect.EditorSettings.DPI / 100f);

        public static float WithDpi(this float value) => value * Architect.EditorSettings.DPI / 100f;
        
        public static System.Numerics.Vector2 WithDpi(this System.Numerics.Vector2 value) =>
            value * Architect.EditorSettings.DPI / 100f;
    }
}
