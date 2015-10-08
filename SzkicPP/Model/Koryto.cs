using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SzkicPP.Model
{
    /// <summary>
    /// Koryto
    /// </summary>
    public class Koryto : Teren
    {
        protected StringFormat _format = new StringFormat(StringFormatFlags.NoClip | StringFormatFlags.FitBlackBox);

        /// <summary>
        /// Konstruktor koryta (domyślny)
        /// </summary>
        public Koryto() : this(-1, -1) { }

        /// <summary>
        /// Konstruktor koryta
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public Koryto(int start, int end)
            : base(start, end)
        {
            _format.Alignment = StringAlignment.Near;
            _format.LineAlignment = StringAlignment.Center;
        }

        /// <summary>
        /// Rysuj koryto na szkicu
        /// </summary>
        /// <param name="g"></param>
        /// <param name="szkic"></param>
        public override void Rysuj(Graphics g, SzkicPrzekroju szkic)
        {
            if (!AktualizujPikiety(szkic)) return;
            
            int x1 = szkic.Obszar.Left, y1 = _startPikieta.Y;
            int x2 = szkic.Obszar.Right, y2 = _endPikieta.Y;

            int korytoX = x1;
            int korytoY = (y1 + y2) / 2; //środek koryta

            _format.Alignment = StringAlignment.Near;
            _format.LineAlignment = StringAlignment.Center;

            //Brzeg lewy
            g.DrawLine(StylSzkicu.SzkicPen, szkic.Obszar.Left, y1, szkic.Obszar.Right, y1);

            g.DrawString(string.Format("{0:F2} [m] n.p.m.", _startPikieta.Rzedna), 
                StylSzkicu.TekstFont, StylSzkicu.TekstBrush, korytoX, y1);
            g.DrawString("zww", StylSzkicu.TekstFont, StylSzkicu.TekstBrush, korytoX - 35, y1, _format);
            g.DrawString("Brzeg lewy", StylSzkicu.TekstFont, StylSzkicu.TekstBrush,
                korytoX - 120, (szkic.Obszar.Top + y1) / 2, _format);

            //Kierunek (na środku)
            g.DrawLine(StylSzkicu.SzkicPen, x1, korytoY, x1 + 100, korytoY);
            StylSzkicu.DrawDart(g, x1 + 100, korytoY, StylSzkicu.SzkicColor, 10, 0, true);

            //Brzeg prawy
            g.DrawLine(StylSzkicu.SzkicPen, szkic.Obszar.Left, y2, szkic.Obszar.Right, y2);

            _format.Alignment = StringAlignment.Near;
            _format.LineAlignment = StringAlignment.Far;

            g.DrawString(string.Format("{0:F2} [m] n.p.m.", _endPikieta.Rzedna),
                StylSzkicu.TekstFont, StylSzkicu.TekstBrush, korytoX, y2, _format);
            g.DrawString("zww", StylSzkicu.TekstFont, StylSzkicu.TekstBrush, korytoX - 35, y2, _format);
            g.DrawString("Brzeg prawy", StylSzkicu.TekstFont, StylSzkicu.TekstBrush,
                korytoX - 120, (szkic.Obszar.Bottom + y2) / 2, _format);

            _format.Alignment = StringAlignment.Near;
            _format.LineAlignment = StringAlignment.Center;
        }
    }
}
