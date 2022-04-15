using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Services
{
    /// <summary>
    /// オブジェクトモデルのドラッグ＆ドロップを管理するマネージャ
    /// </summary>
    /// <remarks>https://www.radzen.com/blog/blazor-drag-and-drop/</remarks>
    public class DragDropManager
    {
        private WeakReference<object> _activeItem = null;
        /// <summary>
        /// ドラッグ対象のオブジェクトモデル
        /// </summary>
        public object ActiveItem
        {
            get
            {
                if (_activeItem == null) return null;
                object value = null;
                _activeItem.TryGetTarget(out value);
                return value;
            }
            set
            {
                if (_activeItem == null)
                    _activeItem = new WeakReference<object>(value);
                else
                    _activeItem.SetTarget(value);
            }
        }

        /// <summary>
        /// ドラッグ＆ドロップ可能な範囲を限定するための Zone 指定
        /// </summary>
        public string ActiveZone { get; set; } = string.Empty;

        /// <summary>
        /// ドラッグ中の扱いの場合に <see cref="true"/>
        /// </summary>
        public bool Dragging { get { return this.ActiveItem != null; } }

        /// <summary>
        /// 現在の <see cref="ActiveItem"/> がどこかにドロップされた後に <see cref="true"/>
        /// </summary>
        /// <remarks>dropからdragendまでの間で使用</remarks>
        public bool Dropped { get; set; } = false;

        /// <summary>
        /// ドラッグの開始・解除時に発火します（ドロップ時は発火しません）
        /// </summary>
        public EventHandler DragStateChanged { get; set; }

        /* メソッド */

        /// <summary>
        /// オブジェクトモデルのドロップが可能な状態の場合に <see cref="true"/>
        /// </summary>
        public bool CanDrop()
        {
            return (this.Dragging && !this.Dropped);
        }

        /// <summary>
        /// オブジェクトモデルのドロップが可能な状態の場合に <see cref="true"/>
        /// </summary>
        public bool CanDrop<T>()
        {
            return (this.Dragging && !this.Dropped && (this.ActiveItem is T));
        }

        /// <summary>
        /// オブジェクトモデルのドロップが可能な状態の場合に <see cref="true"/>
        /// </summary>
        public bool CanDrop(string zone)
        {
            return (this.CanDrop() && this.ActiveZone == zone);
        }

        /// <summary>
        /// オブジェクトモデルのドロップが可能な状態の場合に <see cref="true"/>
        /// </summary>
        /// <remarks>
        /// 下記の場合は <see cref="false"/>
        /// <list type="bullet">
        ///   <item>1.オブジェクトモデルのドラッグが開始されていない。</item>
        ///   <item>2.他の要素でドロップ済み。</item>
        ///   <item>3.ドラッグ要素とドロップ対象要素でzoneが異なる。</item>
        ///   <item>4.ドロップ対象要素が受け付けるオブジェクトモデルがドラッグされていない。</item>
        /// </list>
        /// </remarks>
        public bool CanDrop<T>(string zone)
        {
            return (this.CanDrop<T>() && this.ActiveZone == zone);
        }

        /// <summary>
        /// ドラッグ処理を開始する
        /// </summary>
        public void DragStart(object item, string zone)
        {
            this.Clear();
            this.ActiveItem = item;
            this.ActiveZone = zone;

            DragStateChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// ドロップ処理を行う
        /// </summary>
        public void Drop()
        {
            this.ActiveItem = null;
            this.ActiveZone = null;
            this.Dropped = true;
        }

        /// <summary>
        /// ドラッグ処理を終了する
        /// </summary>
        public void DragEnd()
        {
            this.Clear();
            DragStateChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// ドラッグ中の情報を削除する
        /// </summary>
        protected void Clear()
        {
            this.ActiveItem = null;
            this.ActiveZone = null;
            this.Dropped = false;
        }
    }

}
