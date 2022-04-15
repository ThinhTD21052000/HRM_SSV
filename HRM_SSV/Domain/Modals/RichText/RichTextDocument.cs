using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.RichText
{
    /// <summary>
    /// リッチテキストを持つコンポーネントやモデルが持つインターフェース
    /// </summary>
    public class RichTextDocument : IRichTextDocument
    {
        /// <summary>
        /// リッチテキストコンテンツ
        /// </summary>
        public string RichText { get; set; }

        /// <summary>
        /// コンテンツからテキストデータのみを抽出したもの
        /// </summary>
        public string PlainText { get; set; }
    }
}
