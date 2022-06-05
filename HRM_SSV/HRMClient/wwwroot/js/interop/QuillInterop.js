(function () {

    const private_store = new WeakMap();

    class cz7LikeQuill {

        constructor(quill, options = {}) {
            const _private = new Map();

            private_store.set(this, _private);

            _private
                .set('hashCode', options.hashCode || this.newGuid())
                .set('quill', quill)
                .set('options', options || {})
                .set('listeners', new Set());

            this.addListener(quill.container, 'dragstart', this.onDragStart);
            this.addListener(quill.container, 'dragover', this.onDragOver);
            this.addListener(quill.container, 'drop', this.onDrop);
            this.addListener(quill.container, 'paste', this.onPaste);

            this.onTextChanged = this.onTextChanged.bind(this);
            quill.on('text-change', this.onTextChanged);
        }

        onDragStart(e) {
            e.dataTransfer.setData("text/quillHash", this.hashCode);
        }

        onDragOver(e) {
            if (e.dataTransfer.items.length == 0)
                this.quill.setCarretToPoint(e.clientX, e.clientY);
        }

        onDrop(e) {
            try {
                e.dataTransfer.effectAllowed = 'copyMove';

                // d&d between quil.
                let hash = e.dataTransfer.getData("text/quillHash");
                if (hash) return;

                // d&d between upper dotnet component.
                e.preventDefault();

                const dotnet = this.dotnet;
                if (dotnet) {
                    let stop = dotnet.invokeMethod('OnDrop', this.convertDotNetEvent(e), this.convertDotNetMap(e));
                    if (stop) return;
                }

                // d&d from other.
                e.stopPropagation();

                if (e.dataTransfer) {
                    let text = e.dataTransfer.getData('text/plain');
                    if (text.length > 0) {
                        this.quill.insertTextToPoint(e.clientX, e.clientY, text);
                    }
                }
            } catch (err) {
                console.info(err);
                e.preventDefault();
                e.stopPropagation();
            }
        }

        onPaste(e) {
            try {
                const dotnet = this.dotnet;
                if (dotnet != null) {
                    let clipboardEvent = this.convertDotNetEvent(e);
                    let cancel = dotnet.invokeMethod('OnPaste', clipboardEvent, clipboardEvent.DataTransfer, this.convertDotNetMap(e));
                    if (cancel) {
                        e.preventDefault();
                        return;
                    }
                }

                if (e.clipboardData) {
                    let ng_data = (e.clipboardData.getData('image') || e.clipboardData.getData('application'));
                    if (ng_data.length > 0) { e.preventDefault(); }
                    if (e.clipboardData.files && e.clipboardData.files.length > 0) { e.preventDefault(); }
                }
            } catch (err) {
                console.info(err);
                e.preventDefault();
                e.stopPropagation();
            }
        }

        onTextChanged(delta, oldDelta, source) {
            const dotnet = this.dotnet;
            if (dotnet != null) {
                dotnet.invokeMethodAsync('OnTextChanged', JSON.stringify(delta), JSON.stringify(oldDelta), source);
            }
        }

        destroy() {
            const listeners = private_store.get(this).get('listeners');
            listeners.forEach(({ node, event_name, listener }) => {
                node.removeEventListener(event_name, listener);
            });
            quill.off('text-change', this.onTextChanged);

            this.options['dotnet'] = null;
        }

        addListener(node, event_name, listener_fn) {
            const listener = listener_fn.bind(this);
            node.addEventListener(event_name, listener, false);
            private_store.get(this).get('listeners').add({ node, event_name, listener });
        }

        convertDotNetEvent(e) {
            if (e == null) return {};

            var dataTransfer = {};
            var dataContainer = (e.dataTransfer || e.clipboardData || window.clipboardData);
            if (dataContainer != null) {
                let files = [];
                for (var i = 0; i < dataContainer.files.length; i++) {
                    files.push(dataContainer.files[i].name);
                }

                let items = [];
                for (var i = 0; i < dataContainer.items.length; i++) {
                    let kind = dataContainer.items[i].kind;
                    let type = dataContainer.items[i].type;
                    items.push({ kind, type });
                }

                dataTransfer = {
                    DropEffect: dataContainer.dropEffect, EffectAllowed: dataContainer.effectAllowed,
                    Items: items, Types: dataContainer.types, Files: files
                }
            }

            return {
                AltKey: e.altKey, Button: e.button, Buttons: e.buttons, clientX: e.clientX, clientY: e.clientY,
                CtrlKey: e.ctrlKey, Detail: e.detail, MetaKey: e.metaKey, OffsetX: e.offsetX, OffsetY: e.offsetY,
                ScreenX: e.screenX, ScreenY: e.screenY, ShiftKey: e.shiftKey, Type: e.type,
                DataTransfer: dataTransfer
            };
        }

        convertDotNetMap(e) {
            var items = {};
            if (e == null) return items;

            var dataContainer = (e.dataTransfer || e.clipboardData || window.clipboardData);
            if (dataContainer == null) return items;

            for (var i = 0; i < dataContainer.types.length; i++) {
                const key = dataContainer.types[i];
                let value = dataContainer.getData(key);
                items[key] = value;
            }
            return items;
        }

        newGuid() {
            return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
                (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
            );
        }

        get hashCode() {
            return private_store.get(this).get('hashCode');
        }

        get quill() {
            return private_store.get(this).get('quill');
        }

        get options() {
            return private_store.get(this).get('options');
        }

        get dotnet() {
            const _options = this.options;
            if (!(_options.dotnet === undefined || _options.dotnet === null)) {
                return _options.dotnet;
            }
            return null;
        }
    };

    Quill.prototype.getIndexFromPoint = function (x, y) {
        const document = this.root.ownerDocument;
        const baseElement = document.elementFromPoint(x, y);
        if (baseElement === null) return -1;

        const containerBounds = this.container.getBoundingClientRect();
        x -= containerBounds.x;
        y -= containerBounds.y;

        const blotOrQuill = Quill.find(baseElement, true);
        if (blotOrQuill === null || blotOrQuill instanceof Quill) {
            return -1;
        }

        const blot = blotOrQuill;
        const blotOffset = this.getIndex(blot);

        let low = blotOffset - 1;
        let high = blotOffset + blot.length();
        let probe;
        let probeBounds;

        // 文字の描画範囲をもとに二分探索でキャレット位置を特定する
        while (high - low > 1) {
            probe = (low + high) >>> 1;
            probeBounds = this.getBounds(probe, 1);

            if (y < probeBounds.top) {
                high = probe;
            } else if (y >= probeBounds.bottom) {
                low = probe;
            } else if (x < probeBounds.left) {
                high = probe;
            } else {
                low = probe;
            }
        }
        if (low === -1) return -1;

        probeBounds = this.getBounds(low, 1);
        if ((x < probeBounds.left) || (x >= probeBounds.right) ||
            (y < probeBounds.top) || (y >= probeBounds.bottom)) {
            // 特定できなかった場合
            const dropOnEndOfLine = (probeBounds.width === 0) && (probeBounds.left < 0);
            const dropAfterEndOfLine = (y >= probeBounds.top) && (y < probeBounds.bottom);

            if (dropOnEndOfLine || dropAfterEndOfLine) {
                return (blotOffset + blot.length() - 1);
            }
            return -1;
        }
        return low;
    }

    Quill.prototype.setCarretToPoint = function (x, y) {
        const index = this.getIndexFromPoint(x, y);
        if (index < 0) return;
        this.setSelection(index, 0);
    }

    Quill.prototype.insertTextToPoint = function (x, y, text) {
        let index = this.getIndexFromPoint(x, y);
        if (index < 0) index = this.getLength();
        return this.insertText(index, text);
    }

    window.quillFunctions = {

        globalInitialized: false,

        /**
         * 全体共通の初期化処理あれば書く
         * */
        initialize: function () {
            quillFunctions.globalInitialized = true;

            var _size = Quill.import('attributors/style/size');
            _size.whitelist = null;
            Quill.register(_size, true);

            // 従来の RichTextBox 風に動作させる
            // TODO:モジュール化
            Quill.register('modules/cz7like', cz7LikeQuill, true);
        },

        createTextBox: function (container, options, toolbarOptions, interopOptions) {
            if (!quillFunctions.globalInitialized) quillFunctions.initialize();

            var ignorePicture = function (node, delta) {
                if (node === 'img' || node === 'picture') {
                    const Delta = Quill.import('delta')
                    return new Delta()
                }
                return delta;
            }

            let _options = Object.assign(options);
            _options['modules'] = {
                toolbar: Object.assign(toolbarOptions), cz7like: Object.assign(interopOptions), clipboard: { matchers: [[Node.TEXT_NODE, ignorePicture]] }
            };
            _options['theme'] = 'bubble';

            new Quill(container, _options);
            return true;
        },

        getLength: function (container) {
            return container.__quill.getLength();
        },

        getContents: function (container) {
            var quill = container.__quill;
            if (quill === null || !(quill instanceof Quill)) return {};

            let contents = JSON.stringify(quill.getContents());
            let text = quill.getText();
            let html = quill.root.innerHTML;

            return { HtmlText: html, PlainText: text, Delta: contents };
        },

        getRichText: function (container) {
            return container.__quill.root.innerHTML
        },

        setRichText: function (container, content) {
            return container.__quill.root.innerHTML = content;
        },

        getPlainText: function (container) {
            return container.__quill.getText();
        },

        setCanEdit: function (container, canEdit) {
            container.__quill.enable(canEdit);
        },

        getSelection: function (container, focus = false) {
            return container.__quill.getSelection(focus);
        },

        setSelection: function (container, index, length) {
            return container.__quill.setSelection(index, length);
        },

        getBounds: function (container, index, length) {
            return container.__quill.getBounds(index, length);
        },

        getIndexFromPoint: function (container, x, y) {
            return container.__quill.getIndexFromPoint(x, y);
        },

        setCarretToPoint: function (container, x, y) {
            return container.__quill.setCarretToPoint(x, y);
        },

        insertText: function (container, index, text) {
            return JSON.stringify(container.__quill.insertText(index, text));
        },

        insertTextToPoint: function (container, x, y, text) {
            return JSON.stringify(container.__quill.insertTextToPoint(x, y, text));
        }

    };
})();