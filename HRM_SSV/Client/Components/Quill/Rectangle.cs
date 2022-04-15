using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Components.Control.Quill
{
    /// <summary>
    /// 文字描画範囲
    /// </summary>
    /// <remarks>{ left: Number, top: Number, height: Number, width: Number }</remarks>
    public class Rectangle
    {
        public decimal left { get; set; }
        public decimal top { get; set; }
        public decimal height { get; set; }
        public decimal width { get; set; }

        public System.Drawing.Rectangle ToDrawing()
        {
            return new System.Drawing.Rectangle((int)this.left, (int)this.top, (int)this.width, (int)this.height);
        }
    }
}
