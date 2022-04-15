using GUI_20212202_MQ7GIA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using GUI_20212202_MQ7GIA.Logic;

namespace GUI_20212202_MQ7GIA.UI.Renderer
{
    public class Display : FrameworkElement
    {
        // Models
        GameLogic logic;
        Size size;
        public void SetupModel(GameLogic logic, Size size)
        {
            this.logic = logic;
            this.size = size;
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
        }

    }
}
