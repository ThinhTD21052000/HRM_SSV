using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Components.Control.Quill
{
    /// <summary>
    /// QuillInterop.jsをラッピングし、Quillとの連携を行う
    /// </summary>
    /// <remarks>https://blazorhelpwebsite.com/ViewBlogPost/12</remarks>
    public class QuillInterop : IDisposable
    {
        private IJSRuntime jsRuntime;
        private DotNetObjectReference<QuillInterop> dotnetRef;

        /// <summary>
        /// このインスタンスを示すハッシュ文字列
        /// </summary>
        public string HashCode { get; set; } = string.Empty;

        public QuillInterop(IJSRuntime JSRuntime)
        {
            jsRuntime = JSRuntime;
            dotnetRef = DotNetObjectReference.Create<QuillInterop>(this);
            this.HashCode = Guid.NewGuid().ToString();
            
            // Todo:モジュール化
            //if (quillModule == null)
            //{
            //    Task.Run(async () => {
            //        quillModule = await jsRuntime.InvokeAsync<IJSObjectReference>("import", "./interop/QuillInterop.js");
            //    });
            //}
        }

        /// <summary>
        /// TextBoxスタイルでQuill.jsのインスタンスを生成する
        /// </summary>
        public ValueTask<bool> CreateTextBox(ElementReference container, ElementReference toolbar, bool readOnly, string placeholder)
        {
            var options = new Hashtable { ["placeholder"] = placeholder, ["readOnly"] = readOnly };
            var interop = new Hashtable { ["dotnet"] = dotnetRef, ["hashCode"] = this.HashCode };
            return jsRuntime.InvokeAsync<bool>("quillFunctions.createTextBox", container, options, toolbar, interop);
        }

        /// <summary>
        /// Quillのコンテンツを取得する
        /// </summary>
        public ValueTask<QuillContents> GetContents(ElementReference container)
        {
            return jsRuntime.InvokeAsync<QuillContents>("quillFunctions.getContents", container);
        }

        /// <summary>
        /// Quill内のHTMLを取得する
        /// </summary>
        public ValueTask<string> GetRichtext(ElementReference container)
        {
            return jsRuntime.InvokeAsync<string>("quillFunctions.getRichText", container);
        }

        /// <summary>
        /// QuillのHTMLを設定
        /// </summary>
        public ValueTask SetRichtext(ElementReference container, string richText)
        {
            return jsRuntime.InvokeVoidAsync("quillFunctions.setRichText", container, richText);
        }

        /// <summary>
        /// Quillのコンテンツを取得する（テキストデータのみ）
        /// </summary>
        public ValueTask<string> GetPlainText(ElementReference container)
        {
            return jsRuntime.InvokeAsync<string>("quillFunctions.getPlainText", container);
        }

        /// <summary>
        /// 入力内容の文字列の長さを取得する
        /// </summary>
        public ValueTask<int> GetLength(ElementReference container)
        {
            return jsRuntime.InvokeAsync<int>("quillFunctions.getLength", container);
        }

        /// <summary>
        /// 編集可・不可を切り替える
        /// </summary>
        public ValueTask<object> SetCanEdit(ElementReference container, bool canEdit)
        {
            return jsRuntime.InvokeAsync<object>("quillFunctions.setCanEdit", container, canEdit);
        }

        /// <summary>
        /// 選択位置を取得する
        /// </summary>
        public ValueTask<QuillRange> GetSelection(ElementReference container, bool focus = false)
        {
            return jsRuntime.InvokeAsync<QuillRange>("quillFunctions.getSelection", container, focus);
        }

        /// <summary>
        /// 選択位置を変更する
        /// </summary>
        public ValueTask<object> SetSelection(ElementReference container, int index, int length = 0)
        {
            return jsRuntime.InvokeAsync<object>("quillFunctions.setSelection", container, index, length);
        }

        /// <summary>
        /// 文字位置から描画範囲を取得する
        /// </summary>
        public ValueTask<Rectangle> GetBounds(ElementReference container, int index, int length = 0)
        {
            return jsRuntime.InvokeAsync<Rectangle>("quillFunctions.getBounds", container, index, length);
        }

        /// <summary>
        /// 画面上の位置情報から表示位置を取得する
        /// </summary>
        public ValueTask<int> GetIndexFromPoint(ElementReference container, decimal x, decimal y)
        {
            return jsRuntime.InvokeAsync<int>("quillFunctions.getIndexFromPoint", container, x, y);
        }

        /// <summary>
        /// カーソル位置を変更する
        /// </summary>
        public ValueTask SetCarret(ElementReference container, int index)
        {
            return jsRuntime.InvokeVoidAsync("quillFunctions.setSelection", container, index, 0);
        }

        /// <summary>
        /// 指定した画面上の位置情報にカーソル位置を変更する
        /// </summary>
        public ValueTask SetCarretToPoint(ElementReference container, decimal x, decimal y)
        {
            return jsRuntime.InvokeVoidAsync("quillFunctions.setCarretToPoint", container, x, y);
        }

        /// <summary>
        /// 文字列を挿入する
        /// </summary>
        public ValueTask<string> InsertText(ElementReference container, int index, string text)
        {
            return jsRuntime.InvokeAsync<string>("quillFunctions.insertText", container, index, text);
        }

        /// <summary>
        /// 文字列を指定した画面上の位置に挿入する
        /// </summary>
        public ValueTask<string> InsertTextToPoint(ElementReference container, decimal x, decimal y, string text)
        {
            return jsRuntime.InvokeAsync<string>("quillFunctions.insertTextToPoint", container, x, y, text);
        }

        /* Quillからのイベント */

        public delegate bool TextChangedEventhandler(string delta, string oldDelta, string source);

        /// <summary>
        /// Quillの text-changed イベント時に実行されるデリゲート
        /// </summary>
        public TextChangedEventhandler TextChanged { get; set; } = null;

        /// <summary>
        /// Quillコンテンツの内容が変更された際に発火します。
        /// </summary>
        /// <param name="delta">変更した内容</param>
        /// <param name="oldDelta">変更前のQuillコンテンツjson</param>
        /// <param name="source">変更ソース（'api', 'user', 'silent'）</param>
        /// <returns>cancel: Quill側でイベントをこれ以上処理しない場合に <see cref="true"/></returns>
        [JSInvokable]
        public bool OnTextChanged(string delta, string oldDelta, string source)
        {
            if(TextChanged != null) return TextChanged.Invoke(delta, oldDelta, source);
            return false;
        }

        /// <summary>
        /// Quill.container の drop イベント時に実行されるデリゲート
        /// </summary>
        public Func<DragEventArgs, Dictionary<string, string>, bool> Drop { get; set; } = null;

        /// <summary>
        /// Quillの親要素にドロップされた場合に発火します。
        /// </summary>
        /// <param name="e">イベント変数</param>
        /// <param name="items">ドロップされたデータ</param>
        /// <returns>cancel: Quill側でイベントをこれ以上処理しない場合に <see cref="true"/></returns>
        [JSInvokable]
        public bool OnDrop(DragEventArgs e, Dictionary<string, string> items)
        {
            if (Drop != null) return Drop.Invoke(e, items);
            return false;
        }


        /// <summary>
        /// Quill.container の paste イベント時に実行されるデリゲート
        /// </summary>
        public Func<ClipboardEventArgs, Dictionary<string, string>, bool> Paste { get; set; } = null;

        /// <summary>
        /// Quillにデータがペーストされた場合に発火します。
        /// </summary>
        /// <param name="e">イベント変数</param>
        /// <param name="clipboardData">ペーストされたデータにまつわる情報</param>
        /// <param name="items">ペーストされたデータ</param>
        /// <returns>cancel: Quill側でイベントをこれ以上処理しない場合に <see cref="true"/></returns>
        [JSInvokable]
        public bool OnPaste(ClipboardEventArgs e, DataTransfer clipboardData, Dictionary<string, string> items)
        {
            // clipboaardData は推奨されないデータなのでdotNet側にはわたさない
            if (Paste != null) return Paste.Invoke(e, items);
            return false;
        }

        void IDisposable.Dispose()
        {
            jsRuntime = null;
            dotnetRef?.Dispose();
            dotnetRef = null;
            this.TextChanged = null;
            this.Drop = null;
            this.Paste = null;
        }
    }

}