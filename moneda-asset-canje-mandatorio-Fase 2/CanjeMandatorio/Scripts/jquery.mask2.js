/*
	Masked Input plugin for jQuery
	Copyright (c) 2007-2013 Josh Bush (digitalbush.com)
	Licensed under the MIT license (http://digitalbush.com/projects/masked-input-plugin/#license)
	Version: 1.3.1
*/
(function($) {
$.fn.extend({

    mask2: function (mask, settings) {
        //store in data
        this.data('data-mask', mask);
        this.data('data-maskLength', mask.length);
        

        //add data attributes as html markups (optional)
        this.attr('data-mask', mask);
        this.attr('data-maskLength', mask.length);
        
        return this.mask(mask, settings);
    }


});    
})(jQuery);