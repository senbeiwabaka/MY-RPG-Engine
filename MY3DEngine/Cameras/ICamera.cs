using SharpDX;

namespace MY3DEngine.Cameras
{
    public interface ICamera
    {
        Vector3 Position { get; }
        Matrix ViewMatrix { get; set; }

        /// <summary>
        /// Initialize the Camera variables
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        void Initialize(int width = default(int), int height = default(int));

        void OnResize(float new_width, float new_height);

        void Render();

        void SetPosition(Vector3 newPosition);

        void SetPosition(float x, float y, float z);
    }
}