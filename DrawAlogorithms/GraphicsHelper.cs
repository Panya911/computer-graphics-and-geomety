using System;
using System.Drawing;

namespace KGG
{
    public static class GraphicsHelper
    {
        public static void Fill(this Bitmap bitmap, Color color)
        {
            using (var gfx = Graphics.FromImage(bitmap))
            using (var brush = new SolidBrush(color))
            {
                gfx.FillRectangle(brush, 0, 0, bitmap.Width, bitmap.Height);
            }
        }

        public static void DrawText(this Bitmap bitmap, int x, int y, string text, Color color)
        {
            using (var gfx = Graphics.FromImage(bitmap))
            {
                gfx.DrawString(text, new Font(FontFamily.GenericMonospace, 8), new SolidBrush(color), x, y);
            }
        }

        public static void DrawLine(this Bitmap image, int x1, int y1, int x2, int y2, Color color)
        {
            var steep = Math.Abs(y2 - y1) > Math.Abs(x2 - x1); // Проверяем рост отрезка по оси икс и по оси игрек
            // Отражаем линию по диагонали, если угол наклона слишком большой
            if (steep)
            {
                Swap(ref x1, ref y1); // Перетасовка координат вынесена в отдельную функцию для красоты
                Swap(ref x2, ref y2);
            }
            // Если линия растёт не слева направо, то меняем начало и конец отрезка местами
            if (x1 > x2)
            {
                Swap(ref x1, ref x2);
                Swap(ref y1, ref y2);
            }
            var dx = x2 - x1;
            var dy = Math.Abs(y2 - y1);
            var error = dx / 2; // Здесь используется оптимизация с умножением на dx, чтобы избавиться от лишних дробей
            var ystep = y1 < y2 ? 1 : -1; // Выбираем направление роста координаты y
            var y = y1;
            for (var x = x1; x <= x2; x++)
            {
                image.SetPixel(steep ? y : x, steep ? x : y, color); // Не забываем вернуть координаты на место
                error -= dy;
                if (error < 0)
                {
                    y += ystep;
                    error += dx;
                }
            }

            void Swap(ref int first, ref int second)
            {
                var temp = first;
                first = second;
                second = temp;
            }
        }
    }
}