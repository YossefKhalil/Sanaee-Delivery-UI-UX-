using MudBlazor;

namespace SanaeeUI.Client.Theme;

// Builds the MudBlazor theme and the CSS custom properties from SanaeeBrand.
// Everything here is derived — never hard-code colors in components or CSS.
public static class SanaeeTheme
{
    public static MudTheme Create() => new()
    {
        PaletteLight = ToLight(SanaeeBrand.Light),
        PaletteDark = ToDark(SanaeeBrand.Dark),
        LayoutProperties = new LayoutProperties
        {
            DefaultBorderRadius = SanaeeBrand.BorderRadius,
            AppbarHeight = "64px"
        },
        Typography = new Typography
        {
            Default = new DefaultTypography
            {
                FontFamily = new[] { "Roboto", "Segoe UI", "system-ui", "sans-serif" }
            }
        }
    };

    // The set of tokens for the currently active light/dark mode.
    public static SanaeeColorSet Current(bool dark) => dark ? SanaeeBrand.Dark : SanaeeBrand.Light;

    // CSS custom properties consumed by sanaee.css and the shared components.
    // Emitted by MainLayout so the markup only ever references var(--sanaee-*).
    public static string CssVariables(SanaeeColorSet c) => $$"""
        :root {
            --sanaee-primary: {{c.Primary}};
            --sanaee-on-primary: {{c.OnPrimary}};
            --sanaee-secondary: {{c.Secondary}};
            --sanaee-on-secondary: {{c.OnSecondary}};
            --sanaee-tertiary: {{c.Tertiary}};
            --sanaee-on-tertiary: {{c.OnTertiary}};
            --sanaee-bg: {{c.Background}};
            --sanaee-surface: {{c.Surface}};
            --sanaee-drawer: {{c.Drawer}};
            --sanaee-appbar: {{c.Appbar}};
            --sanaee-appbar-text: {{c.AppbarText}};
            --sanaee-text: {{c.TextPrimary}};
            --sanaee-text-secondary: {{c.TextSecondary}};
            --sanaee-border: {{c.Border}};
            --sanaee-success: {{c.Success}};
            --sanaee-warning: {{c.Warning}};
            --sanaee-error: {{c.Error}};
            --sanaee-info: {{c.Info}};
            --sanaee-gold: {{c.Gold}};
            --sanaee-sand: {{c.Sand}};
            --sanaee-radius: {{SanaeeBrand.BorderRadius}};
            --sanaee-card-radius: {{SanaeeBrand.CardRadius}};
        }
        """;

    private static PaletteLight ToLight(SanaeeColorSet c) => new()
    {
        Primary = c.Primary,
        PrimaryContrastText = c.OnPrimary,
        Secondary = c.Secondary,
        SecondaryContrastText = c.OnSecondary,
        Tertiary = c.Tertiary,
        TertiaryContrastText = c.OnTertiary,
        Background = c.Background,
        BackgroundGray = c.Sand,
        Surface = c.Surface,
        DrawerBackground = c.Drawer,
        DrawerText = c.TextPrimary,
        DrawerIcon = c.TextSecondary,
        AppbarBackground = c.Appbar,
        AppbarText = c.AppbarText,
        TextPrimary = c.TextPrimary,
        TextSecondary = c.TextSecondary,
        ActionDefault = c.TextSecondary,
        LinesDefault = c.Border,
        LinesInputs = c.Border,
        TableLines = c.Border,
        TableStriped = $"{c.Primary}0d",
        TableHover = $"{c.Primary}14",
        Divider = c.Border,
        Success = c.Success,
        Warning = c.Warning,
        Error = c.Error,
        Info = c.Info,
        GrayLight = c.Border,
        GrayLighter = c.Sand
    };

    private static PaletteDark ToDark(SanaeeColorSet c) => new()
    {
        Primary = c.Primary,
        PrimaryContrastText = c.OnPrimary,
        Secondary = c.Secondary,
        SecondaryContrastText = c.OnSecondary,
        Tertiary = c.Tertiary,
        TertiaryContrastText = c.OnTertiary,
        Background = c.Background,
        BackgroundGray = c.Surface,
        Surface = c.Surface,
        DrawerBackground = c.Drawer,
        DrawerText = c.TextPrimary,
        DrawerIcon = c.TextSecondary,
        AppbarBackground = c.Appbar,
        AppbarText = c.AppbarText,
        TextPrimary = c.TextPrimary,
        TextSecondary = c.TextSecondary,
        ActionDefault = c.TextSecondary,
        LinesDefault = c.Border,
        LinesInputs = c.Border,
        TableLines = c.Border,
        TableStriped = "#ffffff0a",
        TableHover = "#ffffff12",
        Divider = c.Border,
        Success = c.Success,
        Warning = c.Warning,
        Error = c.Error,
        Info = c.Info,
        GrayLight = c.Border,
        GrayLighter = c.Surface
    };
}
