``` mermaid
classDiagram
    class Shape {
        <<abstract>>
        +Point StartPoint
        +Point EndPoint
        +Pen OutlinePen
        +Show(Graphics g) void*
    }
 
    class PointShape {
        +Brush FillBrush
        +Show(Graphics g) void
    }
 
    class LineShape {
        +Show(Graphics g) void
    }
 
    class RectShape {
        +Brush FillBrush
        +Show(Graphics g) void
    }
 
    class EllipseShape {
        +Show(Graphics g) void
    }
 
    Shape <|-- PointShape
    Shape <|-- LineShape
    Shape <|-- RectShape
    Shape <|-- EllipseShape
```
