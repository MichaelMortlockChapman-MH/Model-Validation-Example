const parseFloatFormatted = (value) => {
    value = value.replace(",", ".");
    return parseFloat(value);
} 

$.validator.addMethod('containz', function (value, element, params) {
    const str = $(element).val();

    return str != null && (str.includes('z') || str.includes('Z'));
});

$.validator.unobtrusive.adapters.add('containz', [], function (options) {
    options.rules['containz'] = [options.element, null];
    options.messages['containz'] = options.message;
});

$.validator.addMethod('atleast1selected', function (value, element, params) {
    const group = $(params[1].group);
    return group.filter(":checked").length >= 1;
});

$.validator.unobtrusive.adapters.add('atleast1selected', ['group'], function (options) {
    options.rules['atleast1selected'] = [options.element, { group: options.params['group'] }];
    options.messages['atleast1selected'] = options.message;
});

$.validator.addMethod('totalselectedis100', function (value, element, params) {
    var checkboxes = $(params[1].selectiongroup);
    var numbers = $(params[1].numbergroup);
    let total = 0
    numbers.each((i, e) => {
        console.log(i, e)
        const ischecked = $(checkboxes[i]).is(":checked");
        if (ischecked) {
            total += parseFloat(e.value)
        }
    })

    if (total === 100) {
        return true;
    }
    if (!(params[1].exact === "True") && total >= 99.99 && total <= 100) {
        return true;
    }
    return false;
});

$.validator.unobtrusive.adapters.add('totalselectedis100', ['selectiongroup', 'numbergroup', 'exact'], function (options) {
    options.rules['totalselectedis100'] = [options.element, {
        selectiongroup: options.params['selectiongroup'],
        numbergroup: options.params['numbergroup'],
        exact: options.params['exact']
    }];
    options.messages['totalselectedis100'] = options.message;
});

$.validator.methods.number = function (value, element) {
    return !isNaN(parseFloatFormatted(value));
};

$.validator.methods.range = function (value, element, params) {
    return this.optional(element) || parseFloatFormatted(value) >= params[0] && parseFloatFormatted(value) <= params[1]
};

$.validator.setDefaults({
    ignore: ":hidden:not(#Dummy)" // include #Applicants
});