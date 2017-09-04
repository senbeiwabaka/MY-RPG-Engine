using SharpDX;

namespace MY3DEngine.Cameras
{
    public interface ICamera
    {
        Vector3 Position { get; set; }

        Vector3 Rotation { get; set; }
    }
}