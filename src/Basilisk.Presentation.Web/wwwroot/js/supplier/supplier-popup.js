(function () {
    GetAllSuplier();
}())

function CallEventListener() {
    addContactButtonListener();
    addCloseButtonListener();
    addInsertButtonListener();
    addUpdateButtonListener();
    addSubmitFormListener();
    addDeleteButtonListener();
    addSubmitDeleteButtonListener();
}

function GetAllSuplier() {
    const request = new XMLHttpRequest();
    let token = localStorage.getItem('token');
    request.open("GET", "http://localhost:5231/api/v1/suppliers");
    request.setRequestHeader('Authorization', `Bearer ${token}`);
    request.send();
    request.onload = function () {
        //console.log(JSON.parse(request.responseText));
        let suppliers = JSON.parse(request.responseText);
        Allsupplier(suppliers);
        CallEventListener();
    }
}


function Allsupplier(suppliers) {
    let div = document.querySelector('.grid-container table tbody');
    div.innerHTML = '';
    for (let supplier of suppliers) {
        let row = document.createElement('tr');
        row.innerHTML = `
                <td>
                    <a data-id="${supplier.id}" class="blue-button delete-button">Delete</a>
                    <a data-id="${supplier.id}" class="blue-button update-button">Edit</a>
                    <a data-id="${supplier.id}" class="blue-button contact-button">Contact</a>
                </td>
                <td>${supplier.companyName}</td>
                <td>${supplier.contactPerson}</td>
                <td>${supplier.jobTitle}</td>
                `;

        div.appendChild(row);

    }
}


function addContactButtonListener() {
    $('.contact-button').click(function (event) {
        let supplierId = $(this).attr('data-id');
        let token = localStorage.getItem('token');
        $.ajax({
            url: `http://localhost:5231/api/v1/suppliers/${supplierId}`,
            headers: {
                'Authorization': `Bearer ${token}`
            },
            success: function ({ address, city, phone, email }) {
                $('.contact-dialog .address').text(address);
                $('.contact-dialog .city').text(city);
                $('.contact-dialog .phone').text(phone);
                $('.contact-dialog .email').text(email);
                $('.modal-layer').addClass('modal-layer--opened');
                $('.contact-dialog').addClass('popup-dialog--opened');
            },
            error: function (xhr) {
                console.log(xhr);
                if (xhr.status === 403) {
                    window.location.href = '/auth/AccessDenied';
                }
            }
        });
    });
}

function addCloseButtonListener() {
    $('.close-button').click(function (event) {
        $('.modal-layer').removeClass('modal-layer--opened');
        $('.popup-dialog').removeClass('popup-dialog--opened');
        $('.popup-dialog input').val("");
        $('.popup-dialog textarea').val("");
        $('.popup-dialog .validation-message').text("");
    });
}

function addInsertButtonListener() {
    $('.create-button').click(function (event) {
        event.preventDefault();
        $('.modal-layer').addClass('modal-layer--opened');
        $('.form-dialog').addClass('popup-dialog--opened');
    });
}

function addUpdateButtonListener() {
    $('.update-button').click(function (event) {
        event.preventDefault();
        let supplierId = $(this).attr('data-id');
        let token = localStorage.getItem('token');
        $.ajax({
            url: `http://localhost:5231/api/v1/suppliers/${supplierId}`,
            headers: {
                'Authorization': `Bearer ${token}`
            },
            success: function (response) {
                populateInputForm(response);
                $('.modal-layer').addClass('modal-layer--opened');
                $('.form-dialog').addClass('popup-dialog--opened');
            },
            error: function (xhr) {
                console.log(xhr);
                if (xhr.status === 403) {
                    window.location.href = '/auth/AccessDenied';
                }
            }
        })
    });
}

function populateInputForm({ id, companyName, contactPerson, jobTitle, address, city, phone, email }) {
    $('.form-dialog .id').val(id);
    $('.form-dialog .companyName').val(companyName);
    $('.form-dialog .contactPerson').val(contactPerson);
    $('.form-dialog .jobTitle').val(jobTitle);
    $('.form-dialog .address').val(address);
    $('.form-dialog .city').val(city);
    $('.form-dialog .phone').val(phone);
    $('.form-dialog .email').val(email);
}

function addSubmitFormListener() {
    $('.form-dialog button').click(function (event) {
        event.preventDefault();
        let dto = collectInputForm();
        let token = localStorage.getItem('token');
        let requestMethod = (dto.id === 0) ? 'POST' : 'PUT';
        $.ajax({
            method: requestMethod,
            url: `http://localhost:5231/api/v1/suppliers/${dto.id}`,
            data: JSON.stringify(dto),
            contentType: 'application/json',
            headers: {
                'Authorization': `Bearer ${token}`
            },
            success: function (response) {
                location.reload();
            },
            error: function ({ status, responseJSON }) {
                if (status === 400) {
                    writeValidationMessage(responseJSON.errors);
                }
                else if (status === 403) {
                    window.location.href = '/auth/AccessDenied';
                }
            }
        });
    })
}

function collectInputForm() {
    let id = $('.form-dialog .id').val()
    let dto = {
        id: (id == 0) ? 0 : id,
        companyName: $('.form-dialog .companyName').val(),
        contactPerson: $('.form-dialog .contactPerson').val(),
        jobTitle: $('.form-dialog .jobTitle').val(),
        address: $('.form-dialog .address').val(),
        city: $('.form-dialog .city').val(),
        phone: $('.form-dialog .phone').val(),
        email: $('.form-dialog .email').val()
    };
    return dto;
}

function writeValidationMessage(errorMessages) {
    for (let field in errorMessages) {
        let messages = errorMessages[field];
        let camelCasedField = field.replace(field[0], field[0].toLowerCase())
        $(`.form-dialog [data-for=${camelCasedField}]`).text(messages[0]);
    }
}

function addDeleteButtonListener() {
    $('.delete-button').click(function (event) {
        event.preventDefault();
        let supplierId = $(this).attr('data-id');
        $('.delete-dialog .id').val(supplierId);
        $('.modal-layer').addClass('modal-layer--opened');
        $('.delete-dialog').addClass('popup-dialog--opened');
    });
}

function addSubmitDeleteButtonListener() {
    $('.remove-button').click(function (event) {
        let supplierId = $('.delete-dialog .id').val();
        let token = localStorage.getItem('token');
        $.ajax({
            method: "DELETE",
            url: `http://localhost:5231/api/v1/suppliers/${supplierId}`,
            headers: {
                'Authorization': `Bearer ${token}`
            },
            success: function (response) {
                location.reload();
            },
            error: function (xhr) {
                if (xhr.status === 403) {
                    window.location.href = '/auth/AccessDenied';
                }
            }
        });
    });
}