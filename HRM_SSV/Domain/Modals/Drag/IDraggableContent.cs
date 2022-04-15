using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yakureki.Domain.Model.Drag
{
    /// <summary>
    /// ドラッグ対象になるクラスのインターフェース
    /// </summary>
    public interface IDraggableContent
    {
        /// <summary>
        /// ドロップした際のコンテンツ
        /// </summary>
        public string GetDropContent();
    }
}
