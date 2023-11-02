using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace Engine;

static class ResourceManager
{
    public static ContentManager Resource { get; private set; }

    public static void SetManager(ContentManager manager)
    {
        Debug.Assert(manager != null, $"parametr {nameof(manager)} should not be null.");
        Resource = manager;
    }
}
