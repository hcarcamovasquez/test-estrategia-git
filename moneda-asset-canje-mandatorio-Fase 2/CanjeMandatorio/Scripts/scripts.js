$(function () {
    $(".gvv").each(function () {
        $(this).prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
            "lengthMenu": [[10, 15, 25, 50, -1], [10, 15, 25, 50, "All"]],
            "language": {
                "lengthMenu": "Mostrar _MENU_ registros por página",
                "info": "Revisando la página _PAGE_ de _PAGES_",
                "sSearch": "Filtrar :",
                "oPaginate": {
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior"
                },
            }
        }).on('draw', function () {
            for (var i = 1; i < this.rows.length; i++) {
                var radioBtn = this.rows[i].cells[0].getElementsByTagName("input");
                if (radioBtn.length > 0) {
                    radioBtn[0].checked = false;
                    enableDisableButtons(true);
                }
            }
        });
    });
});

$(function () {
    $('.wrap-responsive').wrap('<div class="table-responsive"></div>');
});

$(function () {
    $(".gvv2").each(function () {
        $(this).prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
            "lengthMenu": [[10, 15, 25, 50, -1], [10, 15, 25, 50, "All"]],
            "language": {
                "lengthMenu": "Mostrar _MENU_ registros por página",
                "info": "Revisando la página _PAGE_ de _PAGES_",
                "sSearch": "Filtrar :",
                "oPaginate": {
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior"
                },
            }
        }).on('draw', function () {
            llamarFuncion();

        });
    });
});

function getMask(ent, dec) {
    if (dec == null || dec == 0) {
        return '9'.repeat(ent);
    } else {
        return '9'.repeat(ent) + ',' + '0'.repeat(dec);
    }
}

function soloLetras(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " áéíóúabcdefghijklmnñopqrstuvwxyz";
    especiales = "8-37-39-46";
    tecla_especial = false
    for (var i in especiales) {    
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }   

    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
    else {
        return true;
    }
}

function onBlurSoloLetras(texto) {
    letras = " áéíóúabcdefghijklmnñopqrstuvwxyz";

    for (i = 1; i <= texto.length; i++) {
        var letra = texto.charAt(texto.length - i).toLowerCase();
        if (letras.indexOf(letra) == -1) {
            return false
        }        
    }
    return true    
}

function onBlurSoloNumeros(texto) {
    letras = "0123456789";
    especiales = "8-37-39-46";
    
    for (i = 1; i <= texto.length; i++) {
        var letra = texto.charAt(texto.length - i).toLowerCase();
        if (letras.indexOf(letra) == -1) {
            return false
        }       
    }

    return true
};

function soloNumeros(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = "0123456789.";
    especiales = "8-37-39-46";
    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }
    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
    else {
        return true;
    }
}

function soloNumerosSinPunto(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = "0123456789";
    especiales = "8-37-39-46";
    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }
    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
    else {
        return true;
    }
}

function soloNumerosyComa(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = "0123456789,";
    especiales = "8-37-39-46";
    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }
    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
    else {
        return true;
    }
}

function NumCheck(e, field) {
    key = e.keyCode ? e.keyCode : e.which;
    if (key === 8)
        return true;
    if (field.value !== "") {
        if ((field.value.indexOf(",")) > 0) {
            if (key > 47 && key < 58) {
                if (field.value === "")
                    return true;
                regexp = /[0-9]{1,10}[,][0-9]{1,3}$/;
                regexp = /[0-9]{6}$/;
                return !(regexp.test(field.value))
            }
        }
    }
    if (key > 47 && key < 58) {
        if (field.value === "")
            return true;
        regexp = /[0-9]{20}/;
        return !(regexp.test(field.value));
    }
    if (key === 44) {
        if (field.value === "")
            return false;
        regexp = /^[0-9]+$/;
        return regexp.test(field.value);

    }

    return false;
}

function soloNumeros4Decimales(e, field) {
    key = e.keyCode ? e.keyCode : e.which;
    if (key === 8)
        return true;
    if (field.value !== "") {
        if ((field.value.indexOf(",")) > 0) {
            if (key > 47 && key < 58) {
                if (field.value === "")
                    return true;
                regexp = /[0-9]{1,10}[,][0-9]{1,3}$/;
                regexp = /[0-9]{4}$/;
                return !(regexp.test(field.value))
            }
        }
    }
    if (key > 47 && key < 58) {
        if (field.value === "")
            return true;
        regexp = /[0-9]{20}/;
        return !(regexp.test(field.value));
    }
    if (key === 44) {
        if (field.value === "")
            return false;
        regexp = /^[0-9]+$/;
        return regexp.test(field.value);

    }

    return false;
}



function soloRut(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = "0123456789k";
    especiales = "8-37-39-46";
    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }
    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
    else {
        return true;
    }
}

function soloNemotecnico(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toUpperCase()
    letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-";
    especiales = "8-37-39-46";
    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }
    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
    else {
        return true;
    }
}

function onblurCalendar(c) {
    if (c.style.display == 'none')
        c.style.display = 'block';
    else
        c.style.display = 'none';
}

