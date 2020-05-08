function slugFunc() {

    this.ajaxUrl = null;

    this.init = function (ajaxUrl) {
        this.ajaxUrl = ajaxUrl;
    };

    this.run = function (sourceSelector, targetSelector, refreshSelector) {

        _ajaxUrl = this.ajaxUrl;

        $(sourceSelector).change(function () {
            if (!$(targetSelector).val()) {
                generateSlug();
            }
        });

        $(refreshSelector).click(function (event) {
            event.preventDefault();
            generateSlug();
        });

        function generateSlug() {
            var title = $(sourceSelector).val();
            if (!title) return;

            $.post(_ajaxUrl, { title: title }, function (data) {
                $(targetSelector).val(data);
                $(targetSelector).trigger("blur"); // in order to trigger validation
            });
        }
    };
}

var slug = new slugFunc();