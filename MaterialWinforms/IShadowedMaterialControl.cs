using System.Drawing.Drawing2D;

namespace MaterialWinforms
{
    interface IShadowedMaterialControl : IMaterialControl
    {
        int Depth { get; set; }
        int Elevation { get; set; }

        GraphicsPath ShadowBorder { get; set; }
        MaterialSkinManager SkinManager { get; }
        MouseState MouseState { get; set; }

    }

    public enum MouseState
    {
        HOVER,
        DOWN,
        OUT
    }
}
