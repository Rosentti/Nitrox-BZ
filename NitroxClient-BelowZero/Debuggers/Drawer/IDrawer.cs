using System;

namespace NitroxClient_BelowZero.Debuggers.Drawer;

public interface IDrawer
{
    public Type[] ApplicableTypes { get; }
    public void Draw(object target);
}

public interface IStructDrawer
{
    public Type[] ApplicableTypes { get; }
    public object Draw(object target);
}
