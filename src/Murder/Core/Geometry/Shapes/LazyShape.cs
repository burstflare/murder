using Murder.Utilities;

namespace Murder.Core.Geometry
{
    public struct LazyShape : IShape
    {   
        public readonly float Radius;
        public readonly Point Offset;
        public const float SQUARE_ROOT_OF_TWO = 1.41421356237f;
        
        public Rectangle Rectangle(Vector2 addPosition)
        {
            int size = Calculator.RoundToInt(SQUARE_ROOT_OF_TWO * Radius / 2f);
            return new(addPosition.X + Offset.X - size, addPosition.Y + Offset.Y - size, size * 2, size * 2);
        }

        public Rectangle GetBoundingBox()
        {
            int size = Calculator.RoundToInt(SQUARE_ROOT_OF_TWO * Radius / 2f);
            return new(Offset.X - size, Offset.Y - size, size * 2, size * 2);
        }

        public LazyShape(float radius, Point offset)
        {
            Radius = radius;
            Offset = offset;
        }

        internal bool Touches(Circle circle, Point offset1, Point offset2) 
        {
            var center1 = offset1 + new Point(Calculator.RoundToInt(circle.X), Calculator.RoundToInt(circle.Y));
            var center2 = offset2 + Offset;

            return (center1 - center2).LengthSquared() <= MathF.Pow(Radius + circle.Radius, 2);
        }
        
        internal bool Touches(LazyShape lazy2, Point offset1, Point offset2)
        {
            var center1 = offset1 + Offset;
            var center2 = offset2 + lazy2.Offset;

            return (center1 - center2).Length() <= Radius + lazy2.Radius;
        }

        internal bool Touches(Point point)
        {
            var delta = Offset - point;
            return delta.LengthSquared() <= MathF.Pow(Radius, 2);
        }
    
        private PolygonShape? _polygonCache = null;
        public PolygonShape GetPolygon()
        {
            _polygonCache ??= new PolygonShape(
                new Polygon(
                        new Point[] {
                            new Point(Offset.X, Offset.Y - Radius),
                            new Point(Offset.X + Radius * 0.75f, Offset.Y - Radius * 0.75f),
                            new Point(Offset.X + Radius * 1.25f, Offset.Y),
                            new Point(Offset.X + Radius* 0.75f, Offset.Y + Radius * 0.75f),
                            new Point(Offset.X, Offset.Y + Radius),
                            new Point(Offset.X - Radius* 0.75f, Offset.Y + Radius * 0.75f),
                            new Point(Offset.X - Radius * 1.25f, Offset.Y),
                            new Point(Offset.X - Radius * 0.75f, Offset.Y - Radius * 0.75f),
                        }
                    )
                );
            return _polygonCache.Value;
        }
    }
}
