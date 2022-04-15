using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Components.Control.Quill
{
    /// <summary>
    /// Quill内のデータを形式別に保持するクラス
    /// </summary>
    public class QuillContents
    {
        /// <summary>
        /// HTML状態のコンテンツ
        /// </summary>
        public string HtmlText { get; set; } = string.Empty;

        /// <summary>
        /// コンテンツからテキストデータのみを抽出したもの
        /// </summary>
        public string PlainText { get; set; } = string.Empty;

        /// <summary>
        /// Quill内部で使われるjsonデータ
        /// </summary>
        public string Delta { get; set; } = string.Empty;
    }

}
