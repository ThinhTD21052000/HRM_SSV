(function () {
    window.movableFunctions = {

        attach: function (element) {
            var previous_x = 0, previous_y = 0;

            element.onmousedown = startDrag;

            function startDrag(e) {
                e = (e || window.event);
                e.preventDefault();

                previous_x = e.clientX;
                previous_y = e.clientY;
                document.onmouseup = endDrag;
                document.onmousemove = mouseMove;
            }

            function mouseMove(e) {
                e = (e || window.event);
                e.preventDefault();

                var diff_x = previous_x - e.clientX;
                var diff_y = previous_y - e.clientY;
                previous_x = e.clientX;
                previous_y = e.clientY;

                element.style.top = (element.offsetTop - diff_y) + "px";
                element.style.left = (element.offsetLeft - diff_x) + "px";
            }

            function endDrag() {
                document.onmouseup = null;
                document.onmousemove = null;
            }
        }

    };
})();