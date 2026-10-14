namespace SanaeeUI.Client.Theme;

// -----------------------------------------------------------------------------
// SanaeeBrand — SINGLE SOURCE OF TRUTH for the whole look & feel.
//
// To re-skin the entire application you only edit the values in this file:
//   * brand colors (light + dark) live in Light / Dark below
//   * shape + typography defaults live in the constants at the top
//
// SanaeeTheme turns these tokens into (a) the MudBlazor theme and
// (b) the CSS custom properties (--sanaee-*) consumed by sanaee.css and the
// shared components. No color is hard-coded anywhere else.
// -----------------------------------------------------------------------------
public static class SanaeeBrand
{
    // --- Shape / typography -------------------------------------------------
    public const string FontFamily = "Roboto, 'Segoe UI', system-ui, sans-serif";
    public const string BorderRadius = "12px";
    public const string CardRadius = "16px";

    // --- Palette name (shown nowhere, kept for future multi-theme support) --
    public const string Name = "Egyptian Nile";

    // --- Light theme --------------------------------------------------------
    public static readonly SanaeeColorSet Light = new()
    {
        Primary = "#0B6E6E",      // Nile teal
        OnPrimary = "#FFFFFF",
        Secondary = "#C9A227",    // Antique gold
        OnSecondary = "#2A2200",
        Tertiary = "#1E5FA8",     // Lapis
        OnTertiary = "#FFFFFF",
        Background = "#F5F0E6",   // Warm sand
        Surface = "#FFFFFF",
        Drawer = "#FFFFFF",
        Appbar = "#0B6E6E",
        AppbarText = "#FFFFFF",
        TextPrimary = "#211E19",
        TextSecondary = "#6B6355",
        Border = "#E7DFCF",
        Success = "#2E7D32",
        Warning = "#B26B00",
        Error = "#C0392B",
        Info = "#1E5FA8",
        Gold = "#C9A227",
        Sand = "#F5F0E6"
    };

    // --- Dark theme ---------------------------------------------------------
    public static readonly SanaeeColorSet Dark = new()
    {
        Primary = "#35BDBD",
        OnPrimary = "#04302F",
        Secondary = "#E3C766",
        OnSecondary = "#2A2200",
        Tertiary = "#5C8FE0",
        OnTertiary = "#06182F",
        Background = "#0E1214",
        Surface = "#161B1D",
        Drawer = "#12171A",
        Appbar = "#0B2B2B",
        AppbarText = "#EAF7F7",
        TextPrimary = "#EDE7DA",
        TextSecondary = "#A99F8C",
        Border = "#2A2F31",
        Success = "#3DCB6C",
        Warning = "#E3A93E",
        Error = "#FF6B5C",
        Info = "#5C8FE0",
        Gold = "#E3C766",
        Sand = "#0E1214"
    };
}

// A flat, serializable description of every color token the UI needs.
// Keeping it flat (instead of leaning on MudBlazor types) means the same tokens
// drive both the Mud theme and the CSS variables.
public sealed record SanaeeColorSet
{
    public required string Primary { get; init; }
    public required string OnPrimary { get; init; }
    public required string Secondary { get; init; }
    public required string OnSecondary { get; init; }
    public required string Tertiary { get; init; }
    public required string OnTertiary { get; init; }
    public required string Background { get; init; }
    public required string Surface { get; init; }
    public required string Drawer { get; init; }
    public required string Appbar { get; init; }
    public required string AppbarText { get; init; }
    public required string TextPrimary { get; init; }
    public required string TextSecondary { get; init; }
    public required string Border { get; init; }
    public required string Success { get; init; }
    public required string Warning { get; init; }
    public required string Error { get; init; }
    public required string Info { get; init; }
    public required string Gold { get; init; }
    public required string Sand { get; init; }
}
