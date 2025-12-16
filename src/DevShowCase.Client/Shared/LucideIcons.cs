namespace DevShowcase.Client.Shared;

public static class LucideIcons
{
    // ViewBox is usually 0 0 24 24 for Lucide
    // MudBlazor expects the content of the <svg> tag path d="..." usually
    // But MudBlazor Icon accepts a string which is the SVG path "d" attribute content.
    // Lucide icons are strokes, so we need to set Style="stroke: currentColor; fill: none; stroke-width: 2;" on the MudIcon if possible,
    // OR we provide the full SVG if MudIcon supports it.
    // However, MudBlazor's default Icons.<Set>.<Name> returns a string path for internal use with a standard viewBox.
    // Standard Material icons are filled. Lucide are stroked.
    // Start with the path data.

    public const string Home = "<path d=\"M3 9l9-7 9 7v11a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2z\" /><path d=\"M9 22V12h6v10\" />";
    public const string Dashboard = "<rect width=\"7\" height=\"9\" x=\"3\" y=\"3\" rx=\"1\" /><rect width=\"7\" height=\"5\" x=\"14\" y=\"3\" rx=\"1\" /><rect width=\"7\" height=\"9\" x=\"14\" y=\"12\" rx=\"1\" /><rect width=\"7\" height=\"5\" x=\"3\" y=\"16\" rx=\"1\" />";
    public const string User = "<path d=\"M19 21v-2a4 4 0 0 0-4-4H9a4 4 0 0 0-4 4v2\" /><circle cx=\"12\" cy=\"7\" r=\"4\" />";
    public const string Briefcase = "<rect width=\"20\" height=\"14\" x=\"2\" y=\"7\" rx=\"2\" ry=\"2\" /><path d=\"M16 21V5a2 2 0 0 0-2-2h-4a2 2 0 0 0-2 2v16\" />";
    public const string Code = "<polyline points=\"16 18 22 12 16 6\" /><polyline points=\"8 6 2 12 8 18\" />";
    public const string LogOut = "<path d=\"M9 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h4\" /><polyline points=\"16 17 21 12 16 7\" /><line x1=\"21\" x2=\"9\" y1=\"12\" y2=\"12\" />";
    public const string LogIn = "<path d=\"M15 3h4a2 2 0 0 1 2 2v14a2 2 0 0 1-2 2h-4\" /><polyline points=\"10 17 15 12 10 7\" /><line x1=\"15\" x2=\"3\" y1=\"12\" y2=\"12\" />";
    public const string UserPlus = "<path d=\"M16 21v-2a4 4 0 0 0-4-4H6a4 4 0 0 0-4 4v2\" /><circle cx=\"9\" cy=\"7\" r=\"4\" /><line x1=\"19\" x2=\"19\" y1=\"8\" y2=\"14\" /><line x1=\"22\" x2=\"16\" y1=\"11\" y2=\"11\" />";
    
    // Theme
    public const string Moon = "<path d=\"M12 3a6 6 0 0 0 9 9 9 9 0 1 1-9-9Z\" />";
    public const string Sun = "<circle cx=\"12\" cy=\"12\" r=\"4\" /><path d=\"M12 2v2\" /><path d=\"M12 20v2\" /><path d=\"m4.93 4.93 1.41 1.41\" /><path d=\"m17.66 17.66 1.41 1.41\" /><path d=\"M2 12h2\" /><path d=\"M20 12h2\" /><path d=\"m6.34 17.66-1.41 1.41\" /><path d=\"m19.07 4.93-1.41 1.41\" />";

    // Custom helper to direct MudIcon to use ViewBox 0 0 24 24 and stroke instead of fill
    public const string ViewBox = "0 0 24 24";
}
