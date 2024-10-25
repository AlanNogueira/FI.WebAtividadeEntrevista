var beneficiarios = [];

$(document).ready(function () {

    $(".CPF").mask("000.000.000-00");
    $("#CEP").mask("00000-000");
    $("#Telefone").mask("(99)99999-9999");

    if (obj) {
        console.log(obj)
        $('#formCadastro #Nome').val(obj.Nome);
        $('#formCadastro #CEP').val(obj.CEP.replace(/^([\d]{2})([\d]{3})([\d]{3})|^[\d]{2}.[\d]{3}-[\d]{3}/, "$1.$2-$3"));
        $('#formCadastro #CPF').val(obj.CPF.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4"));
        $('#formCadastro #Email').val(obj.Email);
        $('#formCadastro #Sobrenome').val(obj.Sobrenome);
        $('#formCadastro #Nacionalidade').val(obj.Nacionalidade);
        $('#formCadastro #Estado').val(obj.Estado);
        $('#formCadastro #Cidade').val(obj.Cidade);
        $('#formCadastro #Logradouro').val(obj.Logradouro);
        $('#formCadastro #Telefone').val(obj.Telefone.replace(/^(\d\d)(\d{5})(\d{4}).*/, "($1) $2-$3"));
        beneficiarios = obj.Beneficiarios;
    }

    $('#formCadastro').submit(function (e) {
        e.preventDefault();
        
        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "NOME": $(this).find("#Nome").val(),
                "CEP": $(this).find("#CEP").val(),
                "CPF": $(this).find("#CPF").val(),
                "Email": $(this).find("#Email").val(),
                "Sobrenome": $(this).find("#Sobrenome").val(),
                "Nacionalidade": $(this).find("#Nacionalidade").val(),
                "Estado": $(this).find("#Estado").val(),
                "Cidade": $(this).find("#Cidade").val(),
                "Logradouro": $(this).find("#Logradouro").val(),
                "Telefone": $(this).find("#Telefone").val(),
                "beneficiarios": beneficiarios
            },
            error:
            function (r) {
                if (r.status == 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON);
                else if (r.status == 500)
                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
            success:
            function (r) {
                ModalDialog("Sucesso!", r)
                $("#formCadastro")[0].reset();                                
                window.location.href = urlRetorno;
            }
        });
    })
    
})

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}

function ModalBeneficiarios() {

    var tableBody = ""
    console.log(beneficiarios);

    for (var i = 0; i < beneficiarios.length; i++) {
        tableBody += CriarTableLine(beneficiarios[i].CPF, beneficiarios[i].Nome, i);
    };

    if ($('#tableBody').html().length != beneficiarios.length) {
        $('#tableBody tr').remove();
        $('#tableBody').append(tableBody);
    }

    $('#modalBeneficiarios').modal('show');
}

function ModalEditarBeneficiario(id) {

    $("#UpdateBenf-Nome").val(beneficiarios[id].Nome);
    $("#UpdateBenf-CPF").val(beneficiarios[id].CPF.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4"));

    $('.btnAlterarBeneficiario').prop('id', id);

    $('#modalEditarBeneficiario').modal('show');
}

function AdicionarBeneficiario() {
    var nome = $("#Benf-form #Benf-Nome").val();
    var cpf = $("#Benf-form #Benf-CPF").val();

    if (nome === "" || cpf === "")
        return window.alert("É obrigatório informar o Nome e o CPF.");

    var existe = beneficiarios.find((beneficiario) => beneficiario.CPF == cpf);

    console.log(existe);

    if (existe)
        return window.alert("Já existe um beneficiário com este CPF: " + cpf);
    else {
        beneficiarios.push({ Nome: nome, CPF: cpf });
        var index = beneficiarios.findIndex((beneficiario) => beneficiario.CPF == cpf);
        var tableLine = $(CriarTableLine(cpf, nome, index));
        $("#tableBody").append($(tableLine));
    }

    $("#Benf-form #Benf-Nome").val('');
    $("#Benf-form #Benf-CPF").val('');

    console.log(beneficiarios);
}

function ExcluirBeneficiario(item, index) {
    beneficiarios.splice(index, 1);
    $(item).closest('tr').remove();
}

function CriarTableLine(cpf, nome, index) {
    return `<tr id="${index}"><td>${ cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4") }</td><td>${nome}</td><td><button type="button" class="btn btn-sm btn-primary" onclick="ModalEditarBeneficiario(${index})">Alterar</button>&nbsp<button type="button" class="btn btn-sm btn-primary" onclick="ExcluirBeneficiario(this, ${index})">Excluir</button></td></tr>`
}

function AlterarBeneficiario(id) {
    var nome = $("#UpdateBenf-Nome").val();
    var cpf = $("#UpdateBenf-CPF").val();

    beneficiarios[id].Nome = nome;
    beneficiarios[id].CPF = cpf;

    $('#fecharAtualizarBeneficiario').trigger('click');
    $('#BtnAdicionarBeneficiarios').trigger('click');

    console.log(beneficiarios);
}
