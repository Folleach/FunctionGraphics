using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FunctionGraphics
{
    public class Camera
    {
        float smoothVelocity = 0.0000005f;
        Vector3 targetPosition;
        public Vector3 TargetPosition
        {
            get => targetPosition;
            set => targetPosition = value;
        }

        Vector3 position = new Vector3(0, 0, 0);
        Vector3 cameraUpVector = Vector3.UnitY;

        BasicEffect effect;

        float aspectRatio;
        float fieldOfView;
        float nearClipPlane;
        float farClipPlane;

        public Camera(BasicEffect effect, Vector3 position, float aspect, float fov, float nearClip, float farClip)
        {
            this.position = position;
            this.targetPosition = position;
            this.effect = effect;
            this.SetView(position);
            this.aspectRatio = aspect;
            this.fieldOfView = fov;
            this.nearClipPlane = nearClip;
            this.farClipPlane = farClip;
            this.effect.Projection = Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
        }

        public void Update(long time)
        {
            Vector3 velocity = targetPosition - position;
            this.position += velocity * this.smoothVelocity * time;
            this.position.Z += (this.targetPosition.Z - this.position.Z) * this.smoothVelocity * time;
            SetView(position);
        }

        private void SetView(Vector3 position)
        {
            Vector3 lockAt = position;
            lockAt.Z = 0;
            this.effect.View = Matrix.CreateLookAt(position, lockAt, cameraUpVector);
        }
    }
}
