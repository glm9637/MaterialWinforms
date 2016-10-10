namespace MaterialWinforms
{
    interface IMaterialControl
    {
        int Depth { get; set; }
        MaterialSkinManager SkinManager { get; }
        MouseState MouseState { get; set; }

        System.Drawing.Color BackColor { get; } 

    }
}
