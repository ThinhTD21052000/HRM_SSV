using Client.Components.Control.Quill;
using Client.Services;
using Domain.Models.RichText;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Yakureki.Domain.Model.Drag;

namespace Client.Components
{
    public partial class RichTextBox : ComponentBase, IDisposable
    {
        public RichTextBox()
        {
        }

        [Inject] DragDropManager DragDropManager { get; set; }

        /// <summary>
        /// 渡されるデータ
        /// </summary>
        [Parameter]
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// 表示形式
        /// </summary>
        [Parameter]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// 拡大・縮小
        /// </summary>
        [Parameter]
        public double Zoom { get; set; } = 1;

        /// <summary>子要素群</summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }


        private IRichTextDocument _document = null;

        /// <summary>
        /// リッチテキストデータ
        /// </summary>
        [Parameter]
        public IRichTextDocument Document
        {
            get { return _document; }
            set
            {
                bool changed = (_document == null || _document.RichText != value.RichText);
                _document = value;
                if (changed)
                {
                    Task.Run(async () => {
                        await quillInterop.SetRichtext(QuillContainer, _document.RichText);
                    });
                }
            }
        }
        /// <summary>
        /// コンテンツが更新された場合に発火します
        /// </summary>
        [Parameter]
        public EventCallback<IRichTextDocument> DocumentChanged { get; set; }

        private string _text = string.Empty;    // 都度quillから取得したくないので一旦変数に貯める

        /// <summary>
        /// プレーンテキスト（BindToSource only）
        /// </summary>
        /// <remarks>通常は使う必要なし</remarks>
        [Parameter]
        public string DocumentText
        {
            get { return _text; }
            set { }
        }

        /// <summary>
        /// コンテンツが更新された場合に発火します
        /// </summary>
        [Parameter]
        public EventCallback<string> DocumentTextChanged { get; set; }

        private bool _readonly = false;

        /// <summary>
        /// 読み取り専用にする場合に <see cref="true"/>
        /// </summary>
        [Parameter]
        public bool ReadOnly
        {
            get { return _readonly; }
            set
            {
                if (_readonly != value)
                {
                    _readonly = value;
                    Task.Run(async () => {
                        await quillInterop.SetCanEdit(QuillContainer, !value);
                    });
                }
            }
        }

        /// <summary>
        /// 未入力状態で表示しておく文字列
        /// </summary>
        [Parameter]
        public string Placeholder { get; set; } = "";

        /* パラメータ・DIインジェクション以外の変数 */

        protected ElementReference QuillContainer;
        protected ElementReference ToolBar;
        protected QuillInterop quillInterop;

        /* 自分自身のイベント */

        protected override void OnInitialized()
        {
            quillInterop = new QuillInterop(JSRuntime);
            quillInterop.TextChanged = OnQuillTextChanged_Internal;
            quillInterop.Drop = OnQuillDrop_Internal;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await quillInterop.CreateTextBox(QuillContainer, ToolBar, ReadOnly, Placeholder);
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        /* Quillのイベント */

        /// <summary>
        /// Quillコンテンツの内容が変更された際に発火します。
        /// </summary>
        /// <param name="delta">変更した内容</param>
        /// <param name="oldDelta">変更前のQuillコンテンツjson</param>
        /// <param name="source">変更ソース（'api', 'user', 'silent'）</param>
        /// <returns>cancel: Quill側でイベントをこれ以上処理しない場合に <see cref="true"/></returns>
        protected bool OnQuillTextChanged_Internal(string delta, string oldDelta, string source)
        {
            Task.Run(async () => {
                var contents = await quillInterop.GetContents(QuillContainer);
                if (contents != null)
                {
                    if (_document != null)
                    {
                        _document.RichText = contents.HtmlText;
                        _document.PlainText = contents.PlainText;
                        await DocumentChanged.InvokeAsync(_document);
                    }
                    _text = contents.PlainText;
                    await DocumentTextChanged.InvokeAsync(_text);
                }
            });
            return true;
        }

        /// <summary>
        /// Quillの親要素にドロップされた場合に発火します。
        /// </summary>
        /// <param name="e">イベント変数</param>
        /// <param name="items">ドロップされたデータ</param>
        /// <returns>cancel: Quill側でイベントをこれ以上処理しない場合に <see cref="true"/></returns>
        protected bool OnQuillDrop_Internal(DragEventArgs e, Dictionary<string, string> items)
        {
            // Quillより上位の要素でDropを処理する予定がある場合はQuillのDropをキャンセルしておく。
            if (DragDropManager.CanDrop()) return true;
            return this.ReadOnly;
        }

        /// <summary>
        /// Quillにデータがペーストされた場合に発火します。
        /// </summary>
        /// <param name="e">イベント変数</param>
        /// <param name="items">ペーストされたデータ</param>
        /// <returns>cancel: Quill側でイベントをこれ以上処理しない場合に <see cref="true"/></returns>
        protected bool OnQuillPaste_Internal(ClipboardEventArgs e, Dictionary<string, string> items)
        {
            // 現状やることなし
            return false;
        }

        /* Droppableのイベント */

        /// <summary>
        /// dotNetのクラスがドロップされた場合
        /// </summary>
        protected async Task OnDropAsync(DragEventArgs e, object data)
        {
            if (data != null || !this.ReadOnly)
            {
                var target = (data as IDraggableContent);
                if (target != null)
                {
                    string content = target.GetDropContent();
                    await quillInterop.InsertTextToPoint(QuillContainer, (decimal)e.ClientX, (decimal)e.ClientY, content);
                }
            }
        }

        /* その他 */

        void IDisposable.Dispose()
        {
            if (quillInterop != null)
                ((IDisposable)quillInterop).Dispose();
        }
    }
}
