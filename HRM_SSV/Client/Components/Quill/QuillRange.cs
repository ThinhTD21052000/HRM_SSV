using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Components.Control.Quill
{
    /// <summary>
    /// Quillの選択範囲を保持するクラス
    /// </summary>
    public class QuillRange
    {
        /// <summary>
        /// 開始位置
        /// </summary>
        public int Index { get; set; } = 0;

        /// <summary>
        /// 長さ
        /// </summary>
        public int Length { get; set; } = 0;
    }

}
